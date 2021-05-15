import { Pipe, PipeTransform } from '@angular/core';
import { IPerson } from '../../interfaces/person.interface';

@Pipe({
  name: 'onlyInternalPeople'
})
export class OnlyInternalPeoplePipe implements PipeTransform {

  transform(value: IPerson[], args?: any): IPerson[] {

    if(!value) {
      return [];
    }

    let array = value.filter(x=> x.user)
    return array;
  }

}
