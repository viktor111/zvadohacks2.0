import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BlogComponent } from './blog/blog.component';
import { HacksComponent } from './hacks/hacks.component';
import { SnippetsComponent } from './snippets/snippets.component';
import { ManageComponent } from './manage/manage.component';

const routes: Routes = [
{ path: '', redirectTo: '/home', pathMatch: 'full' },
{ path: 'blog', component: BlogComponent },
{ path: 'hacks', component: HacksComponent },
{ path: 'snippets', component: SnippetsComponent },
{ path: 'manage', component: ManageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
