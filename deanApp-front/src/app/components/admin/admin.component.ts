import { Component } from '@angular/core';
import { AuthService } from '../../auth/auth.service'

import { MatCardModule } from '@angular/material'
import { MatRipple } from '@angular/material'

@Component({
	selector: 'admin-component',
	templateUrl: './admin.component.html',
	styleUrls: ['./admin.component.scss']
})

export class AdminComponent {
	constructor(private auth: AuthService) { }

	logout(): void {
		this.auth.logout()
	}
}
