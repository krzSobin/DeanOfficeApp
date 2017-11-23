import { Component, Input, OnInit } from '@angular/core'

import { MatDialog, MatDialogRef } from '@angular/material'

import { Teacher } from '../../../../models/Teacher'
import { TeachersService } from '../../../../services/teachers.service'
import { ConfirmationModal } from '../../confirmation.component'

@Component({
	selector: 'teacher-form',
	templateUrl: './form.component.html'
})

export class TeacherFormComponent implements OnInit {
	public confirmationModal: MatDialogRef<ConfirmationModal>

	constructor(private teachersService: TeachersService, public modal: MatDialog) {	}

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

	openDeleteTeacherModal(teacher: Teacher): void {
		this.confirmationModal = this.modal.open(ConfirmationModal)

		this.confirmationModal.afterClosed().subscribe(result => {
			result && this.teachersService.passTeacherFormData(teacher, 'delete', this.confirmationModal)
		})
	}

	onSubmit(form) {
		const teacher: Teacher = form.value
		this.teachersService.passTeacherFormData(teacher, this.type)
	}
}
