import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { DateRangeModel } from ".";
import { dateRangeValidator } from "@shared/validators";

export class DateRangeForm extends FormGroup {

    readonly fromDate = this.get('fromDate') as FormControl;
    readonly toDate = this.get('toDate') as FormControl;

    constructor(readonly model: DateRangeModel, readonly fb: FormBuilder = new FormBuilder()) {
        super(
            fb.group({
                fromDate: [model?.fromDate],
                toDate: [model?.toDate]
            }).controls,
            {
                validators: [dateRangeValidator('fromDate', 'toDate')],
            }
        );
    }

}