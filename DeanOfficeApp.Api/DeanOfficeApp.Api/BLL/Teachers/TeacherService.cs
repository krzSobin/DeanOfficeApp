using AutoMapper;
using DeanOfficeApp.Api.DAL.Teachers;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts;
using DeanOfficeApp.Contracts.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DeanOfficeApp.Api.BLL.Teachers
{
    public class TeacherService
    {
        private ApplicationUserManager _userManager;
        private readonly ITeacherRepository _repository;
        private readonly CustomUserStore _store;

        public TeacherService(ApplicationUserManager userManager, ITeacherRepository repository, CustomUserStore store)
        {
            _userManager = userManager;
            _repository = repository;
            _store = store;
        }

        public async Task<NewTeacherResultDTO> AddTeacher(CreateTeacherDTO teacherDTO)
        {
            var createResult = new NewTeacherResultDTO
            {
                Created = false,
                Teacher = null
            };
            var user = new ApplicationUser { UserName = teacherDTO.Email, Email = teacherDTO.Email, CreatedDate = DateTime.Now, FirstName = teacherDTO.FirstName, LastName = teacherDTO.LastName, EmailConfirmed = true };

            var result = await _userManager.CreateAsync(user, teacherDTO.Pesel.ToString() + teacherDTO.LastName.ToLower().Substring(0, Math.Min(teacherDTO.LastName.Length, 3)));
            var saveUserResult = _store.Context.SaveChanges();
            if (!result.Succeeded && saveUserResult < 1)
            {
                return createResult;
            }

            var teacher = Mapper.Map<Teacher>(teacherDTO);
            teacher.UserId = user.Id;

            var createdTeacher = _repository.InsertTeacher(teacher);
            if (_repository.Save())
            {
                createResult.Created = true;
                createResult.Teacher = Mapper.Map<GetTeacherDTO>(createdTeacher);

                return createResult;
            }

            return createResult;
        }

        public IEnumerable<GetTeacherDTO> GetTeachers()
        {
            var teachersEntities = _repository.GetTeachers();

            return Mapper.Map<IEnumerable<GetTeacherDTO>>(teachersEntities);
        }

        public GetTeacherDTO GetTeacherById(int id)
        {
            var teacherEntity = _repository.GetTeacherByID(id);

            return Mapper.Map<GetTeacherDTO>(teacherEntity);
        }

        public async Task<DeleteTeacherResultDTO> DeleteTeacherAsync(int id)
        {
            var teacher = _repository.GetTeacherByID(id);
            if (teacher == null)
                return null;

            _repository.DeleteTeacher(teacher);
            var deleteTeacherResult = new DeleteTeacherResultDTO
            {
                DataDeleted = false,
                AccountDeleted = false,
                Teacher = Mapper.Map<GetTeacherDTO>(teacher)
            };

            if (_repository.Save())
            {
                var result = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(teacher.UserId));
                if (result.Succeeded)
                    deleteTeacherResult.AccountDeleted = true;
                deleteTeacherResult.DataDeleted = true;
            }

            return deleteTeacherResult;
        }

        public async Task<UpdateTeacherResultDTO> UpdateTeacherAsync(UpdateTeacherDTO teacher)
        {
            var updateResult = new UpdateTeacherResultDTO
            {
                Updated = false,
                Teacher = null
            };

            var teacherEntity = _repository.GetTeacherByID(teacher.TeacherId);

            var user = await _userManager.FindByIdAsync(teacherEntity.UserId);
            user.FirstName = string.IsNullOrWhiteSpace(teacher.FirstName) ? user.FirstName : teacher.FirstName;
            user.LastName = string.IsNullOrWhiteSpace(teacher.LastName) ? user.LastName : teacher.LastName;


            //TODO: zrobić ładniejszy kod
            teacherEntity.Degree = teacher.Degree;
            teacherEntity.Pesel = teacher.Pesel;
            teacherEntity.Position = teacher.Position;
            teacherEntity.Room = teacher.Room;
            teacherEntity.UserId = user.Id;
            teacherEntity.UserData = user;
            //var newTeacher = Mapper.Map<Teacher>(teacher);
            //newTeacher.UserId = user.Id;
            //newTeacher.UserData = user;

            updateResult.Teacher = Mapper.Map<GetTeacherDTO>(teacherEntity);


            _repository.UpdateTeacher(teacherEntity);
            if (_repository.Save())
            {
                updateResult.Updated = true;
                return updateResult;
            }

            return updateResult;
        }
    }
}