import { Injectable } from '@angular/core'
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http'
import { Observable } from 'rxjs/Observable'

import { AuthService } from './auth.service'

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
	constructor(private auth: AuthService) { }

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		const authReq = req.clone({
			headers: req.headers.set('Authorization', `Bearer ${ this.auth.getAccessToken() }`)
		})
		return next.handle(authReq)
	}
}
