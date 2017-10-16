import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service'

@Component({
	selector: 'login',
	template: '<button (click)="login()" id="login">login</button><p>login</p>',
})

export class LoginComponent {
	constructor(private auth: AuthService) { }

	login(): void {
		this.auth.login('aa', 'bb')
	}
}
