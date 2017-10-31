import { BrowserModule } from '@angular/platform-browser'
import { NgModule } from '@angular/core'
import { RouterModule } from '@angular/router'
import { HttpModule } from '@angular/http'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { MatInputModule, MatButtonModule, MatCardModule, MatToolbarModule } from '@angular/material'

import { AppComponent } from './app.component'
import { DashboardComponent } from './components/dashboard/dashboard.component'
import { LoginComponent } from './components/login/login.component'
import { AdminComponent } from './components/admin/admin.component'
import { AdminChildComponent } from './components/admin/child/admin-child.component'

import { RoutingModule } from './routing.module'
import { FormsModule } from '@angular/forms'

import { AuthGuard } from './auth/auth-guard.service'
import { AuthService } from './auth/auth.service'

import { CustomHttpInterceptor } from './http.interceptor'

@NgModule({
	declarations: [
		AppComponent,
		DashboardComponent,
		LoginComponent,
		AdminComponent,
		AdminChildComponent
	],
	imports: [
		BrowserModule,
		RoutingModule,
		HttpModule,
		FormsModule,
		HttpClientModule,
		BrowserAnimationsModule,

		MatInputModule,
		MatButtonModule,
		MatCardModule,
		MatToolbarModule
	],
	providers: [
		AuthGuard,
		AuthService,
		{
			provide: HTTP_INTERCEPTORS,
			useClass: CustomHttpInterceptor,
			multi: true,
		}
	],
	bootstrap: [AppComponent]
})

export class AppModule { }
