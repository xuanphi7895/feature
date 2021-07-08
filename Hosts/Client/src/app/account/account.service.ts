import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { IUser } from './../shared/models/user';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<IUser>(null);
  currentUser$ = this.currentUserSource.asObservable();


  constructor(private http: HttpClient, private routes : Router) {}

   login(value: any){
     return this.http.post<IUser>(this.baseUrl + 'account/login', value).pipe(
       map((user: IUser) => {
         if (user){
           localStorage.setItem('token', user.token);
           this.currentUserSource.next(user);
         }
       })
     );
   }

   logout(){
     localStorage.removeItem('token');
     this.currentUserSource.next(null);
     this.routes.navigateByUrl('/');
   }
}
