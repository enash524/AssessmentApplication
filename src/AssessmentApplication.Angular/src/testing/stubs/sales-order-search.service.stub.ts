import { Injectable } from "@angular/core";
import {
  SalesOrderDetail,
  SalesOrderHeaderModel,
  SalesOrderSearchModel,
} from "@app/sales-order";
import { PagedResponseModel } from "@shared/models";
import { Observable, of } from "rxjs";

@Injectable()
export class SalesOrderSearchServiceStub {
  public get(id: number): Observable<SalesOrderDetail[]> {
    if (id <= 0) {
      return of([]);
    }

    const details: SalesOrderDetail[] = [];
    for (let i = 1; i <= id; i++) {
      const detail: SalesOrderDetail = {
        lineTotal: i,
        name: `Test ${i}`,
        orderQty: i,
        productNumber: `ABC123${i}`,
        unitPrice: 1.23 * 1,
        unitPriceDiscount: 0.1 * 1,
      };
      details.push(detail);
    }
    return of(details);
  }

  public search(
    searchModel: SalesOrderSearchModel
  ): Observable<PagedResponseModel<SalesOrderHeaderModel[]>> {
    return of();
  }
}
