import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service'

@Component({
	selector: 'login',
	templateUrl: './login.component.html',
})

export class LoginComponent {
	constructor(private auth: AuthService) { }

	login(formData: any): void {
		console.log(formData)
		this.auth.login(formData)
	}
}
