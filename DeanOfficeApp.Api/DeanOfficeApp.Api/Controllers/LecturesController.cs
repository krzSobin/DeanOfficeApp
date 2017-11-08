using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DeanOfficeApp.Api.Models;
using NLog;
using DeanOfficeApp.Api.DAL.Lectures;
using DeanOfficeApp.Api.BLL.Lectures;
using DeanOfficeApp.Contracts.Lectures;

namespace DeanOfficeApp.Api.Controllers
{
    [RoutePrefix("api/lectures")]
    public class LecturesController : ApiController
    {
        private readonly static Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ILectureRepository _repository;
        private LectureService lectureService;

        public LecturesController()
        {
            var context = new ApplicationDbContext();
            _repository = new LectureRepository(context);
        }

        public LecturesController(ILectureRepository studentRepository)
        {
            _repository = studentRepository;
        }

        public LectureService LectureService
        {
            get { return lectureService ?? new LectureService(_repository); }
            private set { lectureService = value; }
        }

        // GET: api/Lectures
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<GetLectureDTO>))]
        public IEnumerable<GetLectureDTO> GetLectures()
        {
            return LectureService.GetLectures();
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
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LectureExists(int id)
        {
            return _repository.LectureExists(id);
        }
    }
}