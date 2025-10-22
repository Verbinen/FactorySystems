import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ISystemPayload } from '../../models/system-payload.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-table',
  imports: [
    TableModule,
    ButtonModule,
    RippleModule,
    ToastModule
  ],
  templateUrl: './table.html',
  styleUrl: './table.scss'
})
export class Table {
  @Input() data: ISystemPayload[] = [];

  @Output() deleteData = new EventEmitter();
  @Output() viewData = new EventEmitter();

  private router = inject(Router);

  editProduct(item: ISystemPayload) {
    this.router.navigate(['./edit', item.id]);
  }

  onDelete(item: ISystemPayload) {
    this.deleteData.emit(item.id);
  }

  onViewData(item: ISystemPayload) {
    this.viewData.emit(item.id);
  }
}
