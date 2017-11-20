import { Component, Input, Output, EventEmitter } from '@angular/core'

import { Student } from '../../../../models/Student'
import { StudentsService } from '../../../../services/students.service'

@Component({
	selector: 'student-form',
	templateUrl: './form.component.html'
})

export class StudentFormComponent {
	constructor(private studentsService: StudentsService) {}

	@Input() type: string
	@Input() student: Student

	onSubmit(form) {
		const student: Student = form.value
		this.studentsService.passStudentFormData(student, this.type)
	}
}
