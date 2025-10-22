import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemRegister } from './system-register';

describe('SystemRegister', () => {
  let component: SystemRegister;
  let fixture: ComponentFixture<SystemRegister>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemRegister]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SystemRegister);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
