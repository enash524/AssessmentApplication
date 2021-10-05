import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription, of } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { SalesOrderDetail, SalesOrderSearchService } from '..';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {

  public salesOrderDetails: SalesOrderDetail[] | null = null;
  private _subscriptions: Subscription[] = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private salesOrderService: SalesOrderSearchService,
  ) {}

  ngOnInit(): void {
    this.activatedRoute.paramMap.pipe(
      map(params => {
        const param = params.get('id');
        return param ? +param : null;
      }),
      switchMap(id => id ? this.salesOrderService.get(id) : of([])),
    ).subscribe(details => this.salesOrderDetails = details);
  }

}
