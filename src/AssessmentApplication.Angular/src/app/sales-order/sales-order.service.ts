import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from '@app/env.service';
import { PagedResponseModel } from '@shared/models';
import { PagedRequestModel } from '@shared/models/paged-request.model';
import { Observable } from 'rxjs';
import { SalesOrderHeaderModel, SalesOrderSearchModel } from '.';


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

  public search(searchModel: SalesOrderSearchModel): Observable<PagedResponseModel<SalesOrderHeaderModel[]>> {
    const qs: string = this.generateQueryString(searchModel);
    const endpoint: string = `${this.url}SalesOrderHeader?${qs}`;
    return this.http.get<PagedResponseModel<SalesOrderHeaderModel[]>>(endpoint);
  }

  private generateQueryString(pagedRequest: PagedRequestModel) {
    // TODO - NEED TO ADD REMAINING SEARCH PARAMETERS!!!
    const queryString: string[] = [
      `limit=${ pagedRequest.limit }`,
      `offset=${ pagedRequest.offset }`,
      `sortBy=${ pagedRequest.sortBy || 'SalesOrderId' }`,
      `sortDirection=${ pagedRequest.sortDirection || 'Asc' }`
    ];
    return queryString.join('&');
  }

}
