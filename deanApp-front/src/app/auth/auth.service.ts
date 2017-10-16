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
		return this.http.post('', { UserName: username, Password: password, grant_type: 'password' })
			.map((res: Response) => {
				window.localStorage.setItem('token', 'xd')
			})
	}

	logout() {
		window.localStorage.removeItem('token')
        this.router.navigate(['/login']);
	}

	checkAuth() {
		if (window.localStorage.getItem('token')) {
			return true
		}
		return false
	}
}
