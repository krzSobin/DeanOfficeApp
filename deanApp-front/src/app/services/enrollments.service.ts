import { Injectable, EventEmitter } from '@angular/core'
import { Router } from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { Headers, Response } from '@angular/http'

import { MatDialogRef } from '@angular/material'

import { Subject } from 'rxjs/Subject'
import { Observable } from 'rxjs/Observable'

import { Lecture } from '../models/Lecture'
import { Enrollment } from '../models/Enrollment'
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

	signOut(id: number): Observable<Enrollment> {
		return this.http.delete(`${ this.url }/${ id }`)
	}
}
