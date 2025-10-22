import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemDetailsDialog } from './system-details-dialog';

describe('SystemDetailsDialog', () => {
  let component: SystemDetailsDialog;
  let fixture: ComponentFixture<SystemDetailsDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemDetailsDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SystemDetailsDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
