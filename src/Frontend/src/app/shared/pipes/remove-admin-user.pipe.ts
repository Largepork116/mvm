import { Pipe, PipeTransform } from '@angular/core';
import { IUser } from '@app/interfaces/user.interface';

@Pipe({
  name: 'removeAdminUser'
})
export class RemoveAdminUserPipe implements PipeTransform {

  transform(value: IUser[], args?: any): IUser[] {

    if(!value) {
      return [];
    }

    let array = value.filter(x=> x.name != "Admin")
    return array;
  }

}
