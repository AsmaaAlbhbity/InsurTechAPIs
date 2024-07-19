import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditHealthInsurancePlanComponent } from './edithealthinsurance.component';

describe('EdithealthinsuranceComponent', () => {
  let component: EditHealthInsurancePlanComponent;
  let fixture: ComponentFixture<EditHealthInsurancePlanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditHealthInsurancePlanComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditHealthInsurancePlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
