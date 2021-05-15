import { Pipe, PipeTransform } from '@angular/core';
import { IPerson } from '../../interfaces/person.interface';

@Pipe({
  name: 'removeAdminPerson'
})
export class RemoveAdminPersonPipe implements PipeTransform {

  transform(value: IPerson[], args?: any): IPerson[] {
    if(!value) {
      return [];
    }

    let array = value.filter(x=> x.name != "Admin")
    return array;
  }

}
