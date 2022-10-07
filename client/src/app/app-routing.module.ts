import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListComponent } from './list/list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberEditsComponent } from './members/member-edits/member-edits.component';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { TableComponent } from './table/table.component';
import { OthersEditComponent } from './members/others-edit/others-edit.component';
import { LearnMoreComponent } from './learn-more/learn-more.component';
import { MemberSelectEditsComponent } from './members/member-select-edits/member-select-edits.component';
import { AccountService } from './_services/account.service';
import { BusyService } from './_services/busy.service';
import { MembersService } from './_services/members.service';
import { MessageService } from './_services/message.service';
import { LoginhomeComponent } from './loginhome/loginhome.component';
import { EmployeeDetailsComponent } from './members/employee-details/employee-details.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'members', component: MemberListComponent},
      {path: 'members/:username', component: MemberDetailComponent},
      {path: 'staff/:username', component: EmployeeDetailsComponent},
      {path: 'lists', component: ListComponent},
      {path: 'tables', component: TableComponent},
      {path:'member/edit/:username', component:MemberEditsComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      {path: 'message', component: MessagesComponent},
      {path:'member/selectedit/:username', component:MemberSelectEditsComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      {path: 'member/othersedit/:username', component: OthersEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      {path: 'learnmore', component: LearnMoreComponent},
      {path: 'loginhome', component: LoginhomeComponent}
    ]
  },
  {path: 'errors', component: TestErrorsComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AccountService, BusyService, MembersService, MessageService]
})
export class AppRoutingModule { }
