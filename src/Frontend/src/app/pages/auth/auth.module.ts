import { NgModule } from '@angular/core';

import { AuthRoutingModule } from '@auth/auth-routing.module';
import { AuthComponent } from '@auth/auth.component';

import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from '@shared/layout/layout.module';
import { UsersComponent } from './users/users.component';
import { TableModule } from 'ngx-easy-table';
import { CreateUserComponent } from './users/create-user/create-user.component';
import { EditUserComponent } from './users/edit-user/edit-user.component';
import { PipesModule } from '../../shared/pipes/pipes.module.module';
import { DocumentManagerComponent } from './documents-manager/document-manager.component';
import { CreateDocumentComponent } from './documents-manager/create-document/create-document.component';
import { MyDocumentsComponent } from './my-documents/my-documents.component';
import { LogDataChangeComponent } from './log-data-change/log-data-change.component';

@NgModule({
  declarations: [
    AuthComponent,
    UsersComponent,
    CreateUserComponent,
    EditUserComponent,
    DocumentManagerComponent,
    CreateDocumentComponent,
    MyDocumentsComponent,
    UsersComponent,
    CreateUserComponent,
    EditUserComponent,
    LogDataChangeComponent
  ],
  entryComponents: [
    CreateUserComponent,
    EditUserComponent,
    CreateDocumentComponent,
    CreateUserComponent,
    EditUserComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AuthRoutingModule,
    LayoutModule,
    TableModule,
    PipesModule
  ],
  bootstrap: [AuthComponent]
})
export class AuthModule { }
