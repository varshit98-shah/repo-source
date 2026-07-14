import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignalsP } from './signals-p';

describe('SignalsP', () => {
  let component: SignalsP;
  let fixture: ComponentFixture<SignalsP>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignalsP],
    }).compileComponents();

    fixture = TestBed.createComponent(SignalsP);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
