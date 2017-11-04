import { Component } from '@angular/core';
import { AuthService } from '../../auth/auth.service'

@Component({
	selector: 'admin-component',
	template: `<h1>Admin dashboard</h1><button (click)="logout()" id="logout">logout</button>
	<a routerLink="/admin/students/">Studenci</a>

	<router-outlet></router-outlet>`,
})

export class AdminComponent {
	constructor(private auth: AuthService) { }

	logout(): void {
		this.auth.logout()
	}
}
