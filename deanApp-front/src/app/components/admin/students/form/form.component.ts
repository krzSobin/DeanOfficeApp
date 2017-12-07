import { Component, Input, OnInit } from '@angular/core'

import { MatDialog, MatDialogRef } from '@angular/material'

import { Student } from '../../../../models/Student'
import { StudentsService } from '../../../../services/students.service'
import { ConfirmationModal } from '../../confirmation.component'

@Component({
	selector: 'student-form',
	templateUrl: './form.component.html'
})

export class StudentFormComponent implements OnInit {
	public confirmationModal: MatDialogRef<ConfirmationModal>

	constructor(private studentsService: StudentsService, public modal: MatDialog) { }

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

	openDeleteStudentModal(student: Student): void {
		this.confirmationModal = this.modal.open(ConfirmationModal)

		this.confirmationModal.afterClosed().subscribe(result => {
			result && this.studentsService.passStudentFormData(student, 'delete', this.confirmationModal)
		})
	}

	onSubmit(form) {
		const formData = form.value
		formData.address = {}
		Object.entries(formData).forEach(([key, value]) => {
			if (key.startsWith('address.')) {
				formData.address[key.split('.')[1]] = value
				delete formData[key]
			}
		})
		const student: Student = formData
		this.studentsService.passStudentFormData(student, this.type)
	}
}
