import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomearticlesComponent } from './homearticles.component';

describe('HomearticlesComponent', () => {
  let component: HomearticlesComponent;
  let fixture: ComponentFixture<HomearticlesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomearticlesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HomearticlesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
