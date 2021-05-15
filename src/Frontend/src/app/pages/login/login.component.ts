import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '@services/api/auth.service';
import { NotificationService } from '../../services/others/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  isSubmitted: boolean;

  constructor(private router: Router, 
    private notificationService: NotificationService,
    private formBuilder: FormBuilder,
    private authService: AuthService) {

      this.loginForm = this.formBuilder.group({
        username: ['admin@app.com', Validators.required],
        password: ['P4$$w0rd', Validators.required]
      })
  }

  errorMessages = {
    username: [
      { type: "required", message: "Required field" }
    ],
    password: [
      { type: "required", message: "Required field" }
    ]
  };

  get username(): FormControl {
    return this.loginForm.get("username") as FormControl;
  }

  get password(): FormControl {
    return this.loginForm.get("password") as FormControl;
  }

  ngOnInit(): void {
  }

  login() {
    this.isSubmitted = true;

    if(!this.loginForm.valid) {
      return;
    }

    this.authService.login( this.username.value, this.password.value)
      .subscribe(response => {
        this.authService.setProfile(response.data);

        this.router.navigateByUrl('/auth');


        console.log("login => ", response);
      })
  }

}

export const ERRORS_CONST = {
  LOGIN: {
    ERROR: 'Usuario o contraseña no válida'
  }
}










