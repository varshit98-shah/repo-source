import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgotpPassword } from './forgotp-password';

describe('ForgotpPassword', () => {
  let component: ForgotpPassword;
  let fixture: ComponentFixture<ForgotpPassword>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForgotpPassword],
    }).compileComponents();

    fixture = TestBed.createComponent(ForgotpPassword);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
