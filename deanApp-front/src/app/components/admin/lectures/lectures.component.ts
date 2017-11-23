import { Component, OnInit, Inject, OnDestroy, ChangeDetectorRef } from '@angular/core'
import { LecturesService } from '../../../services/lectures.service'
import { DataSource } from '@angular/cdk/collections'

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material'

import { Observable } from 'rxjs/Observable'
import { Subscription } from 'rxjs/Subscription'
import 'rxjs/add/observable/of'

import { Lecture } from '../../../models/Lecture'
import { LectureFormComponent } from './form/form.component'
import { ConfirmationModal } from '../confirmation.component'

@Component({
	selector: 'lectures-component',
	templateUrl: './lectures.component.html'
})

export class LecturesComponent implements OnDestroy {
	public columns: Array<string>
	public dataSource: LecturesDataSource | null
	public subscription: Subscription

	private addLectureModal: MatDialogRef<AddEditLectureModal>
	public editLectureModal: MatDialogRef<AddEditLectureModal>

	public lectures: Lecture[]

	constructor(private lecturesService: LecturesService, public modal: MatDialog, private changeDetectorRefs: ChangeDetectorRef, public snackbar: MatSnackBar) {
		this.columns = ['name', 'minimalSemester', 'ecstsPoints']

		this.subscription = lecturesService.formSubmitted$.subscribe(({ lecture, type, modal }) => {
			switch (type) {
				case 'add':
					this.addLecture(lecture)
					break
				case 'edit':
					this.editLecture(lecture)
					break
				case 'delete':
					this.deleteLecture(lecture, modal)
					break
				default:
					throw new Error('Lecture Form - invalid action type')
			}
		})
	}

	ngOnInit() {
		this.getLectures()
	}

	openEditLectureModal(lecture: Lecture): void {
		this.editLectureModal = this.modal.open(AddEditLectureModal, {
			data: {
				action: 'edit',
				lecture: { ...lecture }
			}
		})
	}

	openAddLectureModal(): void {
		this.addLectureModal = this.modal.open(AddEditLectureModal, {
			data: {
				action: 'add',
				lecture: {
					enrollmentDate: new Date()
				}
			}
		})
	}

	getLectures(): void {
		this.lecturesService.get().subscribe(lectures => {
			this.lectures = lectures
			this.refreshTable()
		})

	}

	addLecture(lecture: Lecture): void {
		this.lecturesService.add(lecture).subscribe(lecture => {
			this.lectures.push(lecture)
			this.refreshTable()
			this.snackbar.open('Pomyślnie dodano przedmiot!', 'OK', {
				duration: 5000
			})
			this.addLectureModal.close()
		})
	}

	editLecture(lecture: Lecture): void {
		this.lecturesService.edit(lecture).subscribe(editedLecture => {
			const index = this.lectures.findIndex(lecture => lecture.lectureId === editedLecture.lecture.lectureId)
			this.lectures[index] = editedLecture.lecture
			this.refreshTable()
			this.snackbar.open('Zapisano zmiany!', 'OK', {
				duration: 4000
			})
			this.editLectureModal.close()
		})
	}

	deleteLecture(lecture: Lecture, modal: MatDialogRef<ConfirmationModal>): void {
		this.lecturesService.delete(lecture).subscribe(res => {
			this.lectures = this.lectures.filter(lecture => lecture.lectureId !== res.lecture.lectureId)
			this.refreshTable()
			this.snackbar.open('Pomyślnie usunięto przedmiot!', 'OK', {
				duration: 5000
			})
			modal.close()
			this.editLectureModal.close()
		})
	}

	refreshTable() {
		this.dataSource = new LecturesDataSource(this.lectures)
		this.changeDetectorRefs.detectChanges()
	}

	ngOnDestroy() {
		this.subscription.unsubscribe()
	}
}


export class LecturesDataSource extends DataSource<Lecture> {
	constructor(private lectures: Lecture[]) {
		super()
	}

	connect(): Observable<Lecture[]> {
		return Observable.of(this.lectures)
	}

	disconnect() { }
}


@Component({
	selector: 'lecture-modal',
	template: '<lecture-form [type]="data.action" [lecture]="data.lecture"></lecture-form>'
})

export class AddEditLectureModal {

	constructor(public dialogRef: MatDialogRef<AddEditLectureModal>, @Inject(MAT_DIALOG_DATA) public data: any) { }

	onNoClick(): void {
		this.dialogRef.close()
	}
}
