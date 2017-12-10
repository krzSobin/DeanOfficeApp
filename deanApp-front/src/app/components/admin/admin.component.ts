import { Component } from '@angular/core'
import { Router } from '@angular/router'

import { MatCardModule } from '@angular/material'
import { MatRipple } from '@angular/material'

@Component({
	selector: 'admin-component',
	templateUrl: './admin.component.html'
})

export class AdminComponent {
	public router: Router

	constructor(private _router: Router) {
		this.router = _router
	}
}
