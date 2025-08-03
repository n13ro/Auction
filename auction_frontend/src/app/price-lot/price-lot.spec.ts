import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PriceLot } from './price-lot';

describe('PriceLot', () => {
  let component: PriceLot;
  let fixture: ComponentFixture<PriceLot>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PriceLot]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PriceLot);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
