import { CommonModule } from "@angular/common";
import {
  Component,
  computed,
  DestroyRef,
  forwardRef,
  inject,
  input,
  OnInit,
} from "@angular/core";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  FormGroup,
  FormsModule,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
  ValidationErrors,
  Validator,
} from "@angular/forms";
import { DateRangeModel } from "@shared/models";
import { dateRangeValidator } from "@shared/validators";
import { DatePickerModule } from "primeng/datepicker";

@Component({
  selector: "shared-date-range",
  templateUrl: "./date-range.component.html",
  styleUrl: "./date-range.component.scss",
  imports: [CommonModule, DatePickerModule, FormsModule, ReactiveFormsModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DateRangeComponent),
      multi: true,
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => DateRangeComponent),
      multi: true,
    },
  ],
  host: {
    "[id]": "id",
  },
})
export class DateRangeComponent
  implements ControlValueAccessor, OnInit, Validator
{
  static nextId = 0;
  id = `date-range-${DateRangeComponent.nextId++}`;
  public dateRangeForm: FormGroup = new FormGroup<DateRangeForm>(
    {
      fromDate: new FormControl<Date | null>(null),
      toDate: new FormControl<Date | null>(null),
    },
    {
      validators: [dateRangeValidator()],
    }
  );

  public errorMessage = computed(
    () => `${this.label()} End cannot occur before ${this.label()} Start`
  );
  public placeholderFrom = computed(() => `${this.label()} Start`);
  public placeholderTo = computed(() => `${this.label()} End`);
  public label = input.required<string>();

  private destroyRef = inject(DestroyRef);
  private _onChange: (val: Partial<DateRangeModel> | null) => void = () => {};
  private _onTouched: () => void = () => {};

  get fromDateControl() {
    return this.dateRangeForm.controls["fromDate"];
  }

  get toDateControl() {
    return this.dateRangeForm.controls["toDate"];
  }

  ngOnInit(): void {
    this.dateRangeForm.valueChanges
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((value: Partial<DateRangeModel> | null) => {
        if (this._onChange) {
          const dateRange = this.createDateRange(value);
          this._onChange(dateRange);
        }
      });
  }

  writeValue(value: Partial<DateRangeModel> | null): void {
    const dateRange = this.createDateRange(value);
    this.dateRangeForm.patchValue(dateRange);
  }

  registerOnChange(fn: (value: Partial<DateRangeModel> | null) => void): void {
    this._onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this._onTouched = fn;
  }

  // communicate the inner form validation to the parent form
  validate(
    control: AbstractControl<DateRangeModel | null>
  ): ValidationErrors | null {
    return control.value?.isValid() ? null : { dateRange: { invalid: true } };
  }

  public handleOnTouched(): void {
    if (this._onTouched) {
      this._onTouched();
    }
  }

  private createDateRange(
    value: Partial<DateRangeModel> | null
  ): DateRangeModel {
    return new DateRangeModel(value?.fromDate, value?.toDate);
  }
}

export type DateRangeForm = {
  fromDate: FormControl<Date | null>;
  toDate: FormControl<Date | null>;
};
