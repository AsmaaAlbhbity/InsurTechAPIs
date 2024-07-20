import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyInsurancePlanComponent } from './company-insurance-plan.component';

describe('CompanyInsurancePlanComponent', () => {
  let component: CompanyInsurancePlanComponent;
  let fixture: ComponentFixture<CompanyInsurancePlanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyInsurancePlanComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CompanyInsurancePlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
