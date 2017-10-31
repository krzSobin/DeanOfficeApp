import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service'

@Component({
	selector: 'login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss']
})

export class LoginComponent {
	constructor(private auth: AuthService) { }

	login(form: any): void {
		form.valid && this.auth.login(form.value)
	}
}