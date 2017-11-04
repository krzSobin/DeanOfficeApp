import { Injectable } from '@angular/core'
import { Router } from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { Headers, Response } from '@angular/http'

import { Observable } from 'rxjs/Observable'
import { Student } from '../models/Student'

import { MatTableModule } from '@angular/material';

import 'rxjs/add/operator/toPromise'

@Injectable()
export class StudentsService {

	private url: string = 'api/students'

	constructor(private http: HttpClient) { }

	getStudent(id?: number): Observable<Student[]> {
		return this.http.get(this.url)
			// .then(res => {
			// 	console.log(res)
			// 	return res as Student[]
			// })
			// .catch(err => {
			// 	console.error(err.error)
			// 	return Promise.reject(err)
			// })
	}
}
