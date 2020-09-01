import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth.service';
import { tap } from 'rxjs/operators';
import { BuiltinType } from '@angular/compiler';
import { MessagingService } from './services/messaging.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService) {

  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    // If the user hasnt logged in yet and for the main page, pass through since the adoptions page does not need a login.
    // This only applies to the adoptions route since this is the default route for the app
    return new Promise((resolve, reject) =>{
      this.auth.isAuthenticated$.pipe(
        tap(loggedIn => {
          if (!loggedIn) {
            if (state.url !== '/adoptions') {
              this.auth.login(state.url);
            }
          }
        })
      ).subscribe((result) =>{
        if (result || state.url == '/adoptions'){
          resolve(true);
        }else{
          resolve(result);
        }
      });
    });
  }
}
