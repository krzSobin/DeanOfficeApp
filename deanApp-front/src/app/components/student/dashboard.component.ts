import { Component } from '@angular/core'
import { Router } from '@angular/router'

import { AuthService } from '../../auth/auth.service'
// import { StudentsService } from '../../students/students.service'

// import { Student } from '../../models/Student'

@Component({
	selector: 'student-dashboard',
	templateUrl: './dashboard.component.html',
	styleUrls: ['./dashboard.component.scss']
})

export class StudentDashboardComponent {
	constructor(private auth: AuthService, private router: Router) { }
}
