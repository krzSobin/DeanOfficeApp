import { Component, OnInit } from '@angular/core'
import { Router } from '@angular/router'
import { MatExpansionModule, MatSnackBar } from '@angular/material'

import { LecturesService } from '../../../services/lectures.service'
import { TeachersService } from '../../../services/teachers.service'
import { EnrollmentsService } from '../../../services/enrollments.service'

import { Enrollment } from '../../../models/Enrollment'

@Component({
	selector: 'student-lectures',
	templateUrl: './lectures.component.html'
})

export class StudentLecturesComponent implements OnInit {

	private enrollments: Enrollment[]

	constructor(private lecturesService: LecturesService, private teachersService: TeachersService, private enrollmentsService: EnrollmentsService, public snackbar: MatSnackBar) { }

	ngOnInit() {
		this.getEnrollments()
	}

	getEnrollments(): void {
		this.enrollmentsService.get().subscribe(enrollments => {
			this.enrollments = enrollments
		})
	}

	getLectureDetails(enrollment): void {
		this.lecturesService.get(enrollment.lectureId).subscribe(lecture => {
			enrollment.lecture = lecture
		})
	}

	signOutFromLecture(enrollmentId): void {
		this.enrollmentsService.signOut(enrollmentId).subscribe(res => {
			this.enrollments = this.enrollments.filter(enrollment => enrollment.id !== res.id)
		})
	}
}
