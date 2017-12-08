import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core'
import { FormControl } from '@angular/forms';

import { MatDialog, MatDialogRef } from '@angular/material'
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/startWith'

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
	private filteredTeachers: Teacher[]
	private teacherId: number

	ngOnInit() {
		this.teachersService.get().subscribe(teachers => {
			this.teachers = teachers
			this.filteredTeachers = teachers
			this.getTeacherId(this.lecture.teacher)
		})
		this.isEditable = this.type === 'add'
	}

	getTeacherId(fullName: string) {
		this.teacherId = this.teachers.filter(teacher => `${ teacher.firstName } ${ teacher.lastName }` === fullName)[0].teacherId
	}

	showFullName(teacherId: number): string {
		if (teacherId) {
			const teacher = this.teachers.find(teacher => teacher.teacherId === teacherId)
			return `${teacher.firstName} ${teacher.lastName}`
		}
		if (this.lecture.teacher) {
			return this.lecture.teacher // TO FIX
		}
	}

	makeEditable(form): void {
		this.isEditable = true
		window.setTimeout(() => {
			const firstInput = <HTMLElement>document.querySelector('#lectureForm input')
			firstInput.focus()
			form.control.markAsDirty()
		}, 0)
	}

	openDeleteLectureModal(lecture: Lecture): void {
		this.confirmationModal = this.modal.open(ConfirmationModal)

		this.confirmationModal.afterClosed().subscribe(result => {
			result && this.lecturesService.passLectureFormData(lecture, 'delete', this.confirmationModal)
		})
	}

	filter(query: string): Teacher[] {
		if (typeof query == 'string' || query instanceof String) {
			return this.teachers.filter(teacher => {
				teacher.fullName = `${teacher.firstName} ${teacher.lastName}`
				return teacher.fullName.toLowerCase().indexOf(query.toLowerCase()) === 0
			})
		}
	}

	onSubmit(form): void {
		const lecture: Lecture = form.value
		this.lecturesService.passLectureFormData(lecture, this.type)
	}
}
