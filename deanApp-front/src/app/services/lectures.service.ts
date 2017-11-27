import { Injectable, EventEmitter } from '@angular/core'
import { Router } from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { Headers, Response } from '@angular/http'

import { MatDialogRef } from '@angular/material'

import { Subject } from 'rxjs/Subject'
import { Observable } from 'rxjs/Observable'

import { Lecture } from '../models/Lecture'
import { ConfirmationModal } from '../components/admin/confirmation.component'

@Injectable()
export class LecturesService {

	private url: string = 'api/lectures'
	private lectureFormSubmitHandler: Subject<any> = new Subject<any>()

	formSubmitted$ = this.lectureFormSubmitHandler.asObservable()

	constructor(private http: HttpClient) { }

	passLectureFormData(lecture: Lecture, type: string, modal?: MatDialogRef<ConfirmationModal>) {
		this.lectureFormSubmitHandler.next({ lecture, type, modal })
	}

	get(id: number | string = ''): Observable<Lecture[]> {
		return this.http.get(`${ this.url }/${ id }`)
	}

	add(lecture: Lecture): Observable<Lecture> {
		return this.http.post(this.url, lecture)
	}

	edit(lecture: Lecture): Observable<any> {
		return this.http.put(`${ this.url }/${ lecture.lectureId }`, lecture)
	}

	delete(lecture: Lecture): Observable<any> {
		return this.http.delete(`${ this.url }/${ lecture.lectureId }`, lecture)
	}
}
