import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EdithomeinsuranceComponent } from './edithomeinsurance.component';

describe('EdithomeinsuranceComponent', () => {
  let component: EdithomeinsuranceComponent;
  let fixture: ComponentFixture<EdithomeinsuranceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EdithomeinsuranceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EdithomeinsuranceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
