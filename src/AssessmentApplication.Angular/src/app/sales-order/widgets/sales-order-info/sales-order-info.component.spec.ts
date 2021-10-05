import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesOrderInfoComponent } from './sales-order-info.component';

describe('SalesOrderInfoComponent', () => {
  let component: SalesOrderInfoComponent;
  let fixture: ComponentFixture<SalesOrderInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SalesOrderInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SalesOrderInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
