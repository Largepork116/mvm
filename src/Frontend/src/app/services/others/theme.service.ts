import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  private THEME_KEY = 'davidMoralesTheme'
  private _theme: BehaviorSubject<string>;

  constructor() {
    let theme = localStorage.getItem(this.THEME_KEY);
    if(!theme) {
      this._theme = new BehaviorSubject( ThemesEnum.Light );
    }
    this._theme = new BehaviorSubject( theme );
  }

  setDark (): void {
    this._theme.next(ThemesEnum.Dark);
    localStorage.setItem(this.THEME_KEY, ThemesEnum.Dark);
  }

  setLight (): void {
    this._theme.next(ThemesEnum.Light);
    localStorage.setItem(this.THEME_KEY, ThemesEnum.Light);
  }

  get theme() {
    if(!this._theme.value) {
      this.setLight();
    }
    return this._theme.value;
  }
}


export enum ThemesEnum {
  Dark = 'dark',
  Light = 'light'
}
