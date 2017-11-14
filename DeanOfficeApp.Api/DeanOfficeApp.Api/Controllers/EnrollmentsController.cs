using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Api.DAL.Enrollments;
using DeanOfficeApp.Api.BLL.Enrollments;
using DeanOfficeApp.Contracts.Enrollments;
using System.Web;
using Microsoft.AspNet.Identity;
using DeanOfficeApp.Api.DAL;
using DeanOfficeApp.Api.DAL.Lectures;

namespace DeanOfficeApp.Api.Controllers
{
    [RoutePrefix("api/enrollments")]
    public class EnrollmentsController : ApiController
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILectureRepository _lectureRepository;
        private EnrollmentService enrollmentService;
        private ApplicationDbContext db = new ApplicationDbContext();

        public EnrollmentsController()
        {
            var context = new ApplicationDbContext();
            _enrollmentRepository = new EnrollmentRepository(context);
            _lectureRepository = new LectureRepository(context);
            _studentRepository = new StudentRepository(context);
        }

        public EnrollmentsController(IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository, ILectureRepository lectureRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _lectureRepository = lectureRepository;
        }

        public EnrollmentService EnrollmentService
        {
            get { return enrollmentService ?? new EnrollmentService(_enrollmentRepository, _studentRepository, _lectureRepository); }
            private set { enrollmentService = value; }
        }

        // GET: api/Enrollments
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<GetEnrollmentDTO>))]
        public IEnumerable<GetEnrollmentDTO> GetEnrollments()
        {
            return EnrollmentService.GetEnrollments();
        }

        // GET: api/Enrollments/5
        [Route("{id:int}", Name = "GetEnrollment")]
        [HttpGet]
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult GetEnrollment(int id)
        {
            var enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(enrollment);
        }

        // PUT: api/Enrollments/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnrollment(int id, Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrollment.Id)
            {
                return BadRequest();
            }

            db.Entry(enrollment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Enrollments
        [Authorize(Roles ="student")]
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(CreateEnrollmentResultDTO))]
        public IHttpActionResult PostEnrollment(CreateEnrollmentDTO enrollmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            //tu walidacja userId o ile podane

            var result = EnrollmentService.AddEnrollment(enrollmentDTO.LectureId, userId);
            if (result.Created)
            {
                return CreatedAtRoute("GetEnrollment", new { id = result.Enrollment.Id }, result);
            }
            if (!string.IsNullOrWhiteSpace(result.Error))
                return BadRequest(result.Error);
            
            return BadRequest("Creating not succeed");
        }

        // DELETE: api/Enrollments/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult DeleteEnrollment(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            db.Enrollments.Remove(enrollment);
            db.SaveChanges();

            return Ok(enrollment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnrollmentExists(int id)
        {
            return db.Enrollments.Count(e => e.Id == id) > 0;
        }
    }
}