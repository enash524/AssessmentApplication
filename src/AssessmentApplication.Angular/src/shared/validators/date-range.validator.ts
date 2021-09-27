import { AbstractControl, FormGroup, ValidationErrors } from '@angular/forms';

export function DateRangeValidator(
    fromDate: string,
    toDate: string
) {
    return (formGroup: FormGroup) : ValidationErrors | null => {
        const fromDateControl: AbstractControl = formGroup.controls[fromDate];
        const toDateControl: AbstractControl = formGroup.controls[toDate];

        if (fromDateControl.value == null || toDateControl.value == null) {
            return null;
        }

        const startDateValue: Date = new Date(fromDateControl.value);
        const endDateValue: Date = new Date(toDateControl.value);
        const invalid: boolean = startDateValue > endDateValue;

        return invalid ? { invalidRange: { startDateValue, endDateValue } } : null;
    }
}
