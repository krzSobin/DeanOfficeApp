import { Address } from './Address'

export class Student {
	recordBookNumber?: number
	firstName: string
	lastName: string
	email: string
	pesel: number
	currentSemester: number
	enrollmentDate: string
	address?: Address
	addresses?: Address[]
}
