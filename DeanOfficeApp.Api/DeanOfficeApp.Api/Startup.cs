using Microsoft.Owin;
using Owin;
using AutoMapper;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts;
using DeanOfficeApp.Contracts.Students;
using DeanOfficeApp.Contracts.Teachers;
using DeanOfficeApp.Contracts.Lectures;
using DeanOfficeApp.Contracts.Enrollments;
using DeanOfficeApp.Contracts.Grades;

[assembly: OwinStartup(typeof(DeanOfficeApp.Api.Startup))]

namespace DeanOfficeApp.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);

            Mapper.Initialize(cfg => {

                //Students mapping
                cfg.CreateMap<CreateStudentDTO, Student>()
                .ForMember(dest => dest.UserData, opt => opt.Ignore())
                .ForMember(dest => dest.RecordBookNumber, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
                
                cfg.CreateMap<Student, GetStudentDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(s => s.UserData.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(s => s.UserData.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(s => s.UserData.Email));


                //Teachers mapping
                cfg.CreateMap<Teacher, GetTeacherDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(s => s.UserData.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(s => s.UserData.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(s => s.UserData.Email));

                cfg.CreateMap<UpdateTeacherDTO, Teacher>()
                .ForMember(dest => dest.UserData, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

                cfg.CreateMap<CreateTeacherDTO, Teacher>()
                .ForMember(dest => dest.UserData, opt => opt.Ignore())
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());


                //Lectures mapping
                cfg.CreateMap<Lecture, GetLectureDTO>()
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(s => s.Teacher.UserData.FirstName + " " + s.Teacher.UserData.LastName));

                cfg.CreateMap<GetLectureDTO, Lecture>()
                .ForMember(dest => dest.LectureId, opt => opt.Ignore());

                cfg.CreateMap<NewLectureDTO, Lecture>()
                .ForMember(dest => dest.Teacher, opt => opt.Ignore())
                .ForMember(dest => dest.LectureId, opt => opt.Ignore());

                //Enrollments mapping
                cfg.CreateMap<Enrollment, GetEnrollmentDTO>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(s => s.Student.UserData.FirstName))
                .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(s => s.Student.UserData.LastName))
                .ForMember(dest => dest.LectureName, opt => opt.MapFrom(s => s.Lecture.Name))
                .ForMember(dest => dest.StudentRecordBookNumber, opt => opt.MapFrom(s => s.Student.RecordBookNumber))
                .ForMember(dest => dest.LectureId, opt => opt.MapFrom(s => s.Lecture.LectureId));


                //grades
                cfg.CreateMap<GradeValue, GetGradeValueDTO>();

                cfg.CreateMap<Grade, GetGradeDTO>()
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(g => g.GradeValue.Value));

                cfg.CreateMap<AddGradeDTO, Grade>()
                .ForMember(dest => dest.Enrollement, opt => opt.Ignore())
                .ForMember(dest => dest.GradeValue, opt => opt.Ignore());
            });
        }
    }
}
