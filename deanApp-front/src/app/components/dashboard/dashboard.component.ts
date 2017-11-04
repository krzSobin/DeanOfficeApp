import { Component } from '@angular/core'
import { Router } from '@angular/router'

import { AuthService } from '../../auth/auth.service'
// import { StudentsService } from '../../students/students.service'

// import { Student } from '../../models/Student'

@Component({
	selector: 'dashboard-component',
	templateUrl: './dashboard.component.html',
	styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent {
	constructor(private auth: AuthService, private router: Router) { }

	logout(): void {
		this.auth.logout()
	}

	getStudents(): void {
		this.router.navigate(['/dashboard/xd'])
	}
}
