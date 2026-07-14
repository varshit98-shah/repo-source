import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CounterAppSignal } from './counter-app-signal';

describe('CounterAppSignal', () => {
  let component: CounterAppSignal;
  let fixture: ComponentFixture<CounterAppSignal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CounterAppSignal],
    }).compileComponents();

    fixture = TestBed.createComponent(CounterAppSignal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
