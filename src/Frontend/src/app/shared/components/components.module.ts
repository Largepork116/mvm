import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NightDaySelectorComponent } from './night-day-selector/night-day-selector.component';

var components = [ NightDaySelectorComponent ]

@NgModule({
  declarations: [ ...components ],
  imports: [
    CommonModule
  ],
  exports: [ ...components ]
})
export class ComponentsModule { }
