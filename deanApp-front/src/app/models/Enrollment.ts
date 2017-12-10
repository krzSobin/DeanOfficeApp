import { Lecture } from './Lecture'
import { Grade } from './Grade'

export class Enrollment {
	id: number
	lectureId: number
	lecture?: Lecture
	studentRecordBookNumber: number
	lectureName: string
	studentName: string
	studentLastName: string
	grades: Grade[]
}
