import { Component, OnInit } from '@angular/core'
import { StudentsService } from '../../../services/students.service'
import { DataSource } from '@angular/cdk/collections'

import { Observable } from 'rxjs/Observable'

import { Student } from '../../../models/Student'

@Component({
	selector: 'students-component',
	templateUrl: './students.component.html',
})

export class StudentsComponent {
	constructor(private studentsService: StudentsService) {
		this.dataSource = new ExampleDataSource(studentsService)
		this.columns = ['firstName', 'lastName', 'recordBookNumber']
	}

	columns: Array<string>
	dataSource: ExampleDataSource | null
	students: Student[]

	// ngOnInit(): void {
	// 	// this.getStudentsList()
	// }

	// getStudentsList(): void {
	// 	this.studentsService.getStudent()
	// 		.then(students => {
	// 			this.students = students
	// 		})
	// }
}

export class ExampleDataSource extends DataSource<Student> {
	constructor(private studentsService: StudentsService) {
		super()
	}

	connect(): Observable<Student[]> {
		return this.studentsService.getStudent()
	}

	disconnect() { }
}
