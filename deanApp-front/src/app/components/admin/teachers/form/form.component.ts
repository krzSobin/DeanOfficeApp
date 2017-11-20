import { Component, Input, Output, EventEmitter } from '@angular/core'

import { Teacher } from '../../../../models/Teacher'
import { TeachersService } from '../../../../services/teachers.service'

@Component({
	selector: 'teacher-form',
	templateUrl: './form.component.html'
})

export class TeacherFormComponent {
	constructor(private teachersService: TeachersService) {}

	@Input() type: string
	@Input() teacher: Teacher

	onSubmit(form) {
		const teacher: Teacher = form.value
		this.teachersService.passTeacherFormData(teacher, this.type)
	}
}
