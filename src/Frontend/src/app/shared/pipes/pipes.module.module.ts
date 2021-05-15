import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RemoveAdminUserPipe } from './remove-admin-user.pipe';
import { ConsecutivePipe } from './consecutive.pipe';
import { RemoveAdminPersonPipe } from './remove-admin-person.pipe';
import { OnlyInternalPeoplePipe } from './only-internal-people.pipe';
import { LogDataChangesPipe } from './log-data-changes.pipe';


let components = [
  RemoveAdminUserPipe,
  ConsecutivePipe,
  RemoveAdminPersonPipe,
  OnlyInternalPeoplePipe,
  LogDataChangesPipe
]

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [	...components  ],
  exports: [	...components  ]
})

export class PipesModule { }

