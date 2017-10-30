using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DeanOfficeApp.Api.Models;
using NLog;
using DeanOfficeApp.Api.DAL.Teachers;
using DeanOfficeApp.Api.BLL.Teachers;
using DeanOfficeApp.Contracts;
using DeanOfficeApp.Contracts.Teachers;

namespace DeanOfficeApp.Api.Controllers
{
    [RoutePrefix("api/teachers")]
    public class TeachersController : ApiController
    {
        private readonly static Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ITeacherRepository _repository;
        private ApplicationUserManager userManager;
        private TeacherService teacherService;
        private readonly CustomUserStore _store;

        public TeachersController()
        {
            var context = new ApplicationDbContext();
            _repository = new TeacherRepository(context);
            _store = new CustomUserStore(context);
        }

        public TeachersController(ITeacherRepository teacherRepository)
        {
            _repository = teacherRepository;
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager ?? new ApplicationUserManager(_store); }
            private set { userManager = value; }
        }

        public TeacherService TeacherService
        {
            get { return teacherService ?? new TeacherService(UserManager, _repository, _store); }
            private set { teacherService = value; }
        }

        // GET: api/Teachers
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<GetTeacherDTO>))]
        public IEnumerable<GetTeacherDTO> GetTeachers()
        {
            _logger.Info("test logowania");
            return TeacherService.GetTeachers();
        }

        // GET: api/Teachers/5
        [Route("{id:int}", Name = "GetTeacher")]
        [HttpGet]
        [ResponseType(typeof(GetTeacherDTO))]
        public async Task<IHttpActionResult> GetTeacher(int id)
        {
            var teacher = TeacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // POST: api/Teachers
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(GetTeacherDTO))]
        public async Task<IHttpActionResult> PostTeacher(CreateTeacherDTO teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await TeacherService.AddTeacher(teacher);
            if (!result.Created)
            {
                return BadRequest("Adding teacher error. Try again.");
            }

            return CreatedAtRoute("GetTeacher", new { id = result.Teacher.TeacherId }, result.Teacher);
        }

        // PUT: api/Teachers/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(UpdateTeacherResultDTO))]
        public async Task<IHttpActionResult> PutTeacher(int id, UpdateTeacherDTO teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }

            var updateResult = await TeacherService.UpdateTeacherAsync(teacher);

            if (updateResult.Updated)
            {
                return Ok(updateResult);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Teachers/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(DeleteTeacherResultDTO))]
        public async Task<IHttpActionResult> DeleteTeacherAsync(int id)
        {
            var result = await TeacherService.DeleteTeacherAsync(id);
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
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}