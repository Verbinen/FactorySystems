import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: "",
    loadComponent: () => import("../app/features/system-list/system-list").then(c => c.SystemList)
  }
];
