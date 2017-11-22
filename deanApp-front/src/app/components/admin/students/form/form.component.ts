import { Component, Input, OnInit } from '@angular/core'

import { Student } from '../../../../models/Student'
import { StudentsService } from '../../../../services/students.service'

@Component({
	selector: 'student-form',
	templateUrl: './form.component.html'
})

export class StudentFormComponent implements OnInit {
	constructor(private studentsService: StudentsService) {}

	@Input() type: string
	@Input() student: Student
	private isEditable: boolean

	ngOnInit() {
		this.isEditable = this.type === 'add'
	}

	makeEditable() {
		this.isEditable = true
		window.setTimeout(() => {
			const firstInput = <HTMLElement>document.querySelector('#studentForm input')
			firstInput.focus()
		}, 0)
	}

	onSubmit(form) {
		const student: Student = form.value
		this.studentsService.passStudentFormData(student, this.type)
	}
}
