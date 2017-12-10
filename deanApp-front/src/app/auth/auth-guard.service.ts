import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from './auth.service'

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {

	constructor(private router: Router, private auth: AuthService) { }

	canActivate(route: ActivatedRouteSnapshot): boolean {
		if (this.auth.checkAuth() && route.data.expectedRole === this.auth.getRole()) {
			return true
		}
		this.router.navigate(['/login'])
		return false
	}

	canActivateChild(route: ActivatedRouteSnapshot): boolean {
		if (this.auth.checkAuth() && route.parent.data.expectedRole === this.auth.getRole()) {
			return true
		}
		this.router.navigate(['/login'])
		return false
	}
}
