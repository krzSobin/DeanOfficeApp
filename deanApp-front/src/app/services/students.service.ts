import { Injectable, EventEmitter } from '@angular/core'
import { Router } from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { Headers, Response } from '@angular/http'

import { Subject } from 'rxjs/Subject'
import { Observable } from 'rxjs/Observable'

// import { MatTableModule } from '@angular/material'

import { Student } from '../models/Student'

// import 'rxjs/add/operator/toPromise'

@Injectable()
export class StudentsService {

	private url: string = 'api/students'
	private studentFormSubmitHandler: Subject<any> = new Subject<any>()

	formSubmitted$ = this.studentFormSubmitHandler.asObservable()

	constructor(private http: HttpClient) { }

	passStudentFormData(student: Student, type: string) {
		this.studentFormSubmitHandler.next({ student, type })
	}

	get(id?: number): Observable<Student[]> {
		return this.http.get(this.url)
	}

	add(student: Student): Observable<Student> {
		return this.http.post(this.url, student)
	}

	edit(student: Student): Observable<any> {
		return this.http.put(`${ this.url }/${ student.recordBookNumber }`, student)
	}

	delete(student: Student): Observable<any> {
		return this.http.delete(`${ this.url }/${ student.recordBookNumber }`, student)
	}
}
