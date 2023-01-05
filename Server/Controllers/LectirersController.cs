using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRectagle1.Shared.Lecturers;
using TriangleDbRepository;

namespace MyRectagle1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectirersController : ControllerBase
    {
        private readonly DbRepository _db;

        public LectirersController(DbRepository db)
        {
            _db = db;
        }

        [HttpGet("{checkMail}")]
        public async Task<IActionResult> GetLecturerExpStudy(string checkMail)
        {
            object param = new
            {
                email = checkMail
            };

            string query = "SELECT FullName, Experience, StudyField FROM Lecturers WHERE Email = @email";

            var records = await _db.GetRecordsAsync<LecturerExpStudy>(query, param);
            LecturerExpStudy lect = records.FirstOrDefault();

            if (lect != null)
            {
                return Ok(lect);
            }
            return BadRequest("not found");

        }

        [HttpGet("All")]
        public async Task<IActionResult> GetLecturersNamesList()
        {
            object param = new { };

            string query = "SELECT ID, FullName FROM Lecturers";

            var records = await _db.GetRecordsAsync<LecturerNamesID>(query, param);

            List<LecturerNamesID> lects = records.ToList();
            return Ok(lects);

        }

        [HttpPost]
        public async Task<IActionResult> InsertLecturer(LecturerToSend lecturer)
        {
            object newLecturer = new
            {
                FullName = lecturer.FullName,
                Email = lecturer.Email,
                Experience = 0,
                CurrentCredits = 0,
                IsExcellence = false,
                StudyField = lecturer.StudyField
            };
            string insertQuery = "INSERT INTO Lecturers (FullName, Email, Experience, CurrentCredits, 			IsExcellence, StudyField) VALUES (@FullName, @Email, @Experience, @CurrentCredits, 				@IsExcellence, @StudyField)";

            int newId = await _db.InsertReturnId(insertQuery, newLecturer);
            if (newId != 0)
            {
                object param = new
                {
                    id = newId
                };
                string query = "SELECT ID, FullName FROM Lecturers WHERE Id=@id";
                var records = await _db.GetRecordsAsync<LecturerNamesID>(query, param);

                LecturerNamesID createdLecturer = records.FirstOrDefault();

                return Ok(createdLecturer);
            }
            return BadRequest("Lecturer not created");
        }




    }
}