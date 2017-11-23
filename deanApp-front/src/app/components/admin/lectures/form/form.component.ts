import { Component, Input, OnInit } from '@angular/core'

import { MatDialog, MatDialogRef } from '@angular/material'

import { Lecture } from '../../../../models/Lecture'
import { LecturesService } from '../../../../services/lectures.service'
import { ConfirmationModal } from '../../confirmation.component'

@Component({
	selector: 'lecture-form',
	templateUrl: './form.component.html'
})

export class LectureFormComponent implements OnInit {
	public confirmationModal: MatDialogRef<ConfirmationModal>

	constructor(private lecturesService: LecturesService, public modal: MatDialog) { }

	@Input() type: string
	@Input() lecture: Lecture
	private isEditable: boolean

	ngOnInit() {
		this.isEditable = this.type === 'add'
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
