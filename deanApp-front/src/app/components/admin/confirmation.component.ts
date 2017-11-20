import { Component } from '@angular/core'
import { MatDialogRef } from '@angular/material'

@Component({
	selector: 'confirmation-modal',
	template: `<p>Czy na pewno chcesz usunąć?</p>
	<button (click)="dialogRef.close('confirmed')">usuń</button>
	<button (click)="dialogRef.close()">anuluj</button>`
})

export class ConfirmationModal {

	constructor(public dialogRef: MatDialogRef<ConfirmationModal>) { }

	onNoClick(): void {
		this.dialogRef.close()
	}
}
