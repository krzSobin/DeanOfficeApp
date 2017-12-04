import { Component, OnInit } from '@angular/core'
import { Router, ActivatedRoute, ParamMap } from '@angular/router'

import 'rxjs/add/operator/switchMap'
import { Observable } from 'rxjs/Observable'

import { Lecture } from '../../../models/Lecture'
import { Enrollment } from '../../../models/Enrollment'
import { Grade } from '../../../models/Grade'

import { LecturesService } from '../../../services/lectures.service'
import { EnrollmentsService } from '../../../services/enrollments.service'

@Component({
	selector: 'single-lecture',
	templateUrl: './single-lecture.component.html'
})

export class SingleLectureComponent implements OnInit {
	private id: number
	private gradeOptions
	private selectedValue: string
	private grades: Grade[]
	private enrollments: Enrollment[]
	private addGrade: boolean = false

	lecture$: Observable<Lecture[]>
	private lecture: Lecture[]

	constructor(private router: Router, private route: ActivatedRoute, private lecturesService: LecturesService, private enrollmentsService: EnrollmentsService) { }

	ngOnInit() {
		this.id = +this.route.snapshot.paramMap.get('id')

		this.lecturesService.get(this.id).subscribe(lecture => {
			this.lecture = lecture
		})
		this.enrollmentsService.getEnrolledStudents(this.id).subscribe(enrollments => {
			this.enrollments = enrollments
		})
		this.enrollmentsService.getGrades().subscribe(gradeOptions => {
			this.gradeOptions = gradeOptions
		})
	}

	onSubmit(form): void {
		const formData = form.value
		formData.date = new Date
		this.enrollmentsService.addGrade(formData).subscribe(res => {
			this.enrollments.find(enrollment => enrollment.id === formData.enrollementId).grades.push(res)
			this.addGrade = false
		})
	}
}
