import { BrowserModule } from '@angular/platform-browser'
import { NgModule } from '@angular/core'
import { RouterModule } from '@angular/router'
import { HttpModule }    from '@angular/http';

import { AppComponent } from './app.component'
import { DashboardComponent } from './components/dashboard/dashboard.component'
import { LoginComponent } from './components/login/login.component'
import { AdminComponent } from './components/admin/admin.component'
import { AdminChildComponent } from './components/admin/child/admin-child.component'

import { RoutingModule } from './routing.module'

import { AuthGuard } from './auth/auth-guard.service'
import { AuthService } from './auth/auth.service'

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
        HttpModule
	],
	providers: [
        AuthGuard,
        AuthService
    ],
	bootstrap: [AppComponent]
})

export class AppModule { }
