import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import * as moment from 'moment';

export function dateRangeValidator(
  fromDatePath: string,
  toDatePath: string
): ValidatorFn {
  return (formGroup: AbstractControl): ValidationErrors | null => {
    const fromDate: Date = formGroup.get(fromDatePath)?.value;
    const toDate: Date = formGroup.get(toDatePath)?.value;

    if (fromDate && toDate) {
      const invalid: boolean = moment(fromDate).isAfter(toDate);

      return invalid
        ? { invalidRange: { fromDate: fromDate, toDate: toDate } }
        : null;
    }

    return null;
  };
}
