using AutoMapper;
using DeanOfficeApp.Api.BLL.Validation.Enrollments;
using DeanOfficeApp.Api.DAL;
using DeanOfficeApp.Api.DAL.Enrollments;
using DeanOfficeApp.Api.DAL.Lectures;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts.Enrollments;
using DeanOfficeApp.Contracts.Grades;
using System;
using System.Collections.Generic;

namespace DeanOfficeApp.Api.BLL.Enrollments
{
    public class EnrollmentService
    {
        private readonly IEnrollmentRepository _repository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILectureRepository _lectureRepository;

        public EnrollmentService(IEnrollmentRepository repository, IStudentRepository studentRepository, ILectureRepository lectureRepository)
        {
            _repository = repository;
            _studentRepository = studentRepository;
            _lectureRepository = lectureRepository;
        }

        public IEnumerable<GetEnrollmentDTO> GetEnrollments(int userId, string role)
        {
            IEnumerable<Enrollment> enrollments = new List<Enrollment>();
            switch (role)
            {
                case "student":
                    enrollments = _repository.GetEnrollments(userId);
                    break;
                case "admin":
                    enrollments = _repository.GetEnrollments();
                    break;
            }

            return Mapper.Map<IEnumerable<GetEnrollmentDTO>>(enrollments);
        }

        public IEnumerable<GetEnrollmentDTO> GetEnrollmentsOfLecture(int lectureId)
        {
            var enrollments = _repository.GetEnrollmentsOfLecture(lectureId);

            return Mapper.Map<IEnumerable<GetEnrollmentDTO>>(enrollments);
        }

        public AddGradeResultDTO AddGrade(int enrollmentId, AddGradeDTO gradeDTO)
        {
            var result = new AddGradeResultDTO
            {
                Added = false,
                Grade = null
            };

            var enrollment = _repository.GetEnrollmentById(enrollmentId);

            var grade = Mapper.Map<Grade>(gradeDTO);
            grade.Enrollement = enrollment;
            if (grade.GradeValueId.HasValue)
            {
                grade.GradeValue = _repository.GetGradeValueById((int)grade.GradeValueId);
            }
            else
            {
                grade.GradeValue = null;
            }


            var addedGrade = _repository.InsertGrade(grade);
            if (_repository.Save())
            {
                result.Added = true;
                result.Grade = Mapper.Map<GetGradeDTO>(addedGrade);
            }

            return result;
        }

        public CreateEnrollmentResultDTO AddEnrollment(int lectureId, int userId)
        {
            var result = new CreateEnrollmentResultDTO(false, "", null);

            var student = _studentRepository.GetStudentByUserId(userId);
            if (student == null)
            {
                result.Error = "Student not found";

                return result;
            }
            var lecture = _lectureRepository.GetLectureByID(lectureId);
            if (lecture == null)
            {
                result.Error = "Lecture not found";

                return result;
            }

            var enrollment = new Enrollment
            {
                EnrollmentDate = DateTime.Now,
                Lecture = lecture,
                LectureId = lecture.LectureId,
                Student = student,
                StudentId = student.RecordBookNumber,
                Grades = new List<Grade>()
            };

            var validationResult = EnrollmentsValidator.Validate(enrollment);
            if (!validationResult.IsValid)
            {
                result.Error = validationResult.Message;

                return result;
            }

            var createdEnrollment = _repository.InsertEnrollment(enrollment);
            if (_repository.Save())
            {
                result.Created = true;
                result.Enrollment = Mapper.Map<GetEnrollmentDTO>(createdEnrollment);
            }

            return result;
        }

        public IEnumerable<GetGradeValueDTO> GetGradeValues()
        {
            var grades = _repository.GetGradeValues();

            return Mapper.Map<IEnumerable<GetGradeValueDTO>>(grades);
        }
    }
}