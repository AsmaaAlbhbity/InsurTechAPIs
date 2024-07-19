import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsuranceChartComponent } from './insurance-chart.component';

describe('InsuranceChartComponent', () => {
  let component: InsuranceChartComponent;
  let fixture: ComponentFixture<InsuranceChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InsuranceChartComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InsuranceChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
