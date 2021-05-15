import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { DocumentManagerComponent } from './documents-manager/document-manager.component';
import { MyDocumentsComponent } from './my-documents/my-documents.component';
import { UsersComponent } from './users/users.component';
import { LogDataChangeComponent } from './log-data-change/log-data-change.component';



const routes: Routes = [
  {
    path: 'document-manager',
    component: DocumentManagerComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ['DocumentManager', 'SuperAdmin'],
        redirectTo: '/auth'
      }
    }
  },
  {
    path: 'my-documents',
    component: MyDocumentsComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ['User', 'SuperAdmin'],
        redirectTo: '/auth'
      }
    }
  },
  {
    path: 'users',
    component: UsersComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ['SuperAdmin'],
        redirectTo: '/auth'
      }
    }
  },
  {
    path: 'logs',
    component: LogDataChangeComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ['SuperAdmin'],
        redirectTo: '/auth'
      }
    }
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
