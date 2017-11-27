import { Lecture } from './Lecture'

export class Enrollment {
	id: number
	lectureId: number
	lecture?: Lecture
	studentRecordBookNumber: number
	lectureName: string
	studentName: string
	studentLastName: string
	grades: string
}
