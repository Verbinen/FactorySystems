import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemRegister } from './system-register';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { RouterTestingModule } from '@angular/router/testing';

describe('SystemRegister', () => {
  let component: SystemRegister;
  let fixture: ComponentFixture<SystemRegister>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemRegister, RouterTestingModule],
      providers: [HttpClient, HttpHandler, MessageService]
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
