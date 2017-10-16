import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'login',
	template: '<button (click)="login()" id="login">login</button><p>login</p>',
})

export class LoginComponent {
	constructor(private router: Router) { }

	login(): void {
		window.localStorage.setItem('token', 'xd')
        this.router.navigate(['/dashboard'])
	}
}
