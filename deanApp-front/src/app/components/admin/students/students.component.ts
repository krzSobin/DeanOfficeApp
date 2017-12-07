import { Component, OnInit, Inject, OnDestroy, ChangeDetectorRef } from '@angular/core'
import { StudentsService } from '../../../services/students.service'
import { DataSource } from '@angular/cdk/collections'

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material'

import { Observable } from 'rxjs/Observable'
import { Subscription } from 'rxjs/Subscription'
import 'rxjs/add/observable/of'

import { Student } from '../../../models/Student'
import { StudentFormComponent } from './form/form.component'
import { ConfirmationModal } from '../confirmation.component'

@Component({
	selector: 'students-component',
	templateUrl: './students.component.html'
})

export class StudentsComponent implements OnDestroy {
	public columns: Array<string>
	public dataSource: StudentsDataSource | null
	public subscription: Subscription

	private addStudentModal: MatDialogRef<AddEditStudentModal>
	public editStudentModal: MatDialogRef<AddEditStudentModal>

	public students: Student[]

	constructor(private studentsService: StudentsService, public modal: MatDialog, private changeDetectorRefs: ChangeDetectorRef, public snackbar: MatSnackBar) {
		this.columns = ['firstName', 'lastName', 'recordBookNumber']

		this.subscription = studentsService.formSubmitted$.subscribe(({ student, type, modal }) => {
			switch (type) {
				case 'add':
					this.addStudent(student)
					break
				case 'edit':
					this.editStudent(student)
					break
				case 'delete':
					this.deleteStudent(student, modal)
					break
				default:
					throw new Error('Student Form - invalid action type')
			}
		})
	}

	ngOnInit() {
		this.getStudents()
	}

	openEditStudentModal(student: Student): void {
		this.editStudentModal = this.modal.open(AddEditStudentModal, {
			data: {
				action: 'edit',
				student: { ...student }
			}
		})
	}

	openAddStudentModal(): void {
		this.addStudentModal = this.modal.open(AddEditStudentModal, {
			data: {
				action: 'add',
				student: {
					enrollmentDate: new Date(),
					addresses: [{}]
				}
			}
		})
	}

	getStudents(): void {
		this.studentsService.get().subscribe(students => {
			this.students = students
			this.refreshTable()
		})

	}

	addStudent(student: Student): void {
		this.studentsService.add(student).subscribe(student => {
			this.students.push(student)
			this.refreshTable()
			this.snackbar.open('Pomyślnie dodano studenta!', 'OK', {
				duration: 5000
			})
			this.addStudentModal.close()
		})
	}

	editStudent(student: Student): void {
		this.studentsService.edit(student).subscribe(editedStudent => {
			const index = this.students.findIndex(student => student.recordBookNumber === editedStudent.student.recordBookNumber)
			this.students[index] = editedStudent.student
			this.refreshTable()
			this.snackbar.open('Zapisano zmiany!', 'OK', {
				duration: 4000
			})
			this.editStudentModal.close()
		})
	}

	deleteStudent(student: Student, modal: MatDialogRef<ConfirmationModal>): void {
		this.studentsService.delete(student).subscribe(res => {
			this.students = this.students.filter(student => student.recordBookNumber !== res.student.recordBookNumber)
			this.refreshTable()
			this.snackbar.open('Pomyślnie usunięto studenta!', 'OK', {
				duration: 5000
			})
			modal.close()
			this.editStudentModal.close()
		})
	}

	refreshTable() {
		this.dataSource = new StudentsDataSource(this.students)
		this.changeDetectorRefs.detectChanges()
	}

	ngOnDestroy() {
		this.subscription.unsubscribe()
	}
}


export class StudentsDataSource extends DataSource<Student> {
	constructor(private students: Student[]) {
		super()
	}

	connect(): Observable<Student[]> {
		return Observable.of(this.students)
	}

	disconnect() { }
}


@Component({
	selector: 'student-modal',
	template: '<student-form [type]="data.action" [student]="data.student"></student-form>'
})

export class AddEditStudentModal {

	constructor(public dialogRef: MatDialogRef<AddEditStudentModal>, @Inject(MAT_DIALOG_DATA) public data: any) { }

	onNoClick(): void {
		this.dialogRef.close()
	}
}
