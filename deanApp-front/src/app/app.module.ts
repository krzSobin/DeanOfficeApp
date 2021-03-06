import { BrowserModule } from '@angular/platform-browser'
import { NgModule } from '@angular/core'
import { RouterModule } from '@angular/router'
import { HttpModule } from '@angular/http'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { MatDialogModule, MatTableModule, MatInputModule, MatButtonModule, MatCardModule, MatToolbarModule, MatMenuModule, MatSnackBarModule, MatAutocompleteModule, MatExpansionModule, MatFormFieldModule, MatSelectModule } from '@angular/material'

import { AppComponent, PasswordModal } from './app.component'
import { StudentDashboardComponent } from './components/student/dashboard.component'
import { TeacherDashboardComponent } from './components/teacher/dashboard.component'
import { LoginComponent } from './components/login/login.component'
import { AdminComponent } from './components/admin/admin.component'
import { AvailableLecturesComponent } from './components/student/available-lectures/available-lectures.component'
import { TeacherLecturesComponent } from './components/teacher/lectures/lectures.component'
import { SingleLectureComponent } from './components/teacher/lectures/single-lecture.component'
import { StudentLecturesComponent } from './components/student/lectures/lectures.component'
import { StudentsComponent, AddEditStudentModal } from './components/admin/students/students.component'
import { TeachersComponent, AddEditTeacherModal } from './components/admin/teachers/teachers.component'
import { LecturesComponent, AddEditLectureModal } from './components/admin/lectures/lectures.component'
import { ConfirmationModal } from './components/admin/confirmation.component'
import { StudentFormComponent } from './components/admin/students/form/form.component'
import { TeacherFormComponent } from './components/admin/teachers/form/form.component'
import { LectureFormComponent } from './components/admin/lectures/form/form.component'

import { RoutingModule } from './routing.module'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'

import { AuthGuard } from './auth/auth-guard.service'
import { AuthService } from './auth/auth.service'
import { StudentsService } from './services/students.service'
import { TeachersService } from './services/teachers.service'
import { LecturesService } from './services/lectures.service'
import { EnrollmentsService } from './services/enrollments.service'

import { CustomHttpInterceptor } from './http.interceptor'
import { EqualValidator } from './directives//equalValidator.directive'

@NgModule({
	declarations: [
		AppComponent,
		StudentDashboardComponent,
		TeacherDashboardComponent,
		LoginComponent,
		AdminComponent,
		StudentsComponent,
		AddEditStudentModal,
		StudentFormComponent,
		TeachersComponent,
		AddEditTeacherModal,
		TeacherFormComponent,
		LecturesComponent,
		AddEditLectureModal,
		LectureFormComponent,
		AvailableLecturesComponent,
		ConfirmationModal,
		PasswordModal,
		EqualValidator,
		StudentLecturesComponent,
		TeacherLecturesComponent,
		SingleLectureComponent
	],
	imports: [
		BrowserModule,
		RoutingModule,
		HttpModule,
		FormsModule,
		ReactiveFormsModule,
		HttpClientModule,
		BrowserAnimationsModule,

		MatInputModule,
		MatFormFieldModule,
		MatSelectModule,
		MatButtonModule,
		MatCardModule,
		MatToolbarModule,
		MatTableModule,
		MatDialogModule,
		MatMenuModule,
		MatSnackBarModule,
		MatAutocompleteModule,
		MatExpansionModule
	],
	providers: [
		AuthGuard,
		AuthService,
		StudentsService,
		TeachersService,
		LecturesService,
		EnrollmentsService,
		{
			provide: HTTP_INTERCEPTORS,
			useClass: CustomHttpInterceptor,
			multi: true,
		}
	],
	entryComponents: [AddEditStudentModal, AddEditTeacherModal, AddEditLectureModal, ConfirmationModal, PasswordModal],
	bootstrap: [AppComponent]
})

export class AppModule { }
