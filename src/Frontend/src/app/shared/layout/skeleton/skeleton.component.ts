import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../../../services/others/theme.service';

@Component({
  selector: 'app-skeleton',
  templateUrl: './skeleton.component.html',
  styleUrls: ['./skeleton.component.scss']
})
export class SkeletonComponent implements OnInit {

  public showLeftNav = true;
  //public $theme : "dark" | 'light' = "dark";
  //public theme : "dark" | 'light' = "dark";

  constructor(public themeService: ThemeService) { }

  ngOnInit() {
  }

  showMenu() {
    this.showLeftNav = !this.showLeftNav;
  }

}
