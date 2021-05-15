import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'logDataChanges'
})
export class LogDataChangesPipe implements PipeTransform {

  transform(value: string, args?: any): string[] {
    let paragraphs: string[] = [];

    if (!value) return paragraphs;

    if (value?.includes("\n")) {
      paragraphs = value.split("\n");
    } else {
      paragraphs.push(value);
    }

    return paragraphs;
  }

}
