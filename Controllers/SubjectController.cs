using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school.Models.DTos;
using school.Services.Iservices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController(ISubject subjectService) : ControllerBase
    {
        [HttpGet]
        [Authorize]
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

        [HttpGet("{subjectCode}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetSubjectByCode(string subjectCode)
        {
            var (data, errors) = await subjectService.GetSubjectBySubjectCode(subjectCode);
            if(errors is not null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:errors,Data:null);
                return NotFound(errorResponse);
            }

            var successResponse = new ResponseDto(Success:true,Message:"",Data:data);
            return Ok(successResponse);
        }

        [HttpPut("{subjectCode}")]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> UpdateSubject(string subjectCode,AddSubjectDto addSubject)
        {
            var (results, errors) = await subjectService.UpdateSubject(subjectCode, addSubject);

            if (errors is not null)
            {
                var errorResponse = new ResponseDto(Success: false, Message: errors, Data: null);
                return NotFound(errorResponse);
            }
            var successResponse = new ResponseDto(Success: true, Message: "Subject updated successfully!!", Data: results);
            return Ok(successResponse);

        }

        [HttpDelete("{subjectCode}")]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> DeleteSubject(string subjectCode)
        {
            var (success, errors) = await subjectService.DeleteSubject(subjectCode);

            if (errors is not null)
            {
                var errorResponse = new ResponseDto(Success: false, Message: errors, Data: null);
                return NotFound(errorResponse);
            }

            var successResponse = new ResponseDto(Success:true,Message:success,Data: success);
            return Ok(successResponse);

        }
    }
}
