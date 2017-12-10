using DeanOfficeApp.Api.BLL.Validation.Enrollments.Models;
using DeanOfficeApp.Api.Models;

namespace DeanOfficeApp.Api.BLL.Validation.Enrollments
{
    public static class EnrollmentsValidator
    {
        public static BussinessLogicValidationResult Validate(Enrollment enrollment)
        {
            var result = new BussinessLogicValidationResult
            {
                IsValid = true,
                Message = ""
            };

            if (enrollment.Lecture.MinimalSemester > enrollment.Student.CurrentSemester)
            {
                result.IsValid = false;
                result.Message = "Lecture minimal semester is too high";
            }

            return result;
        }
    }
}