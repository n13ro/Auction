import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeOutLot } from './time-out-lot';

describe('TimeOutLot', () => {
  let component: TimeOutLot;
  let fixture: ComponentFixture<TimeOutLot>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TimeOutLot]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TimeOutLot);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
