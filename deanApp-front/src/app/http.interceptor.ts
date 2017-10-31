import { Injectable, Injector } from '@angular/core'
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http'
import { Observable } from 'rxjs/Observable'

import { AuthService } from './auth/auth.service'

@Injectable()
export class CustomHttpInterceptor implements HttpInterceptor {

	constructor(private injector: Injector) {}

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

		const API_url = 'http://krzysek-001-site1.gtempurl.com/'

		const auth = this.injector.get(AuthService)

		const authReq = req.clone({
			headers: req.headers.set('Authorization', `Bearer ${ auth.getAccessToken() }`),
			url: API_url + req.url
		})
		return next.handle(authReq)
	}
}
