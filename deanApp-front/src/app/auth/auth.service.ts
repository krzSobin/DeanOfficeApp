import { Injectable } from '@angular/core'
import { Router } from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { Headers, Response } from '@angular/http'
import { Observable } from 'rxjs/Observable'
import { User } from '../models/User'

import * as mock from './backend-response'

import 'rxjs/add/operator/map'
import 'rxjs/add/operator/toPromise'

@Injectable()
export class AuthService {

	private url: string = ''
	private token: string

	constructor(private http: HttpClient, private router: Router) { }

	login(formData): Promise<void> {
		let string = ''
		Object.entries(formData).forEach(([key, value]) => {
			string += `&${ key }=${ value }`
		})
		return this.http.post('token', string.slice(1))
			.toPromise()
			.then((res: any) => {
				console.log(res)
				this.token = res.access_token
				const role: string = mock.data.role
				window.localStorage.setItem('token', this.token)
				window.localStorage.setItem('role', role)
				window.localStorage.setItem('name', res.userName)
				const redirectUrl: string = (role === 'admin') ? '/admin' : '/dashboard'
				this.router.navigate([redirectUrl])
			})
			.catch((err: any) => {
				console.error(err.error)
			})
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
