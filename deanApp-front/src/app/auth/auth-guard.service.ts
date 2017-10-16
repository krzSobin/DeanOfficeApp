import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, Router } from '@angular/router';
import { AuthService } from './auth.service'

@Injectable()
export class AuthGuard implements CanActivateChild {

	constructor(private router: Router, private auth: AuthService) { }

	canActivateChild() {
		if (this.auth.checkAuth()) {
			return true
		}
		this.router.navigate(['/login'])
		return false
	}
}
