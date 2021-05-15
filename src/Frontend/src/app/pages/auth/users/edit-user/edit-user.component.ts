import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { NotificationService } from '@app/services/others/notification.service';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '@services/api/user.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IUser } from '@interfaces/user.interface';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {

  userForm: FormGroup;
  isSubmitted: boolean;
  roles: any = [];
  subscriptions = new Subject();
  @Input() edit: IUser;

  constructor(public activeModal: NgbActiveModal, 
    private formBuilder: FormBuilder,
    private userService: UserService,
    private notificationService: NotificationService,
    private modalService: NgbModal) { 

    this.userForm = this.formBuilder.group({
        name: ['', Validators.required],
        email: ['', [Validators.required, Validators.email] ],
        role: ['', Validators.required]
      })
  }

  errorMessages = {
    name: [
      { type: "required", message: "Campo requerido" }
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
          this.userForm.patchValue(this.edit);
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

    let user: any = this.userForm.value;

    console.log("SEND =>", user);

    this.userService.update(user)
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
