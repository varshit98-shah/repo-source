import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ControlStatement } from './control-statement';

describe('ControlStatement', () => {
  let component: ControlStatement;
  let fixture: ComponentFixture<ControlStatement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ControlStatement],
    }).compileComponents();

    fixture = TestBed.createComponent(ControlStatement);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
