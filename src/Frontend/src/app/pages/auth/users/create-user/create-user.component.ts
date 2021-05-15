import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormValidators } from '@app/core/validators/form-validators';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { NotificationService } from '@app/services/others/notification.service';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '@services/api/user.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IUser } from '../../../../interfaces/user.interface';
import { IPerson } from '../../../../interfaces/person.interface';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit {

  userForm: FormGroup;
  isSubmitted: boolean;
  roles: any = [];
  subscriptions = new Subject();

  constructor(public activeModal: NgbActiveModal, 
    private formBuilder: FormBuilder,
    private userService: UserService,
    private notificationService: NotificationService,
    private modalService: NgbModal) { 

    this.userForm = this.formBuilder.group({
        name: ['', Validators.required],
        lastName: ['', Validators.required],
        phone: ['', [
          Validators.required,
          FormValidators.PhoneNumberValidator,
        ]],
        email: ['', [
          Validators.required, 
          Validators.email
        ]],
        role: ['', Validators.required],
      })
  }

  errorMessages = {
    name: [
      { type: "required", message: "Campo requerido" }
    ],
    lastName: [
      { type: "required", message: "Campo requerido" }
    ],
    phone: [
      { type: "required", message: "Campo requerido" },
      {
        type: "validPhoneNumber",
        message: "Verifique que el número de celular ingresado sea correcto.",
      }
    ],
    email: [
      { type: "required", message: "Campo requerido" },
      { type: "email", message: "Formato no valido" }
    ],
    role: [
      { type: "required", message: "Campo requerido" }
    ]
  };

  get name(): FormControl {
    return this.userForm.get("name") as FormControl;
  }

  get lastName(): FormControl {
    return this.userForm.get("lastName") as FormControl;
  }

  get phone(): FormControl {
    return this.userForm.get("phone") as FormControl;
  }

  get email(): FormControl {
    return this.userForm.get("email") as FormControl;
  }

  get role(): FormControl {
    return this.userForm.get("role") as FormControl;
  }

  ngOnInit() {
    this.getRoles();
  }

  getRoles(): void {
    this.roles = null;
    this.userService.getRoles()
      .pipe(takeUntil(this.subscriptions))
      .subscribe(
        (response: IAppHttpResponse<any[]>) => {
          this.roles = response.data;
          console.log("DATA => ", this.roles);
        },
        (error: ITrackHttpError) => {
          console.log("ERROR => ", error);
        }
      );
  }

  onSubmit(){
    this.isSubmitted = true;
    console.log("submit => ", this.userForm)

    if(!this.userForm.valid) {
      return;
    }

    let person: IPerson = {};
    person.name = this.name.value;
    person.lastName = this.lastName.value;
    person.phone = this.phone.value;
    person.companyId = 1;

    let user: IUser = {};
    user.email = this.email.value;
    user.role = this.role.value;
    user.password = "P4$$w0rd";
    user.person = person;

    console.log("SEND =>", user);

    this.userService.create(user)
      .subscribe(
        (response: IAppHttpResponse<any>) => {
        this.notificationService.toast("La creación del usuario finalizó exitosamente", "success");
          this.activeModal.dismiss();
        },
        (error: ITrackHttpError) => {
          console.log("submit error");
          console.log(error);
        }
      )
  }

}
