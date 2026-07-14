import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetSetSignal } from './get-set-signal';

describe('GetSetSignal', () => {
  let component: GetSetSignal;
  let fixture: ComponentFixture<GetSetSignal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GetSetSignal],
    }).compileComponents();

    fixture = TestBed.createComponent(GetSetSignal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
