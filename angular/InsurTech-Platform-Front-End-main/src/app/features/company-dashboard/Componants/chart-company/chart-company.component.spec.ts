import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartCompanyComponent } from './chart-company.component';

describe('ChartCompanyComponent', () => {
  let component: ChartCompanyComponent;
  let fixture: ComponentFixture<ChartCompanyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChartCompanyComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChartCompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
