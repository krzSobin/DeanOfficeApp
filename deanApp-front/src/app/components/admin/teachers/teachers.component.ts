import { Component, OnInit, Inject, OnDestroy, ChangeDetectorRef } from '@angular/core'
import { TeachersService } from '../../../services/teachers.service'
import { DataSource } from '@angular/cdk/collections'

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material'

import { Observable } from 'rxjs/Observable'
import { Subscription } from 'rxjs/Subscription'
import 'rxjs/add/observable/of'

import { Teacher } from '../../../models/Teacher'
import { TeacherFormComponent } from './form/form.component'
import { ConfirmationModal } from '../confirmation.component'

@Component({
	selector: 'teachers-component',
	templateUrl: './teachers.component.html'
})

export class TeachersComponent implements OnDestroy {
	public columns: Array<string>
	public dataSource: TeachersDataSource | null
	public subscription: Subscription

	public confirmationModal: MatDialogRef<ConfirmationModal>
	private addTeacherModal: MatDialogRef<AddEditTeacherModal>
	public editTeacherModal: MatDialogRef<AddEditTeacherModal>

	public teachers: Teacher[]

	constructor(private teachersService: TeachersService, public modal: MatDialog, private changeDetectorRefs: ChangeDetectorRef, public snackbar: MatSnackBar) {
		this.columns = ['degree', 'firstName', 'lastName', 'room']

		this.subscription = teachersService.formSubmitted$.subscribe(({ teacher, type }) => {
			switch (type) {
				case 'add':
					this.addTeacher(teacher)
					break
				case 'edit':
					this.editTeacher(teacher)
					break
				default:
					throw new Error('Teacher Form - invalid action type')
			}
		})
	}

	ngOnInit() {
		this.getTeachers()
	}

	openEditTeacherModal(teacher: Teacher): void {
		this.editTeacherModal = this.modal.open(AddEditTeacherModal, {
			data: {
				action: 'edit',
				teacher: { ...teacher }
			}
		})
	}

	openAddTeacherModal(): void {
		this.addTeacherModal = this.modal.open(AddEditTeacherModal, {
			data: {
				action: 'add',
				teacher: {
					enrollmentDate: new Date()
				}
			}
		})
	}

	openDeleteTeacherModal(teacher: Teacher): void {
		this.confirmationModal = this.modal.open(ConfirmationModal)

		this.confirmationModal.afterClosed().subscribe(result => {
			result && this.deleteTeacher(teacher)
	 	})
	}

	getTeachers(): void {
		this.teachersService.get().subscribe(teachers => {
			this.teachers = teachers
			this.refreshTable()
		})

	}

	addTeacher(teacher: Teacher): void {
		this.teachersService.add(teacher).subscribe(teacher => {
			this.teachers.push(teacher)
			this.refreshTable()
			this.snackbar.open('Pomyślnie dodano wykładowcę!', 'OK', {
				duration: 5000
			})
			this.addTeacherModal.close()
		})
	}

	editTeacher(teacher: Teacher): void {
		this.teachersService.edit(teacher).subscribe(editedTeacher => {
			const index = this.teachers.findIndex(teacher => teacher.teacherId === editedTeacher.teacher.teacherId)
			this.teachers[index] = editedTeacher.teacher
			this.refreshTable()
			this.snackbar.open('Zapisano zmiany!', 'OK', {
				duration: 4000
			})
			this.editTeacherModal.close()
		})
	}

	deleteTeacher(teacher: Teacher): void {
		this.teachersService.delete(teacher).subscribe(res => {
			this.teachers = this.teachers.filter(teacher => teacher.teacherId !== res.teacher.teacherId)
			this.refreshTable()
			this.snackbar.open('Pomyślnie usunięto wykładowcę!', 'OK', {
				duration: 5000
			})
			this.confirmationModal.close()
		})
	}

	refreshTable() {
		this.dataSource = new TeachersDataSource(this.teachers)
		this.changeDetectorRefs.detectChanges()
	}

	ngOnDestroy() {
		this.subscription.unsubscribe()
	}
}


export class TeachersDataSource extends DataSource<Teacher> {
	constructor(private teachers: Teacher[]) {
		super()
	}

	connect(): Observable<Teacher[]> {
		return Observable.of(this.teachers)
	}

	disconnect() { }
}


@Component({
	selector: 'teacher-modal',
	template: '<teacher-form [type]="data.action" [teacher]="data.teacher"></teacher-form>'
})

export class AddEditTeacherModal {

	constructor(public dialogRef: MatDialogRef<AddEditTeacherModal>, @Inject(MAT_DIALOG_DATA) public data: any) { }

	onNoClick(): void {
		this.dialogRef.close()
	}
}
