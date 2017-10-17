import { Component } from '@angular/core';
import { AuthService } from '../../auth/auth.service'

@Component({
	selector: 'admin-component',
	template: '<h1>Admin dashboard</h1><router-outlet></router-outlet><button (click)="logout()" id="logout">logout</button>',
})

export class AdminComponent {
	constructor(private auth: AuthService) { }

	logout(): void {
		this.auth.logout()
	}
}
