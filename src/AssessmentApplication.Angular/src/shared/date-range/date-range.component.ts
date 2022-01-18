import { Component, Input } from '@angular/core';
import { DateRangeForm } from '.';

@Component({
  selector: 'shared-date-range',
  templateUrl: './date-range.component.html',
  styleUrls: ['./date-range.component.scss'],
})
export class DateRangeComponent {

  private _errorMessage: string = '';
  private _label: string = '';
  private _placeholderFrom: string = '';
  private _placeholderTo: string = '';

  @Input()
  public dateRangeForm: DateRangeForm;

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

  get placeholderFrom() {
    return this._placeholderFrom;
  }

  get placeholderTo() {
    return this._placeholderTo;
  }

}