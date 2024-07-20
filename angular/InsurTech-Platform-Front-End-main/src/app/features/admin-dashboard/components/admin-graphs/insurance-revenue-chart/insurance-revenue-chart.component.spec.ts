import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsuranceRevenueChartComponent } from './insurance-revenue-chart.component';

describe('InsuranceRevenueChartComponent', () => {
  let component: InsuranceRevenueChartComponent;
  let fixture: ComponentFixture<InsuranceRevenueChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InsuranceRevenueChartComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InsuranceRevenueChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
