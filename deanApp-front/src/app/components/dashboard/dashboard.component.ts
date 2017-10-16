import { Component } from '@angular/core'
import { AuthService } from '../../auth/auth.service'

@Component({
	selector: 'dashboard-component',
	templateUrl: './dashboard.component.html',
    styleUrls: [ './dashboard.component.scss' ]
})

export class DashboardComponent {
    constructor(private auth: AuthService) {}
    
    logout(): void {
        this.auth.logout()
    }
}
