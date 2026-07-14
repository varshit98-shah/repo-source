import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InlineDemo } from './inline-demo';

describe('InlineDemo', () => {
  let component: InlineDemo;
  let fixture: ComponentFixture<InlineDemo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InlineDemo],
    }).compileComponents();

    fixture = TestBed.createComponent(InlineDemo);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
