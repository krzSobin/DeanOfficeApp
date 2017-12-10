using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DeanOfficeApp.Api.Models;
using NLog;
using DeanOfficeApp.Api.DAL.Lectures;
using DeanOfficeApp.Api.BLL.Lectures;
using DeanOfficeApp.Contracts.Lectures;
using DeanOfficeApp.Api.DAL.Teachers;
using DeanOfficeApp.Api.DAL.Enrollments;
using DeanOfficeApp.Api.BLL.Enrollments;
using DeanOfficeApp.Api.DAL;
using DeanOfficeApp.Contracts.Enrollments;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Linq;

namespace DeanOfficeApp.Api.Controllers
{
    [RoutePrefix("api/lectures")]
    public class LecturesController : ApiController
    {
        private readonly static Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILectureRepository _lectureRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;
        private LectureService lectureService;
        private EnrollmentService enrollmentService;

        public LecturesController()
        {
            var context = new ApplicationDbContext();
            _lectureRepository = new LectureRepository(context);
            _teacherRepository = new TeacherRepository(context);
            _enrollmentRepository = new EnrollmentRepository(context);
            _studentRepository = new StudentRepository(context);
        }

        public LecturesController(ILectureRepository lectureRepository, ITeacherRepository teacherRepository, IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository)
        {
            _lectureRepository = lectureRepository;
            _teacherRepository = teacherRepository;
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
        }

        public LectureService LectureService
        {
            get { return lectureService ?? new LectureService(_lectureRepository, _teacherRepository, _studentRepository); }
            private set { lectureService = value; }
        }

        public EnrollmentService EnrollmentService
        {
            get { return enrollmentService ?? new EnrollmentService(_enrollmentRepository, _studentRepository, _lectureRepository); }
            private set { enrollmentService = value; }
        }

        // GET: api/Lectures
        [Authorize(Roles = "student, teacher, admin")]
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<GetLectureDTO>))]
        public IEnumerable<GetLectureDTO> GetLectures()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();

            var role = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).FirstOrDefault();

            return LectureService.GetLectures(userId, role);
        }

        // GET: api/Lectures
        [Authorize(Roles = "student")]
        [Route("available")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<GetLectureDTO>))]
        public IEnumerable<GetLectureDTO> GetLecturesAvailableForEnroll()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();

            return LectureService.GetLecturesAvailableForEnroll(userId);
        }

        // GET: api/Lectures/5
        [Route("{id:int}", Name = "GetLecture")]
        [HttpGet]
        [ResponseType(typeof(GetLectureDTO))]
        public IHttpActionResult GetLecture(int id)
        {
            var lecture = LectureService.GetLectureById(id);
            if (lecture == null)
            {
                return NotFound();
            }

            return Ok(lecture);
        }

        // GET: api/Lectures/5/Enrollments
        [Route("{id:int}/Enrollments", Name = "GetEnrollmentsOfLecture")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<GetEnrollmentDTO>))]
        public IHttpActionResult GetEnrollmentsOfLecture(int id)
        {
            var enrollments = EnrollmentService.GetEnrollmentsOfLecture(id);

            return Ok(enrollments);
        }

        // PUT: api/Lectures/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(UpdateLectureResultDTO))]
        public IHttpActionResult PutLecture(int id, UpdateLectureDTO lecture)
        {
            var updateResult = LectureService.UpdateLecture(lecture);

            if (updateResult.Updated)
            {
                return Ok(updateResult);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Lectures
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(NewLectureResultDTO))]
        public IHttpActionResult PostLecture(NewLectureDTO lecture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = LectureService.AddLecture(lecture);
            if (!result.Created)
            {
                return BadRequest("Adding lecture error. Try again.");
            }

            return CreatedAtRoute("GetLecture", new { id = result.Lecture.LectureId }, result.Lecture);
        }

        // DELETE: api/Lectures/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(DeleteLectureResultDTO))]
        public IHttpActionResult DeleteLecture(int id)
        {
            var result = LectureService.DeleteLecture(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _lectureRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LectureExists(int id)
        {
            return _lectureRepository.LectureExists(id);
        }
    }
}