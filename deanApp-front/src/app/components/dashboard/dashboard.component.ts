import { Component } from '@angular/core'
import { HttpClient } from '@angular/common/http'

import { AuthService } from '../../auth/auth.service'

@Component({
	selector: 'dashboard-component',
	templateUrl: './dashboard.component.html',
	styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent {
	constructor(private auth: AuthService, private http: HttpClient) { }

	logout(): void {
		this.auth.logout()
	}

	sendRequest(): void {
		console.log('sended')
		this.http.get('https://demo7717529.mockable.io/').subscribe()
	}
}
