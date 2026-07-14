import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PipesExample } from './pipes-example';

describe('PipesExample', () => {
  let component: PipesExample;
  let fixture: ComponentFixture<PipesExample>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PipesExample],
    }).compileComponents();

    fixture = TestBed.createComponent(PipesExample);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
