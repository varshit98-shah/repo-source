import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HandleCount } from './handle-count';

describe('HandleCount', () => {
  let component: HandleCount;
  let fixture: ComponentFixture<HandleCount>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HandleCount],
    }).compileComponents();

    fixture = TestBed.createComponent(HandleCount);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
