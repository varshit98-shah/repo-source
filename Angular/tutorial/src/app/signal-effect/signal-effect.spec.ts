import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignalEffect } from './signal-effect';

describe('SignalEffect', () => {
  let component: SignalEffect;
  let fixture: ComponentFixture<SignalEffect>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignalEffect],
    }).compileComponents();

    fixture = TestBed.createComponent(SignalEffect);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
