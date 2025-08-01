import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotMap } from './lot-map';

describe('LotMap', () => {
  let component: LotMap;
  let fixture: ComponentFixture<LotMap>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LotMap]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotMap);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
