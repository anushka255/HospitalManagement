import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  // @ts-ignore
  constructor (private accountSservice: AccountService, private toastr: ToastrService)
  canActivate(): Observable<boolean> {
    return this.accountSservice.currentUser$.pipe(
      map(user => {
        if (user) return true;
        this.toastr.error('You shall not pass!')
      })
    );
  }

}
