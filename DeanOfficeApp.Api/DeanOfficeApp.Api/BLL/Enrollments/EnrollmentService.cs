using AutoMapper;
using DeanOfficeApp.Api.BLL.Validation.Enrollments;
using DeanOfficeApp.Api.DAL;
using DeanOfficeApp.Api.DAL.Enrollments;
using DeanOfficeApp.Api.DAL.Lectures;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts.Enrollments;
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

        public IEnumerable<GetEnrollmentDTO> GetEnrollments()
        {
            var enrollments = _repository.GetEnrollments();

            return Mapper.Map<IEnumerable<GetEnrollmentDTO>>(enrollments);
        }

        public IEnumerable<GetEnrollmentDTO> GetEnrollmentsOfLecture(int lectureId)
        {
            var enrollments = _repository.GetEnrollmentsOfLecture(lectureId);

            return Mapper.Map<IEnumerable<GetEnrollmentDTO>>(enrollments);
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
                StudentId = student.RecordBookNumber
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
    }
}