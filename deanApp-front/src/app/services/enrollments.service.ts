import { Injectable, EventEmitter } from '@angular/core'
import { Router } from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { Headers, Response } from '@angular/http'

import { MatDialogRef } from '@angular/material'

import { Subject } from 'rxjs/Subject'
import { Observable } from 'rxjs/Observable'

import { Lecture } from '../models/Lecture'
import { Enrollment } from '../models/Enrollment'
import { Grade } from '../models/Grade'
import { ConfirmationModal } from '../components/admin/confirmation.component'

@Injectable()
export class EnrollmentsService {

	private url: string = 'api/enrollments'

	constructor(private http: HttpClient) { }

	enroll(lectureId: number): Observable<number> {
		return this.http.post(this.url, { lectureId: lectureId })
	}

	get(): Observable<Enrollment[]> {
		return this.http.get(this.url)
	}

	addGrade(grade): Observable<Grade> {
		return this.http.post(`${ this.url }/${ grade.enrollementId }/grades`, grade)
	}

	getGrades() {
		return this.http.get('api/grades')
	}

	getEnrolledStudents(id: number): Observable<Enrollment[]> {
		return this.http.get(`api/lectures/${ id }/Enrollments/`)
	}

	signOut(id: number): Observable<Enrollment> {
		return this.http.delete(`${ this.url }/${ id }`)
	}
}
