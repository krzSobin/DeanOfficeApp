import { Injectable } from '@angular/core'
import { Router } from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { Headers, Response } from '@angular/http'

import { Subject } from 'rxjs/Subject'
import { Observable } from 'rxjs/Observable'

import { User } from '../models/User'

import 'rxjs/add/operator/map'
import 'rxjs/add/operator/toPromise'

@Injectable()
export class AuthService {

	private url: string = ''
	private token: string
	private loggedInSource: Subject<string> = new Subject<string>()

	loggedIn$ = this.loggedInSource.asObservable()

	constructor(private http: HttpClient, private router: Router) { }

	passUserName(name: string) {
		this.loggedInSource.next(name)
	}

	login(formData): Promise<any> {
		let string = ''
		Object.entries(formData).forEach(([key, value]) => {
			string += `&${key}=${value}`
		})
		return this.http.post('token', string.slice(1))
			.toPromise()
			.then((res: any) => {
				const name = res.userName
				window.localStorage.setItem('token', res.access_token)
				window.localStorage.setItem('role', res.role)
				window.localStorage.setItem('name', name)
				this.passUserName(name)

				this.router.navigate([res.role])
			})
			.catch((err: any) => {
				console.error(err.error)
				return Promise.reject(err)
			})
	}

	getRole(): string | null {
		return window.localStorage.getItem('role')
	}

	getAccessToken(): string | null {
		return window.localStorage.getItem('token')
	}

	logout(): void {
		window.localStorage.removeItem('token')
		window.localStorage.removeItem('role')
		window.localStorage.removeItem('name')
		this.router.navigate(['/login']);
	}

	checkAuth(): boolean {
		return !!window.localStorage.getItem('token')
	}

	changePassword(data): Promise<any> {
		return this.http.post('api/Account/ChangePassword', data, { responseType: "text" })
			.toPromise()
	}
}
