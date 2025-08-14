import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateLot } from './create-lot';

describe('CreateLot', () => {
  let component: CreateLot;
  let fixture: ComponentFixture<CreateLot>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateLot]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateLot);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
