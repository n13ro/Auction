import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Lot } from './lot';

describe('Lot', () => {
  let component: Lot;
  let fixture: ComponentFixture<Lot>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Lot]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Lot);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
