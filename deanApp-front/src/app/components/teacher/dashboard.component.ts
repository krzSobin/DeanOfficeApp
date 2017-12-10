import { Component, OnInit } from '@angular/core'
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router'

import { AuthService } from '../../auth/auth.service'
// import { StudentsService } from '../../students/students.service'
import { Subscription } from 'rxjs/Subscription'
import 'rxjs/add/operator/filter';

// import { Student } from '../../models/Student'

@Component({
	selector: 'teacher-dashboard',
	templateUrl: './dashboard.component.html'
})

export class TeacherDashboardComponent implements OnInit {
	private isSingleLecture: boolean

	constructor(private auth: AuthService, private router: Router, private route: ActivatedRoute) { }

	ngOnInit() {
		const url = this.router.url
		this.isSingleLecture = url.startsWith('/teacher/lecture/')
		this.router.events
			.filter(event => event instanceof NavigationEnd)
			.subscribe((event: NavigationEnd) => {
				this.isSingleLecture = event.url.startsWith('/teacher/lecture/')
			});
	}
}
