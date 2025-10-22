import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: "",
    loadComponent: () => import("../app/features/system-list/system-list").then(c => c.SystemList)
  },
  {
    path: "register",
    loadComponent: () => import("../app/features/system-register/system-register").then(c => c.SystemRegister)
  },
  {
    path: "edit/:id",
    loadComponent: () => import("../app/features/system-register/system-register").then(c => c.SystemRegister)
  }
];
