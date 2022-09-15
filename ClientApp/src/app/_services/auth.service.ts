import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { BehaviorSubject, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'account/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser!: User;
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  
  constructor(private http: HttpClient) { }

  changeMemberPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user.user));
          this.setCurrentUser(user);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user.user;       
        }
      })
    );
  }
  

  setCurrentUser(user: User) {
    
    const roles = this.getDecodedToken(user.token).role;
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token!);
  }

  getDecodedToken(token:any) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
