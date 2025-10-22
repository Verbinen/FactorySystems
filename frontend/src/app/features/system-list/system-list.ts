import { Component, inject } from '@angular/core';
import { FactorySystemsService } from '../../core/services/factory-systems.service';
import { ISystemPayload } from '../../shared/models/system-payload.interface';
import { TitlePage } from "../../shared/components/title-page/title-page";
import { Table } from "../../shared/components/table/table";
import { SystemDetailsDialog } from '../../shared/components/system-details-dialog/system-details-dialog';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-system-list',
  imports: [
    TitlePage,
    Table,
    SystemDetailsDialog
  ],
  templateUrl: './system-list.html',
  styleUrl: './system-list.scss'
})
export class SystemList {
  private factorySystemsService = inject(FactorySystemsService)
  private messageService = inject(MessageService);

  data: ISystemPayload[] = []
  showDetailsDialog = false;
  selectedSystem!: ISystemPayload;

  ngOnInit() {
    this.listAllSystems()
  }

  openDetails(id: string) {
    this.loadSystemData(id);
    this.showDetailsDialog = true;
  }

  listAllSystems() {
    this.factorySystemsService.getAllSystems().subscribe({
      next: (response: ISystemPayload[]) => {
        this.data = response
      }
    })
  }

  loadSystemData(id: string) {
    this.factorySystemsService.getSystemById(id).subscribe({
      next: (response: ISystemPayload) => {
        this.selectedSystem = response;
      }
    })
  }

  handleDelete(id: string) {
    console.log(id)
    this.factorySystemsService.deleteSystem(id).subscribe({
      next: () => {
        this.data = this.data.filter((data: ISystemPayload) => data.id !== id);
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'System deleted successfully!',
          life: 3000
        });
      },
      error: (error) => console.error(error)
    })
  }
}
