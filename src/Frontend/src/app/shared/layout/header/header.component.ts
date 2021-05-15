import { EventEmitter } from '@angular/core';
import { Component, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '@services/api/auth.service';
import { IProfile } from '@interfaces/profile.interface';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() showMenu = new EventEmitter<any>();
  profile: IProfile;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.profile = this.authService.profile;
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl("login");
  }

}
