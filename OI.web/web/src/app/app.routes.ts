import { Routes } from '@angular/router';
import {SearchComponent} from './components/search/search.component';
import {DetailComponent} from './components/detail/detail.component';
import {ImportComponent} from './components/import/import.component';

export const routes: Routes = [
  {
    path: "",
    component: SearchComponent
  },
  {
    path: "details/:id",
    component: DetailComponent
  },
  {
    path: "import",
    component: ImportComponent
  }
];
