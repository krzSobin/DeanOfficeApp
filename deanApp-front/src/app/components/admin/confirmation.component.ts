import { Component } from '@angular/core'
import { MatDialogRef } from '@angular/material'

@Component({
	selector: 'confirmation-modal',
	template: `
		<p class="confirmation-modal__text">Czy na pewno chcesz usunąć?</p>
		<button mat-raised-button class="form__button form__button--left" (click)="dialogRef.close()">Anuluj</button>
		<button mat-raised-button class="form__button form__button--right" color="warn" (click)="dialogRef.close('confirmed')">Usuń</button>
	`,
	styles: ['.confirmation-modal__text { margin: 20px 20px 0; text-align:center; font-size: 18px; }']
})

export class ConfirmationModal {

	constructor(public dialogRef: MatDialogRef<ConfirmationModal>) { }

	onNoClick(): void {
		this.dialogRef.close()
	}
}
