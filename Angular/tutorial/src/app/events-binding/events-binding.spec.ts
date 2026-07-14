import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventsBinding } from './events-binding';

describe('EventsBinding', () => {
  let component: EventsBinding;
  let fixture: ComponentFixture<EventsBinding>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventsBinding],
    }).compileComponents();

    fixture = TestBed.createComponent(EventsBinding);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
