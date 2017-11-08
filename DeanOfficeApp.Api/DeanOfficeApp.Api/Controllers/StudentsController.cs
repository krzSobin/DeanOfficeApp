using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Api.DAL;
using System.Collections.Generic;
using DeanOfficeApp.Contracts;
using DeanOfficeApp.Api.BLL;
using DeanOfficeApp.Contracts.Students;

namespace DeanOfficeApp.Api.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly static Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IStudentRepository _repository;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private StudentService studentService;
        private readonly CustomUserStore _store;
        private readonly CustomRoleStore _roleStore;

        public StudentsController()
        {
            var context = new ApplicationDbContext();
            _repository = new StudentRepository(context);
            _store = new CustomUserStore(context);
            _roleStore = new CustomRoleStore(context);
        }

        public StudentsController(IStudentRepository studentRepository)
        {
            _repository = studentRepository;
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager ?? new ApplicationRoleManager(_roleStore); }
            private set { roleManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager ?? new ApplicationUserManager(_store); }
            private set { userManager = value; }
        }

        public StudentService StudentService
        {
            get { return studentService ?? new StudentService(UserManager, RoleManager, _repository, _store, _roleStore); }
            private set { studentService = value; }
        }


        // GET: api/Students
        [Authorize(Roles = "student")]
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<GetStudentDTO>))]
        public IEnumerable<GetStudentDTO> GetStudents()
        {
            _logger.Info("test logowania");
            return StudentService.GetStudents();
        }

        // GET: api/Students/5
        [Authorize(Roles = "Student")]
        [Route("{id:int}", Name="GetStudent")]
        [HttpGet]
        [ResponseType(typeof(GetStudentDTO))]
        public IHttpActionResult GetStudent(int id)
        {
            var student = StudentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST: api/Students
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(GetStudentDTO))]
        public async Task<IHttpActionResult> PostStudentAsync(CreateStudentDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await StudentService.AddStudentAsync(student);
            if (!result.Created)
            {
                return BadRequest("Adding student error. Try again.");
            }
            return CreatedAtRoute("GetStudent", new { id = result.Student.RecordBookNumber }, result.Student);
        }

        // PUT: api/Students/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(UpdateStudentResultDTO))]
        public async Task<IHttpActionResult> PutStudentAsync(int id, UpdateStudentDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.RecordBookNumber)
            {
                return BadRequest();
            }

            var updateResult = await StudentService.UpdateStudentAsync(student);

            if (updateResult.Updated)
            {
                return Ok(updateResult);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Students/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(DeleteStudentResultDTO))]
        public async Task<IHttpActionResult> DeleteStudentAsync(int id)
        {
            var result = await StudentService.DeleteStudentAsync(id);
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
