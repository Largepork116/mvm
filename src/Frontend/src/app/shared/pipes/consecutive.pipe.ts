import { Pipe, PipeTransform } from '@angular/core';
import { IDocument } from '../../interfaces/document.interface';

@Pipe({
  name: 'consecutive'
})
export class ConsecutivePipe implements PipeTransform {

  transform(value: IDocument, args?: any): string {

    var consecutiveLength = 8;

    var consecutive = '';

    if(value.externalFile) {
      consecutive = '' + value.externalFileId
    }

    if(value.internalFile) {
      consecutive = '' + value.internalFileId
    }

    while (consecutive.length < consecutiveLength) {
      consecutive = '0' + consecutive;
    }

    return `${value.type}${consecutive}`;
  }

}
