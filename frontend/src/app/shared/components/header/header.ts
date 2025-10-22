import { Component } from '@angular/core';
import { MenubarModule } from 'primeng/menubar';
import { CardModule } from 'primeng/card';
@Component({
  selector: 'app-header',
  imports: [MenubarModule,CardModule],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
  items: any[] = [];

  ngOnInit() {
    this.items = [
      {
        label: 'Application List',
        icon: 'pi pi-list',
        routerLink: ['/']
      },
      {
        label: 'New Application',
        icon: 'pi pi-plus',
        routerLink: ['/register']
      }
    ];
  }
}
