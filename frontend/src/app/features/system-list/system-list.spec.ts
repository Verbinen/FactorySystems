import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemList } from './system-list';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { MessageService } from 'primeng/api';

describe('SystemList', () => {
  let component: SystemList;
  let fixture: ComponentFixture<SystemList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemList],
      providers: [HttpClient, HttpHandler, MessageService]
    }).compileComponents();

    fixture = TestBed.createComponent(SystemList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
