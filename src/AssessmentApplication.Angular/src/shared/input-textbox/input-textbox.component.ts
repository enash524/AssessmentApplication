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
import { debounceTime, Subject, takeUntil } from "rxjs";

@Component({
  selector: "shared-input-textbox",
  templateUrl: "./input-textbox.component.html",
  styleUrls: ["./input-textbox.component.scss"],
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
  implements ControlValueAccessor, OnDestroy, Validator
{
  static nextId = 0;
  id = `input-textbox-${InputTextboxComponent.nextId++}`;
  public inputTextboxForm: FormGroup<InputTextboxForm> =
    new FormGroup<InputTextboxForm>({
      textboxValue: new FormControl<string | null>(null),
    });

  private _destroyed$: Subject<void> = new Subject();
  private _label: string = "";
  private _onTouched: Function = () => {};
  private _placeholder: string = "";

  @Input()
  set label(value: string) {
    this._label = value;
    this._placeholder = value;
  }

  get label() {
    return this._label;
  }

  get placeholder() {
    return this._placeholder;
  }

  public ngOnDestroy(): void {
    this._destroyed$.next();
    this._destroyed$.complete();
  }

  writeValue(value: string): void {
    this.inputTextboxForm.patchValue(
      { textboxValue: value },
      { emitEvent: false }
    );
  }

  registerOnChange(fn: (val: string) => void): void {
    this.inputTextboxForm.valueChanges
      .pipe(debounceTime(500), takeUntil(this._destroyed$))
      .subscribe((value) => {
        fn(value.textboxValue);
      });
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
