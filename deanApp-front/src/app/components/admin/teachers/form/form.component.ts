import { Component, Input, OnInit } from '@angular/core'

import { Teacher } from '../../../../models/Teacher'
import { TeachersService } from '../../../../services/teachers.service'

@Component({
	selector: 'teacher-form',
	templateUrl: './form.component.html'
})

export class TeacherFormComponent implements OnInit {
	constructor(private teachersService: TeachersService) {	}

	@Input() type: string
	@Input() teacher: Teacher
	private isEditable: boolean

	ngOnInit() {
		this.isEditable = this.type === 'add'
	}

	makeEditable() {
		this.isEditable = true
		window.setTimeout(() => {
			const firstInput = <HTMLElement>document.querySelector('#teacherForm input')
			firstInput.focus()
		}, 0)
	}

	onSubmit(form) {
		const teacher: Teacher = form.value
		this.teachersService.passTeacherFormData(teacher, this.type)
	}
}
