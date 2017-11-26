import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { StudentDashboardComponent } from './components/student/dashboard.component'
import { TeacherDashboardComponent } from './components/teacher/dashboard.component'

import { LoginComponent } from './components/login/login.component'

import { AdminComponent } from './components/admin/admin.component'
import { StudentsComponent } from './components/admin/students/students.component'
import { TeachersComponent } from './components/admin/teachers/teachers.component'
import { LecturesComponent } from './components/admin/lectures/lectures.component'

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
		component: StudentDashboardComponent
	},
	{
		path: 'teacher',
		canActivateChild: [AuthGuard],
		data: { expectedRole: 'teacher' },
		component: TeacherDashboardComponent
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
			},
			{
				path: 'lectures',
				component: LecturesComponent
			}
		]
	},
	{
		path: '**',
		redirectTo: 'login'
	}


]

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})

export class RoutingModule { }
