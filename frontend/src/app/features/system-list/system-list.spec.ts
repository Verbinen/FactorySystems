import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemList } from './system-list';

describe('SystemList', () => {
  let component: SystemList;
  let fixture: ComponentFixture<SystemList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemList],
    }).compileComponents();

    fixture = TestBed.createComponent(SystemList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
