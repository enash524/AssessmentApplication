import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'shared-input-textbox',
  templateUrl: './input-textbox.component.html',
  styleUrls: ['./input-textbox.component.scss']
})
export class InputTextboxComponent {

  private _label: string;
  private _placeholder: string;

  @Input()
  public control: FormControl;

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

}
