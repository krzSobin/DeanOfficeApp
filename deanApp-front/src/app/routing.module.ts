import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { DashboardComponent } from './components/dashboard/dashboard.component'
import { LoginComponent } from './components/login/login.component'
import { AdminComponent } from './components/admin/admin.component'
import { StudentsComponent } from './components/admin/students/students.component'
import { TeachersComponent } from './components/admin/teachers/teachers.component'

import { AuthGuard } from './auth/auth-guard.service'
import { AuthService } from './auth/auth.service'

const routes: Routes = [
	{
		 path: '',
		 redirectTo: 'login',
		 pathMatch: 'full'
	},
	{
		path: 'login',
		component: LoginComponent
	},
	{
		path: 'student',
		canActivateChild: [AuthGuard],
		data: { expectedRole: 'user' },
		component: DashboardComponent,
		// children: [
		// 	{
		//
		// 	}
		// ]
	},
	{
		path: 'admin',
		component: AdminComponent,
		data: { expectedRole: 'admin' },
		canActivate: [AuthGuard],
		canActivateChild: [AuthGuard],
		children: [
			{
				path: 'students',
				component: StudentsComponent
			},
			{
				path: 'teachers',
				component: TeachersComponent
			}
		]
	},
	{
		path: '**',
		component: LoginComponent
	}


]

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})

export class RoutingModule { }
