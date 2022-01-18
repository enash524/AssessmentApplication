import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { DateRangeForm } from "@shared/date-range";
import { SearchModel } from ".";

export class SearchForm extends FormGroup {

    readonly customerName = this.get('customerName') as FormControl;

    get orderDate(): DateRangeForm {
        return this.controls['orderDate'] as DateRangeForm;
    }

    get dueDate(): DateRangeForm {
        return this.controls['dueDate'] as DateRangeForm;
    }

    get shipDate(): DateRangeForm {
        return this.controls['shipDate'] as DateRangeForm;
    }

    constructor(readonly model: SearchModel, readonly fb: FormBuilder = new FormBuilder()) {
        super(
            fb.group({
                orderDate: new DateRangeForm(model?.orderDate, fb),
                dueDate: new DateRangeForm(model?.dueDate, fb),
                shipDate: new DateRangeForm(model?.shipDate, fb),
                customerName: [model?.customerName],
            }).controls
        );
    }

}