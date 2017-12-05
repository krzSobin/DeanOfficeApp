import { Component, ViewEncapsulation } from '@angular/core'
import { MatMenuModule, MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MAT_SNACK_BAR_DATA } from '@angular/material'
import { Router } from '@angular/router'

import { Subscription } from 'rxjs/Subscription'

import { AuthService } from './auth/auth.service'

import { User } from './models/User'

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
	encapsulation: ViewEncapsulation.None
})

export class AppComponent {
	public user: User
	public router: Router
	private passwordModal: MatDialogRef<PasswordModal>
	public title = 'Dean Office App'

	public subscription: Subscription

	constructor(private auth: AuthService, private _router: Router, public modal: MatDialog) {
		this.router = _router
		let name = window.localStorage.getItem('name')
		if (name && !name.trim().length) {
			name = 'Administrator'
		}
		this.user = {
			firstName: window.localStorage.getItem('firstName'),
			lastName: window.localStorage.getItem('lastName'),
			role: window.localStorage.getItem('role'),
			name: name
		}
		this.subscription = auth.loggedIn$.subscribe(name => {
			if (name && name.trim().length) {
				this.user.name = name
			} else {
				this.user.name = 'Administrator'
			}
		})
	}

	logout(): void {
		this.auth.logout()
	}

	openPasswordModal(): void {
		this.passwordModal = this.modal.open(PasswordModal)
	}
}


@Component({
	selector: 'password-modal',
	template: `
	<h2 mat-dialog-title class="modal__title">Zmiana hasła</h2>
	<form #passwordForm="ngForm" class="modal__form" (ngSubmit)="changePassword(passwordForm.form)">
		<mat-form-field class="modal__form-field--oldPassword">
			<input matInput type="password" name="oldPassword" class="" placeholder="Obecne hasło" [ngModel]="" required>
		</mat-form-field>
		<mat-form-field>
			<input matInput type="password" name="newPassword" class="" placeholder="Nowe hasło" [ngModel]="" required #newPassword="ngModel" validateEqual="confirmPassword" reverse="true">
		</mat-form-field>
		<mat-form-field>
			<input matInput type="password" name="confirmPassword" class="" placeholder="Powtórz nowe hasło" [ngModel]="" #confirmPassword="ngModel" required validateEqual="newPassword" reverse="false">
			<mat-error *ngIf="confirmPassword.dirty && confirmPassword.invalid">
				Podane hasła są różne
			</mat-error>
		</mat-form-field>

		<button mat-raised-button class="modal__button modal__button--center" color="primary" [disabled]="passwordForm.pristine || passwordForm.invalid">Zmień</button>
	</form>
	`
})

export class PasswordModal {

	constructor(public dialogRef: MatDialogRef<PasswordModal>, private auth: AuthService, public snackbar: MatSnackBar) { }

	onNoClick(): void {
		this.dialogRef.close()
	}

	changePassword(form): void {

		this.auth.changePassword(form.value)
			.then(() => {
				this.snackbar.open('Pomyślnie zmieniono hasło!', 'OK', {
					duration: 5000
				})
				this.dialogRef.close()
			})
			.catch(() => {
				console.log('error')
			})
	}
}
