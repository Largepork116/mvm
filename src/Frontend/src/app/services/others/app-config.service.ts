import { Injectable } from '@angular/core';
import { NgxRolesService } from 'ngx-permissions';

import { AuthService } from '@services/api/auth.service';
import { ThemeService } from './theme.service';

@Injectable()
export class AppConfigService {

  constructor(private authService: AuthService, 
    private rolesService: NgxRolesService) {}

  load() {
    this.rolesService  
    .addRoles({
      'User': () => {
        return this.authService.isUser;
      },    
      'DocumentManager': () => {
        return this.authService.isDocumentManager;
      },   
      'SuperAdmin': () => {
        return this.authService.isAdmin;
      }
    });  
  }

}
