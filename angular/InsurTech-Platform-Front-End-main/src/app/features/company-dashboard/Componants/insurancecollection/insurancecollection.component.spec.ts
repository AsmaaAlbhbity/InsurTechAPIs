import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsurancecollectionComponent } from './insurancecollection.component';

describe('InsurancecollectionComponent', () => {
  let component: InsurancecollectionComponent;
  let fixture: ComponentFixture<InsurancecollectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InsurancecollectionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InsurancecollectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
