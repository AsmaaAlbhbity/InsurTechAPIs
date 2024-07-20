import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RippleModule } from 'primeng/ripple';
import { ListInsurancePlanComponent } from './list-insurance-plan.component';

describe('ListInsurancePlanComponent', () => {
  let component: ListInsurancePlanComponent;
  let fixture: ComponentFixture<ListInsurancePlanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListInsurancePlanComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListInsurancePlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
