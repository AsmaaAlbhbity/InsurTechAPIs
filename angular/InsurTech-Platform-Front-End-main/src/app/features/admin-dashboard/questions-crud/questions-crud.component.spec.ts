import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionsCrudComponent } from './questions-crud.component';

describe('QuestionsCrudComponent', () => {
  let component: QuestionsCrudComponent;
  let fixture: ComponentFixture<QuestionsCrudComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuestionsCrudComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(QuestionsCrudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
