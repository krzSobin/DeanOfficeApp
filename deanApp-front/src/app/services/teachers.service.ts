import { Injectable, EventEmitter } from '@angular/core'
import { Router } from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { Headers, Response } from '@angular/http'

import { Subject } from 'rxjs/Subject'
import { Observable } from 'rxjs/Observable'

// import { MatTableModule } from '@angular/material'

import { Teacher } from '../models/Teacher'

// import 'rxjs/add/operator/toPromise'

@Injectable()
export class TeachersService {

	private url: string = 'api/teachers'
	private teacherFormSubmitHandler: Subject<any> = new Subject<any>()

	formSubmitted$ = this.teacherFormSubmitHandler.asObservable()

	constructor(private http: HttpClient) { }

	passTeacherFormData(teacher: Teacher, type: string) {
		this.teacherFormSubmitHandler.next({ teacher, type })
	}

	get(id?: number): Observable<Teacher[]> {
		return this.http.get(this.url)
	}

	add(teacher: Teacher): Observable<Teacher> {
		return this.http.post(this.url, teacher)
	}

	edit(teacher: Teacher): Observable<any> {
		return this.http.put(`${ this.url }/${ teacher.teacherId }`, teacher)
	}

	delete(teacher: Teacher): Observable<any> {
		return this.http.delete(`${ this.url }/${ teacher.teacherId }`, teacher)
	}
}
