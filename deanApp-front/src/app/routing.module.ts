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
import { AvailableLecturesComponent } from './components/student/available-lectures/available-lectures.component'
import { TeacherLecturesComponent } from './components/teacher/lectures/lectures.component'
import { SingleLectureComponent } from './components/teacher/lectures/single-lecture.component'
import { StudentLecturesComponent } from './components/student/lectures/lectures.component'

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
		data: { expectedRole: 'student' },
		canActivate: [AuthGuard],
		component: StudentDashboardComponent,
		children: [
			{
				path: 'lectures',
				component: StudentLecturesComponent
			},
			{
				path: 'available-lectures',
				component: AvailableLecturesComponent
			}
		]
	},
	{
		path: 'teacher',
		data: { expectedRole: 'teacher' },
		canActivate: [AuthGuard],
		canActivateChild: [AuthGuard],
		component: TeacherDashboardComponent,
		children: [
			{
				path: 'lectures',
				component: TeacherLecturesComponent
			},
			{
				path: 'lecture/:id',
				component: SingleLectureComponent
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
