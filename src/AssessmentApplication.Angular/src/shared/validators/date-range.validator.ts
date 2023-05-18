import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { DateRangeModel } from "..";

export function dateRangeValidator(): ValidatorFn {
  return (
    control: AbstractControl<DateRangeModel | null>
  ): ValidationErrors | null => {
    const model: DateRangeModel = new DateRangeModel(
      control.value?.fromDate,
      control.value?.toDate
    );
    return model.isValid()
      ? null
      : {
          invalidRange: {
            fromDate: control.value?.fromDate,
            toDate: control.value?.toDate,
          },
        };
  };
}
