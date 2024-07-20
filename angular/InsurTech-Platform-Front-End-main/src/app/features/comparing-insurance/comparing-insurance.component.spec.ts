import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComparingInsuranceComponent } from './comparing-insurance.component';

describe('ComparingInsuranceComponent', () => {
  let component: ComparingInsuranceComponent;
  let fixture: ComponentFixture<ComparingInsuranceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComparingInsuranceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ComparingInsuranceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
