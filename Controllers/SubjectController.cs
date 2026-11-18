using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController(ISubject subjectService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllSubjects()
        {
            var results = await subjectService.GetAllSubjects();
            if ( results is null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:"Subjects not found",Data:results);
                return NotFound(errorResponse);
            }

            var successResponse = new ResponseDto(Success:true,Message:"Subjects found",Data:results);
            return Ok(successResponse);
        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> AddSubject(AddSubjectDto addSubjectDto)
        {
            var results = await subjectService.AddSubjectAsync(addSubjectDto);

            if(results is null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:"subject name or code exists!!",Data:results);
                return BadRequest(errorResponse);
            }

            var successResponse = new ResponseDto(Success:true,Message:"Subject added",Data:results);
            return Ok(successResponse);
        }
    }
}
