import { Component, OnInit } from '@angular/core'
import { Router } from '@angular/router'
import { MatExpansionModule, MatSnackBar } from '@angular/material'

import { LecturesService } from '../../../services/lectures.service'
import { EnrollmentsService } from '../../../services/enrollments.service'

import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/mergeMap'

import { Lecture } from '../../../models/Lecture'

@Component({
	selector: 'available-lectures',
	templateUrl: './available-lectures.component.html',
	// styleUrls: ['./dashboard.component.scss']
})

export class AvailableLecturesComponent implements OnInit {

	private lectures: Lecture[]

	constructor(private lecturesService: LecturesService, private enrollmentsService: EnrollmentsService, public snackbar: MatSnackBar) { }

	ngOnInit() {
		this.getLectures()
	}

	getLectures(): void {
		this.lecturesService.get().subscribe(lectures => {
			this.lectures = lectures
		})
	}

	joinLecture(lectureId) {
		this.enrollmentsService.enroll(lectureId).subscribe(res => {
			this.snackbar.open('Pomyślnie zapisano na zajęcia!', 'OK', {
				duration: 5000
			})
			this.lectures = this.lectures.filter(lecture => lecture.lectureId !== lectureId)
		})
	}
}
