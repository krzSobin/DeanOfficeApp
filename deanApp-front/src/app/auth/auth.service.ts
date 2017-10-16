import { Injectable } from '@angular/core'
import { Router } from '@angular/router'
import { Http, Headers, Response } from '@angular/http'
import { Observable } from 'rxjs/Observable';
import { User } from '../models/User'
import 'rxjs/add/operator/map'

@Injectable()
export class AuthService {
	constructor(private http: Http, private router: Router) { }

	login(username: string, password: string) {
		// return this.http.post('', { UserName: username, Password: password, grant_type: 'password' })
			// .map((res: Response) => {
				const role: string = 'user'
				window.localStorage.setItem('token', 'xd')
				window.localStorage.setItem('role', role)
				const redirectUrl: string = (role === 'admin') ? '/admin' : '/dashboard'
				this.router.navigate([redirectUrl])
			// })
	}

	getRole(): string | null {
		return window.localStorage.getItem('role')
	}

	logout() {
		window.localStorage.removeItem('token')
		window.localStorage.removeItem('role')
		this.router.navigate(['/login']);
	}

	checkAuth() {
		if (window.localStorage.getItem('token')) {
			return true
		}
		return false
	}
}
