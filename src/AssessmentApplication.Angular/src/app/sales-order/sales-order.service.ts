import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from '@app/env.service';
import { PagedResponseModel, SortDirection } from '@shared/models';
import { Observable, of } from 'rxjs';
import { SalesOrderDetail, SalesOrderHeaderModel, SalesOrderSearchModel } from '.';


@Injectable({
  providedIn: 'root'
})
export class SalesOrderSearchService {

  private readonly url: string;

  constructor(
    private env: EnvService,
    private http: HttpClient,
  ) {
    this.url = `${this.env.api}/sales/`;
  }

  public get(id: number): Observable<SalesOrderDetail[]> {
    const endpoint: string = `${this.url}detail/${id}`;
    return this.http.get<SalesOrderDetail[]>(endpoint);
  }

  public search(searchModel: SalesOrderSearchModel): Observable<PagedResponseModel<SalesOrderHeaderModel[]>> {
    const qs: string = this.generateQueryString(searchModel);
    const endpoint: string = `${this.url}SalesOrderHeader?${qs}`;
    return this.http.get<PagedResponseModel<SalesOrderHeaderModel[]>>(endpoint);
  }

  private generateQueryString(searchModel: SalesOrderSearchModel) {
    const str: string[] = [];

    Object.keys(searchModel).forEach((key: string) => {
      let value = searchModel[key];
      if (value === null || value === undefined) {
        return;
      }
      if (value instanceof Date) {
        value = value.toISOString();
      }
      if (key === 'sortDirection') {
        value = value === SortDirection.Asc ? 'Asc' : 'Desc';
      }
      str.push(`${key}=${value}`);
    });

    return str.join('&');
  }

}
