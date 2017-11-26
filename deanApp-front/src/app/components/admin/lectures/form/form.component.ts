import { Component, Input, OnInit } from '@angular/core'

import { MatDialog, MatDialogRef } from '@angular/material'

import { Lecture } from '../../../../models/Lecture'
import { Teacher } from '../../../../models/Teacher'
import { LecturesService } from '../../../../services/lectures.service'
import { TeachersService } from '../../../../services/teachers.service'
import { ConfirmationModal } from '../../confirmation.component'

@Component({
	selector: 'lecture-form',
	templateUrl: './form.component.html'
})

export class LectureFormComponent implements OnInit {
	public confirmationModal: MatDialogRef<ConfirmationModal>

	constructor(private lecturesService: LecturesService, private teachersService: TeachersService, public modal: MatDialog) { }

	@Input() type: string
	@Input() lecture: Lecture
	private isEditable: boolean
	private teachers: Teacher[] = []
	private teacherId: number

	ngOnInit() {
		this.teachersService.get().subscribe(teachers => {
			this.teachers = teachers
		})
		this.isEditable = this.type === 'add'
	}

	showFullName(teacherId: number) {
		if (teacherId) {
			const teacher = this.teachers.find(teacher => teacher.teacherId === teacherId)
			return `${ teacher.firstName } ${ teacher.lastName }`
		}
		if (this.lecture.teacher) {
			return this.lecture.teacher // TO FIX
		}
	}

	makeEditable() {
		this.isEditable = true
		window.setTimeout(() => {
			const firstInput = <HTMLElement>document.querySelector('#lectureForm input')
			firstInput.focus()
		}, 0)
	}

	openDeleteLectureModal(lecture: Lecture): void {
		this.confirmationModal = this.modal.open(ConfirmationModal)

		this.confirmationModal.afterClosed().subscribe(result => {
			result && this.lecturesService.passLectureFormData(lecture, 'delete', this.confirmationModal)
		})
	}

	onSubmit(form) {
		const lecture: Lecture = form.value
		this.lecturesService.passLectureFormData(lecture, this.type)
	}
}
