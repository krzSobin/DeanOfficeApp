using AutoMapper;
using DeanOfficeApp.Api.DAL.Lectures;
using DeanOfficeApp.Api.DAL.Teachers;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts.Lectures;
using System.Collections.Generic;

namespace DeanOfficeApp.Api.BLL.Lectures
{
    public class LectureService
    {
        private readonly ILectureRepository _repository;
        private readonly ITeacherRepository _teacherRepository;

        public LectureService(ILectureRepository repository, ITeacherRepository teacherRepository)
        {
            _repository = repository;
            _teacherRepository = teacherRepository;
        }

        public IEnumerable<GetLectureDTO> GetLectures()
        {
            var lectureEntites = _repository.GetLectures();

            return Mapper.Map<IEnumerable<GetLectureDTO>>(lectureEntites);
        }

        public GetLectureDTO GetLectureById(int id)
        {
            var lectureEntity = _repository.GetLectureByID(id);

            return Mapper.Map<GetLectureDTO>(lectureEntity);
        }

        public NewLectureResultDTO AddLecture(NewLectureDTO lectureDTO)
        {
            var createResult = new NewLectureResultDTO
            {
                Created = false,
                Lecture = null
            };

            var lecture = Mapper.Map<Lecture>(lectureDTO);

            var createdLecture = _repository.InsertLecture(lecture);
            if (_repository.Save())
            {
                createResult.Created = true;
                createResult.Lecture = Mapper.Map<GetLectureDTO>(createdLecture);

                return createResult;
            }

            return createResult;
        }

        public UpdateLectureResultDTO UpdateLecture(UpdateLectureDTO lecture)
        {
            var updateResult = new UpdateLectureResultDTO
            {
                Updated = false,
                Lecture = null
            };

            var lectureEntity = _repository.GetLectureByID(lecture.LectureId);

            //TODO: zrobić ładniejszy kod
            lectureEntity.Name = lecture.Name;
            lectureEntity.EcstsPoints = lecture.EcstsPoints;
            lectureEntity.MinimalSemester = lecture.MinimalSemester;
            lectureEntity.Description = lecture.Description;
            lectureEntity.Bibliography = lecture.Bibliography;
            lectureEntity.TeacherId = lecture.TeacherId;

            if (lecture.TeacherId != null)
                lectureEntity.Teacher = _teacherRepository.GetTeacherByID((int)lectureEntity.TeacherId);

            updateResult.Lecture = Mapper.Map<GetLectureDTO>(lectureEntity);


            _repository.UpdateLecture(lectureEntity);
            if (_repository.Save())
            {
                updateResult.Updated = true;
                return updateResult;
            }

            return updateResult;
        }

        public DeleteLectureResultDTO DeleteLecture(int id)
        {
            var lecture = _repository.GetLectureByID(id);
            if (lecture == null)
                return null;

            var deleteLectureResult = new DeleteLectureResultDTO
            {
                Deleted = false,
                Lecture = Mapper.Map<GetLectureDTO>(lecture)
            };

            _repository.DeleteLecture(lecture);

            if (_repository.Save())
            {
                deleteLectureResult.Deleted = true;
            }

            return deleteLectureResult;
        }
    }
}