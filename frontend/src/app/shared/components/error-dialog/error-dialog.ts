import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

export interface IErrorResponse {
  statusCode: number;
  errorMessage: string;
}

@Component({
  selector: 'app-error-dialog',
  imports: [
    DialogModule,
    CommonModule,
    ButtonModule
  ],
  templateUrl: './error-dialog.html',
  styleUrl: './error-dialog.scss'
})
export class ErrorDialog {
  @Input() visible: boolean = false;
  @Input() error?: IErrorResponse;
  @Output() visibleChange = new EventEmitter<boolean>();

  onClose() {
    this.visible = false;
    this.visibleChange.emit(false);
  }
}
