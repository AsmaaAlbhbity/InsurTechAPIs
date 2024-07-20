import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditmotorinsuranceComponent } from './editmotorinsurance.component';

describe('EditmotorinsuranceComponent', () => {
  let component: EditmotorinsuranceComponent;
  let fixture: ComponentFixture<EditmotorinsuranceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditmotorinsuranceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditmotorinsuranceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
