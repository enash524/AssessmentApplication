import { DateTime } from "luxon";

export class DateRangeModel {
  constructor(
    public fromDate?: Date | null,
    public toDate?: Date | null
  ) {}

  public isValid(): boolean {
    if (this.fromDate && this.toDate) {
      const toDateTime: DateTime = DateTime.fromJSDate(this.toDate);
      const fromDateTime: DateTime = DateTime.fromJSDate(this.fromDate);
      return fromDateTime <= toDateTime;
    }

    return true;
  }
}
