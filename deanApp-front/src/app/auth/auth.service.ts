import { Injectable } from '@angular/core'
import { Router } from '@angular/router'
import { Http, Headers, Response } from '@angular/http'
import { Observable } from 'rxjs/Observable';
import { User } from '../models/User'

import * as mock from './backend-response'

import 'rxjs/add/operator/map'

@Injectable()
export class AuthService {

	private url: string = ''
	private token: string

	constructor(private http: Http, private router: Router) { }

	login(formData) {
		// return this.http.post(url, { UserName: username, Password: password, grant_type: 'password' })
		// .map((res: Response) => {
		this.token = mock.data.access_token
		const role: string = mock.data.role
		window.localStorage.setItem('token', this.token)
		window.localStorage.setItem('role', role)
		const redirectUrl: string = (role === 'admin') ? '/admin' : '/dashboard'
		this.router.navigate([redirectUrl])
		// })
	}

	getRole(): string | null {
		return window.localStorage.getItem('role')
	}

	getAccessToken(): string {
		return this.token
	}

	logout(): void {
		window.localStorage.removeItem('token')
		window.localStorage.removeItem('role')
		this.router.navigate(['/login']);
	}

	checkAuth(): boolean {
		if (window.localStorage.getItem('token')) {
			return true
		}
		return false
	}
}
