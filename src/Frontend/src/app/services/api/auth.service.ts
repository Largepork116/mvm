import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

import { AppHttpServiceResponse } from '@classes/AppHttpServiceResponse.class';
import { environment } from '@environments/environment';
import { IProfile } from '@interfaces/profile.interface';
import { IAppHttpResponse, ITrackHttpError } from '@interfaces/app-http-response.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends AppHttpServiceResponse {

  private _profile: BehaviorSubject<IProfile>;
  private PROFILE_KEY = "davidMoralesApp"

  constructor(protected http:HttpClient) { 
    super(http)
      this._profile = new BehaviorSubject( this.getProfile() );
  }

  get profile(): IProfile {
    return this._profile.value;
  }

  get isAdmin(): boolean {
    return this._profile.value.role == "SuperAdmin";
  }

  get isUser(): boolean {
    return this._profile.value.role == "User";
  }

  get isDocumentManager(): boolean {
    return this._profile.value.role == "DocumentManager";
  }

  login( username: string, password:string) : Observable<IAppHttpResponse<IProfile> | ITrackHttpError>{
    var url = `${environment.baseUrl}/authentication`;
    var data = { username,  password }

    return this.http
      .post<IAppHttpResponse<IProfile>>(url, data)
      .pipe(catchError((error) => this.handleHttpError(error)));
  }

  logout() {
    this.removeProfile();
  }

  public setProfile(profile: IProfile){
    this._profile.next(profile);
    localStorage.setItem(this.PROFILE_KEY, JSON.stringify(profile));
  }

  private getProfile(): IProfile {
    return JSON.parse(localStorage.getItem(this.PROFILE_KEY));
  }

  private removeProfile(): void {
    this._profile.next(null);
    localStorage.removeItem(this.PROFILE_KEY);
  }

}
