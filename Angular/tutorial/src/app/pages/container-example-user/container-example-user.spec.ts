import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContainerExampleUser } from './container-example-user';

describe('ContainerExampleUser', () => {
  let component: ContainerExampleUser;
  let fixture: ComponentFixture<ContainerExampleUser>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContainerExampleUser],
    }).compileComponents();

    fixture = TestBed.createComponent(ContainerExampleUser);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
