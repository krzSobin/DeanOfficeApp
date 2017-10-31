import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { DashboardComponent } from './components/dashboard/dashboard.component'
import { LoginComponent } from './components/login/login.component'
import { AdminComponent } from './components/admin/admin.component'
import { AdminChildComponent } from './components/admin/child/admin-child.component'

import { AuthGuard } from './auth/auth-guard.service'
import { AuthService } from './auth/auth.service'

const routes: Routes = [
	{
		 path: '',
		 redirectTo: 'login',
		//  pathMatch: 'prefix'
		 pathMatch: 'full'
	},
	{
		path: 'login',
		component: LoginComponent
	},
	{
		path: '',
		canActivateChild: [AuthGuard],
		data: { expectedRole: 'user' },
		children: [
			{
				path: 'dashboard',
				component: DashboardComponent
			}
		]
	},
	{
		path: 'admin',
		component: AdminComponent,
		data: { expectedRole: 'admin' },
		canActivate: [AuthGuard],
		canActivateChild: [AuthGuard],
		children: [
			{
				path: 'child',
				component: AdminChildComponent
			}
		]
	},

]

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})

export class RoutingModule { }
