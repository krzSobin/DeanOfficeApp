import { Component, Input, OnInit } from '@angular/core'

import { Lecture } from '../../../../models/Lecture'
import { LecturesService } from '../../../../services/lectures.service'

@Component({
	selector: 'lecture-form',
	templateUrl: './form.component.html'
})

export class LectureFormComponent implements OnInit {
	constructor(private lecturesService: LecturesService) {	}

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

	onSubmit(form) {
		const lecture: Lecture = form.value
		this.lecturesService.passLectureFormData(lecture, this.type)
	}
}
