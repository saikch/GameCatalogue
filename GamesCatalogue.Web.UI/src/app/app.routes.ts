import { Routes } from '@angular/router';
import { GamesListComponent } from './features/games/pages/games-list/games-list.component';
import { GamesEditComponent } from './features/games/pages/games-edit/games-edit.component';


export const routes: Routes = [
  { path: '', redirectTo: 'games', pathMatch: 'full' },
  { path: 'games', component: GamesListComponent },
  { path: 'games/create', component: GamesEditComponent },
  { path: 'games/edit/:id', component: GamesEditComponent },
  { path: '**', redirectTo: 'games' }
];
