using AutoMapper;
using DeanOfficeApp.Api.DAL;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts;
using DeanOfficeApp.Contracts.Students;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace DeanOfficeApp.Api.BLL
{
    public class StudentService
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly IStudentRepository _repository;
        private readonly CustomUserStore _store;
        private readonly CustomRoleStore _roleStore;

        public StudentService(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IStudentRepository repository, CustomUserStore store, CustomRoleStore roleStore)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repository = repository;
            _store = store;
            _roleStore = roleStore;
        }
        
        public async Task<NewStudentResultDTO> AddStudentAsync(CreateStudentDTO studentDTO)
        {
            var createResult = new NewStudentResultDTO
            {
                Created = false,
                Student = null
            };
            var user = new ApplicationUser() { UserName = studentDTO.Email, Email = studentDTO.Email, CreatedDate = DateTime.Now, FirstName = studentDTO.FirstName, LastName = studentDTO.LastName, EmailConfirmed = true };

            var result = await _userManager.CreateAsync(user, studentDTO.Pesel.ToString()+ studentDTO.LastName.ToLower().Substring(0, Math.Min(studentDTO.LastName.Length, 3)));
            //var saveUserResult = _store.Context.SaveChanges();
            var roleResult = await _userManager.AddToRoleAsync(user.Id, "student");
            if (!result.Succeeded || !roleResult.Succeeded)
            {
                return createResult;
            }

            var student = Mapper.Map<Student>(studentDTO);
            student.UserId = user.Id;

            var createdStudent = _repository.InsertStudent(student);
            if (_repository.Save())
            {
                createResult.Created = true;
                createResult.Student = Mapper.Map<GetStudentDTO>(createdStudent);

                return createResult;
            }

            return createResult;
        }

        public IEnumerable<GetStudentDTO> GetStudents()
        {
            var studentEntities = _repository.GetStudents(ConfigurationManager.ConnectionStrings["DeanOffice"].ConnectionString);

            return Mapper.Map<IEnumerable<GetStudentDTO>>(studentEntities);
        }

        public GetStudentDTO GetStudentById(int id)
        {
            var studentEntity = _repository.GetStudentByID(id);

            return Mapper.Map<GetStudentDTO>(studentEntity);
        }

        public async Task<DeleteStudentResultDTO> DeleteStudentAsync(int id)
        {
            var student = _repository.GetStudentByID(id);
            if (student == null)
                return null;

            _repository.DeleteStudent(student);
            var deleteStudentResult = new DeleteStudentResultDTO
            {
                DataDeleted = false,
                AccountDeleted = false,
                Student = Mapper.Map<GetStudentDTO>(student)
            };

            if (_repository.Save())
            {
                var result = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(student.UserId));
                if (result.Succeeded)
                    deleteStudentResult.AccountDeleted = true;
                deleteStudentResult.DataDeleted = true;
            }

            return deleteStudentResult;
        }

        public async Task<UpdateStudentResultDTO> UpdateStudentAsync(UpdateStudentDTO student)
        {
            var updateResult = new UpdateStudentResultDTO
            {
                Updated = false,
                Student = null
            };

            var studentEntity = _repository.GetStudentByID(student.RecordBookNumber);

            var user = await _userManager.FindByIdAsync(studentEntity.UserId);
            user.FirstName = string.IsNullOrWhiteSpace(student.FirstName) ? user.FirstName : student.FirstName;
            user.LastName = string.IsNullOrWhiteSpace(student.LastName) ? user.LastName : student.LastName;


            //TODO: zrobić ładniejszy kod
            studentEntity.CurrentSemester = student.CurrentSemester;
            studentEntity.Pesel = student.Pesel;
            studentEntity.EnrollmentDate = student.EnrollmentDate;
            studentEntity.UserId = user.Id;
            studentEntity.UserData = user;
            //var newStudent = Mapper.Map<Student>(student);
            //newStudent.UserId = user.Id;
            //newStudent.UserData = user;

            updateResult.Student = Mapper.Map<GetStudentDTO>(studentEntity);


            _repository.UpdateStudent(studentEntity);
            if (_repository.Save())
            {
                updateResult.Updated = true;
                return updateResult;
            }

            return updateResult;
        }
    }
}