import { Component, forwardRef, Input, OnDestroy } from "@angular/core";
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  FormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator,
} from "@angular/forms";
import { dateRangeValidator } from "@shared/validators";
import { Subject, takeUntil } from "rxjs";
import { DateRangeModel } from "@shared/models";

@Component({
  selector: "shared-date-range",
  templateUrl: "./date-range.component.html",
  styleUrls: ["./date-range.component.scss"],
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
  implements ControlValueAccessor, OnDestroy, Validator
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

  private _destroyed$: Subject<void> = new Subject();
  private _errorMessage: string = "";
  private _label: string = "";
  private _onTouched: () => void = () => {};
  private _placeholderFrom: string = "";
  private _placeholderTo: string = "";

  @Input()
  set label(value: string) {
    this._label = value;
    this._errorMessage = `${value} End cannot occur before ${value} Start`;
    this._placeholderFrom = `${value} Start`;
    this._placeholderTo = `${value} End`;
  }

  get label() {
    return this._label;
  }

  get errorMessage() {
    return this._errorMessage;
  }

  get fromDateControl() {
    return this.dateRangeForm.controls["fromDate"];
  }

  get placeholderFrom() {
    return this._placeholderFrom;
  }

  get placeholderTo() {
    return this._placeholderTo;
  }

  get toDateControl() {
    return this.dateRangeForm.controls["toDate"];
  }

  ngOnDestroy(): void {
    this._destroyed$.next();
    this._destroyed$.complete();
  }

  writeValue(value: Partial<DateRangeModel> | null): void {
    const dateRange = this.createDateRange(value);
    this.dateRangeForm.patchValue(dateRange);
  }

  registerOnChange(fn: (val: Partial<DateRangeModel> | null) => void): void {
    this.dateRangeForm.valueChanges
      .pipe(takeUntil(this._destroyed$))
      .subscribe((value) => {
        const dateRange = this.createDateRange(value);
        fn(dateRange);
      });
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
