using AutoMapper;
using DeanOfficeApp.Api.DAL;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts;
using DeanOfficeApp.Contracts.Students;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DeanOfficeApp.Api.BLL
{
    public class StudentService
    {
        private ApplicationUserManager _userManager;
        private readonly IStudentRepository _repository;
        private readonly CustomUserStore _store;

        public StudentService(ApplicationUserManager userManager, IStudentRepository repository, CustomUserStore store)
        {
            _userManager = userManager;
            _repository = repository;
            _store = store;
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
            var saveUserResult = _store.Context.SaveChanges();
            if (!result.Succeeded && saveUserResult < 1)
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
            var studentEntities = _repository.GetStudents();

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