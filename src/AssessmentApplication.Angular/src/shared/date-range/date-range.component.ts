import { Component, forwardRef, Input, OnDestroy } from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormBuilder,
  FormControl,
  FormGroup,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  Validator,
} from '@angular/forms';
import { dateRangeValidator } from '@shared/validators';
import { Subscription } from 'rxjs';
import { DateRangeModel } from '@shared/models';

@Component({
  selector: 'shared-date-range',
  templateUrl: './date-range.component.html',
  styleUrls: ['./date-range.component.scss'],
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
})
export class DateRangeComponent
  implements ControlValueAccessor, OnDestroy, Validator
{
  public dateRangeForm: FormGroup;

  private _errorMessage: string = '';
  private _label: string = '';
  private _onChange: Function = () => {};
  private _onTouched: Function = () => {};
  private _placeholderFrom: string = '';
  private _placeholderTo: string = '';
  private _subscriptions: Subscription[] = [];

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
    return this.dateRangeForm.controls['fromDate'];
  }

  get placeholderFrom() {
    return this._placeholderFrom;
  }

  get placeholderTo() {
    return this._placeholderTo;
  }

  get toDateControl() {
    return this.dateRangeForm.controls['toDate'];
  }

  get value(): DateRangeModel {
    return this.dateRangeForm.value;
  }

  set value(value: DateRangeModel) {
    this.dateRangeForm.setValue(value);
    this._onChange(value);
    this._onTouched();
  }

  constructor(private formBuilder: FormBuilder) {
    this.dateRangeForm = this.formBuilder.group(
      {
        fromDate: new FormControl(''),
        toDate: new FormControl(''),
      },
      {
        validators: [dateRangeValidator('fromDate', 'toDate')],
      }
    );

    this._subscriptions.push(
      this.dateRangeForm.valueChanges.subscribe((value) => {
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

  public writeValue(value: DateRangeModel) {
    if (value) {
      this.value = value;
    }

    if (value === null) {
      this.dateRangeForm.reset();
    }
  }

  public registerOnTouched(fn: any) {
    this._onTouched = fn;
  }

  // communicate the inner form validation to the parent form
  public validate(_: AbstractControl) {
    return this.dateRangeForm.valid ? null : { dateRange: { valid: false } };
  }
}
