import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
// This guard will prompt user , when user switch outside edit user page , we need to add this componenet with edit componenet with memeber edit path in appmodule.tss
@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(component: MemberEditComponent,): boolean {
    if(component.editForm.dirty)
    {
      return confirm('Are you sure you want to continue? unsaved changes will be lost');
    }
    return true;
  }
  
}
