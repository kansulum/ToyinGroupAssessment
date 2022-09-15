import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { TaskComponent } from './task/task.component';
import { AuthGuard } from './_guards/auth.guard';

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', canActivate:[AuthGuard], component: FetchDataComponent },
      { path: 'todo', canActivate:[AuthGuard], component: TaskComponent },
    ])
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }


