import {
  Component,
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
import { InputTextModule } from "primeng/inputtext";
import { debounceTime } from "rxjs";

@Component({
  selector: "shared-input-textbox",
  templateUrl: "./input-textbox.component.html",
  styleUrl: "./input-textbox.component.scss",
  imports: [FormsModule, InputTextModule, ReactiveFormsModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputTextboxComponent),
      multi: true,
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => InputTextboxComponent),
      multi: true,
    },
  ],
  host: {
    "[id]": "id",
  },
})
export class InputTextboxComponent
  implements ControlValueAccessor, OnInit, Validator
{
  static nextId = 0;
  id = `input-textbox-${InputTextboxComponent.nextId++}`;
  public inputTextboxForm: FormGroup<InputTextboxForm> =
    new FormGroup<InputTextboxForm>({
      textboxValue: new FormControl<string | null>(null),
    });

  private destroyRef = inject(DestroyRef);
  private _onChange: (value: string) => void = () => {};
  private _onTouched: () => void = () => {};
  public label = input.required<string>();

  ngOnInit(): void {
    this.inputTextboxForm.valueChanges
      .pipe(debounceTime(250), takeUntilDestroyed(this.destroyRef))
      .subscribe((value) => {
        if (this._onChange) {
          this._onChange(value.textboxValue);
        }
      });
  }

  writeValue(value: string): void {
    this.inputTextboxForm.patchValue(
      { textboxValue: value },
      { emitEvent: false }
    );
  }

  registerOnChange(fn: (val: string) => void): void {
    this._onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this._onTouched = fn;
  }

  // communicate the inner form validation to the parent form
  validate(_: AbstractControl<string>): ValidationErrors | null {
    return this.inputTextboxForm.valid
      ? null
      : { inputText: { invalid: true } };
  }

  public handleOnTouched(): void {
    if (this._onTouched) {
      this._onTouched();
    }
  }
}

export type InputTextboxForm = {
  textboxValue: FormControl<string | null>;
};
