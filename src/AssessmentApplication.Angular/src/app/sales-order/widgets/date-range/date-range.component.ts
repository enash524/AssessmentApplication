import { Component, forwardRef, Input, OnDestroy } from '@angular/core';
import { ControlValueAccessor, FormBuilder, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR } from '@angular/forms';
import { DateRangeValidator } from '@shared/validators';
import { Subscription } from 'rxjs';

export interface DateRangeFormValues {
  fromDate: Date,
  toDate: Date
}

@Component({
  selector: 'app-date-range',
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
  ]
})
export class DateRangeComponent implements ControlValueAccessor, OnDestroy {

  public dateRange: DateRangeFormValues | null = null;
  public dateRangeForm: FormGroup;
  public onChange: Function = () => { };
  public onTouched: Function = () => { };

  private _errorMessage: string = '';
  private _label: string = '';
  private _placeholderFrom: string = '';
  private _placeholderTo: string = '';
  private _subscriptions: Subscription[] = [];

  @Input()
  set label(value: string) {
    this._label = value;
    this._errorMessage = `${ value } End cannot occur before ${ value } Start`;
    this._placeholderFrom = `${ value } Start`;
    this._placeholderTo = `${ value } End`;
  }

  get label() {
    return this._label;
  }

  get errorMessage() {
    return this._errorMessage;
  }

  get placeholderFrom() {
    return this._placeholderFrom;
  }

  get placeholderTo() {
    return this._placeholderTo;
  }

  constructor(private formBuilder: FormBuilder) {
    this.dateRangeForm = this.formBuilder.group({
      fromDate: [],
      toDate: []
    },
      {
        validator: DateRangeValidator('fromDate', 'toDate')
      });

    this._subscriptions.push(
      this.dateRangeForm.valueChanges.subscribe(value => {
        this.onChange(value);
        this.onTouched();
      })
    )
  }

  public ngOnDestroy() {
    this._subscriptions.forEach((s: Subscription) => s.unsubscribe());
  }

  public registerOnChange(fn: any) {
    this.onChange = fn;
  }

  public writeValue(value: DateRangeFormValues) {
    if (value) {
      this.dateRange = value;
    }

    if (value === null) {
      this.dateRangeForm.reset();
    }
  }

  public registerOnTouched(fn: any) {
    this.onTouched = fn;
  }

  // communicate the inner form validation to the parent form
  public validate(_: FormControl) {
    return this.dateRangeForm.valid ? null : { dateRange: { valid: false } };
  }

}