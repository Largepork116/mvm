import { Component, OnInit, Output } from '@angular/core';

import { AuthService } from '@services/api/auth.service';
import { IProfile } from '@interfaces/profile.interface';
import { NAVIGATION_MENU } from '@data/navigation-menu.const';
import { INavigationMenu } from 'src/app/interfaces/navigation-menu.interface';
import { EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  @Output() showMenu = new EventEmitter<any>();
  profile: IProfile;
  menu: INavigationMenu[] = NAVIGATION_MENU;

  constructor(private authService: AuthService,
    private router: Router) { 
    this.profile = authService.profile;
  }

  ngOnInit(): void {
  }

  logout(): void {
    this.authService.logout();
    this.router.navigateByUrl("login");
  }

}
