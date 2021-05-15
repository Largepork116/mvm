import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';


import { NgxPermissionsModule } from 'ngx-permissions';
import { SkeletonComponent } from './skeleton/skeleton.component';
import { HeaderComponent } from './header/header.component';
import { NavigationComponent } from './navigation/navigation.component';
import { ComponentsModule } from '../components/components.module';
import { FooterComponent } from './footer/footer.component';

var components = [ HeaderComponent, NavigationComponent, SkeletonComponent, FooterComponent ]

@NgModule({
  declarations: [ ...components ],
  imports: [
    CommonModule,
    RouterModule,
    ComponentsModule,
    NgxPermissionsModule
  ],
  exports: [ ...components ]
})
export class LayoutModule { }
