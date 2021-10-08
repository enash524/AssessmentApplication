import { AbstractControl, ValidationErrors } from '@angular/forms';

export function DateRangeValidator(
    fromDate: string,
    toDate: string
) {
    return (formGroup: AbstractControl) : ValidationErrors | null => {
        const fromDateControl: AbstractControl | null = formGroup.get(fromDate);
        const toDateControl: AbstractControl | null = formGroup.get(toDate);

        if (fromDateControl?.value == null || toDateControl?.value == null) {
            return null;
        }

        const startDateValue: Date = new Date(fromDateControl.value);
        const endDateValue: Date = new Date(toDateControl.value);
        const invalid: boolean = startDateValue > endDateValue;

        return invalid ? { invalidRange: { startDateValue, endDateValue } } : null;
    }
}
