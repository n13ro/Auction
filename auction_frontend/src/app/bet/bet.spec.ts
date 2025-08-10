import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Bet } from './bet';

describe('Bet', () => {
  let component: Bet;
  let fixture: ComponentFixture<Bet>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Bet]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Bet);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
