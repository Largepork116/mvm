import { FormControl } from "@angular/forms";


export class FormValidators {

  static PhoneNumberValidator(control: FormControl) {

    let regExp = /^[3][0-3][0-9]{8}$/;

    if (!regExp.test(control.value)) {
      return { "validPhoneNumber": true };
    }
    return null;
  }
}
