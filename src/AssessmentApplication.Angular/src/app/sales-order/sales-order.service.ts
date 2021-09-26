import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from '@app/env.service';
import { Observable, of } from 'rxjs';
import { SalesOrderSearchModel } from '.';


@Injectable({
  providedIn: 'root'
})
export class SalesOrderSearchService {

  constructor(
    private env: EnvService,
    private http: HttpClient,
  ) { }

  public search(searchModel: SalesOrderSearchModel): Observable<any> {
    return of(null);
  }
}
