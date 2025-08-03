import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotCardAlt } from './lot-card-alt';

describe('LotCardAlt', () => {
  let component: LotCardAlt;
  let fixture: ComponentFixture<LotCardAlt>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LotCardAlt]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotCardAlt);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
