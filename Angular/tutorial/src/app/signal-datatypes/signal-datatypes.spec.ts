import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignalDatatypes } from './signal-datatypes';

describe('SignalDatatypes', () => {
  let component: SignalDatatypes;
  let fixture: ComponentFixture<SignalDatatypes>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignalDatatypes],
    }).compileComponents();

    fixture = TestBed.createComponent(SignalDatatypes);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
