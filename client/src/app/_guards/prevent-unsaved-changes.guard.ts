import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditsComponent } from '../members/member-edits/member-edits.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(component: MemberEditsComponent): boolean{
    if(component.editForm.dirty)
    {
      return confirm("Are you sure go back?");
    }
    return true;
  }

}
