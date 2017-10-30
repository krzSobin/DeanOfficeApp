using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using AutoMapper;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts;
using DeanOfficeApp.Contracts.Students;
using DeanOfficeApp.Contracts.Teachers;

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
                cfg.CreateMap<CreateStudentDTO, Student>()
                .ForMember(dest => dest.UserData, opt => opt.Ignore())
                .ForMember(dest => dest.RecordBookNumber, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
                
                cfg.CreateMap<Student, GetStudentDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(s => s.UserData.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(s => s.UserData.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(s => s.UserData.Email));




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
            });
        }
    }
}
