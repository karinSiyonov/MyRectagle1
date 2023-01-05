using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRectagle1.Shared.Lecturers;
using TriangleDbRepository;

namespace MyRectagle1.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LearningController : ControllerBase
	{
        private readonly DbRepository _db;
        public LearningController(DbRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetLearning()
        {
            object param = new { };
            string query = "SELECT ID, IsExcellence, Grade FROM StudendsDetails";

            var records = await _db.GetRecordsAsync<Learning>(query, param);
            Learning EXM = records.FirstOrDefault();
            return Ok(EXM);
        }

        [HttpPost]
        public async Task<IActionResult> InsertLearning (Learning Learning)
        {
            object newLearning = new
            {
                FullName = Learning.FullName,
                ID = Learning.ID,   
                Grade = 0,
                IsExcellence = false,
                
            };
            string insertQuery = "INSERT INTO StudendsDetails (FullName,ID,Grade,IsExcellence) VALUES (@FullName, @ID, @Grade, @IsExcellence)";

            int newId = await _db.InsertReturnId(insertQuery, newLearning);
            if (newId != 0)
            {
                object param = new
                {
                    id = newId
                };
                string query = "SELECT ID, FullName ,Grade,IsExcellence FROM StudendsDetails";
                var records = await _db.GetRecordsAsync<LecturerNamesID>(query, param);

                LecturerNamesID createdLearn = records.FirstOrDefault();

                return Ok(createdLearn);
            }
            return BadRequest("Learning not created");
        }





    }

}
