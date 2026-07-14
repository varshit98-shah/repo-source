import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomPipeImplementation } from './custom-pipe-implementation';

describe('CustomPipeImplementation', () => {
  let component: CustomPipeImplementation;
  let fixture: ComponentFixture<CustomPipeImplementation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomPipeImplementation],
    }).compileComponents();

    fixture = TestBed.createComponent(CustomPipeImplementation);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
