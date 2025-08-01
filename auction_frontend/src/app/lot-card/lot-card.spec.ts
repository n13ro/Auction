import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotCard } from './lot-card';

describe('LotCard', () => {
  let component: LotCard;
  let fixture: ComponentFixture<LotCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LotCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
