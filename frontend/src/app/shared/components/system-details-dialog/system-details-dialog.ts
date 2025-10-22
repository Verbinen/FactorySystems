import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISystemPayload } from '../../models/system-payload.interface';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-system-details-dialog',
  imports: [
    DialogModule,
    CommonModule,
    ButtonModule
  ],
  templateUrl: './system-details-dialog.html',
  styleUrl: './system-details-dialog.scss'
})
export class SystemDetailsDialog {
  @Input() visible: boolean = false;
  @Input() system?: ISystemPayload;
  @Output() visibleChange = new EventEmitter<boolean>();

  onClose() {
    this.visible = false;
    this.visibleChange.emit(false);
  }
}
