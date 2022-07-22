import { Component, forwardRef, Input, OnDestroy } from "@angular/core";
import {
  AbstractControl,
  ControlValueAccessor,
  FormBuilder,
  FormControl,
  FormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  Validator,
} from "@angular/forms";
import { InputTextboxModel } from "@shared/models";
import { Subscription } from "rxjs";

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
})
export class InputTextboxComponent
  implements ControlValueAccessor, OnDestroy, Validator {
  public inputTextboxForm: FormGroup;

  private _label: string = "";
  private _onChange: Function = () => { };
  private _onTouched: Function = () => { };
  private _placeholder: string = "";
  private _subscriptions: Subscription[] = [];

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

  get textboxControl() {
    return this.inputTextboxForm.controls["textboxValue"];
  }

  get value(): InputTextboxModel {
    return this.inputTextboxForm.value;
  }

  set value(value: InputTextboxModel) {
    this.inputTextboxForm.setValue(value);
    this._onChange(value);
    this._onTouched();
  }

  constructor(private formBuilder: FormBuilder) {
    this.inputTextboxForm = this.formBuilder.group({
      textboxValue: new FormControl<string>(""),
    });

    this._subscriptions.push(
      this.inputTextboxForm.valueChanges.subscribe((value) => {
        this._onChange(value);
        this._onTouched();
      })
    );
  }

  public ngOnDestroy() {
    this._subscriptions.forEach((s: Subscription) => s.unsubscribe());
  }

  public registerOnChange(fn: any) {
    this._onChange = fn;
  }

  public writeValue(value: InputTextboxModel) {
    if (value) {
      this.value = value;
    }

    if (value === null) {
      this.inputTextboxForm.reset();
    }
  }

  public registerOnTouched(fn: any) {
    this._onTouched = fn;
  }

  // communicate the inner form validation to the parent form
  public validate(_: AbstractControl) {
    return this.inputTextboxForm.valid ? null : { inputText: { valid: false } };
  }
}
