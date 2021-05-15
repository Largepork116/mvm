import { Component, OnInit } from '@angular/core';
import { ThemeService, ThemesEnum } from '@services/others/theme.service';

@Component({
  selector: 'app-night-day-selector',
  templateUrl: './night-day-selector.component.html',
  styleUrls: ['./night-day-selector.component.scss']
})
export class NightDaySelectorComponent implements OnInit {

  constructor(public themeService: ThemeService) { 
  }

  ngOnInit() {
  }

  onClick ():void {
    if(this.themeService.theme == ThemesEnum.Dark) {
      this.themeService.setLight()
    } else {
      this.themeService.setDark()
    }
  }

}
