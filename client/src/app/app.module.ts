import { NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import{HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import {FormControl, FormsModule, NgControl, ReactiveFormsModule} from '@angular/forms'
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import {ToastrModule} from 'ngx-toastr';
import { SharedModule } from './_modules/shared.module';
import { CommonModule } from '@angular/common'
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MembersCardComponent } from './members/members-card/members-card.component';
import {JwtInterceptor} from "./_interceptors/jwt.interceptor";
import { MemberEditsComponent } from './members/member-edits/member-edits.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MessagesComponent } from './messages/messages.component';
import { GalleryModule } from  'ng-gallery';
import { NgxSpinnerModule } from "ngx-spinner";
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { DateInputComponent } from './_forms/date-input/date-input.component';
import { ListComponent } from './list/list.component';
import { OthersEditComponent } from './members/others-edit/others-edit.component';
import { LearnMoreComponent } from './learn-more/learn-more.component';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { TableComponent } from './table/table.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { LoginhomeComponent } from './loginhome/loginhome.component';
import { MembersTableComponent } from './members/members-table/members-table.component';
import { EmployeeDetailsComponent } from './members/employee-details/employee-details.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    DateInputComponent,
    TextInputComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MembersCardComponent,
    MemberEditsComponent,
    ListComponent,
    MessagesComponent,
    OthersEditComponent,
    LearnMoreComponent,
    PhotoEditorComponent,
    TableComponent,
    LoginhomeComponent,
    MembersTableComponent,
    EmployeeDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgbModule,
    NgxSpinnerModule,
    CommonModule,
    GalleryModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass:ErrorInterceptor, multi:true},
    {provide: HTTP_INTERCEPTORS, useClass:JwtInterceptor, multi:true},
    {provide: HTTP_INTERCEPTORS, useClass:LoadingInterceptor, multi:true}

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
