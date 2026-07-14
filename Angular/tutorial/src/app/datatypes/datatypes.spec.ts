import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Datatypes } from './datatypes';

describe('Datatypes', () => {
  let component: Datatypes;
  let fixture: ComponentFixture<Datatypes>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Datatypes],
    }).compileComponents();

    fixture = TestBed.createComponent(Datatypes);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
