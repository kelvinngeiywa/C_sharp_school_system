using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController(ITeacher teacherService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> AddTeacher([FromBody]AddTeacherDto addTeacher)
        {
            var results = await teacherService.AddTeacherAsync(addTeacher);
            if(results is null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:"Teacher with email or phoneNumber exists",Data:results);
                return BadRequest(errorResponse);
            }

            var responseDto = new ResponseDto(Success:true,Message:"Teacher added successfully",Data:results);
            return Ok(responseDto);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllTeachers()
        {
            var results = await teacherService.GetAllTeachersAsync();
            //if(results is null)
            //{
            //    var errorResponse = new ResponseDto(Success: false, Message: "Add teacher failed", Data: results);
            //    return 
            //}
            var responseDto = new ResponseDto(Success: true, Message: "", Data: results);
            return Ok(responseDto);

        }

        [HttpPost("{teacherId}/assign-subject")]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> AssignSubject(Guid teacherId,AssignSubjectDto assignSubject)
        {
            var (results,errors) = await teacherService.AssignTeacherSubject(teacherId, assignSubject);

            if (errors is not null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:errors,Data:null);
                return BadRequest(errorResponse);
            }

            var successResponse = new ResponseDto(Success:true,Message:"Subjects assigned",Data:results);
            return Ok(successResponse);
        }

        [HttpDelete("{teacherId}/delete-subject/{subjectId}")]
        public async Task<ActionResult<ResponseDto>> RemoveSubject(Guid teacherId, Guid subjectId)
        {
            var (results,errors) = await teacherService.RemoveSubject(teacherId, subjectId);

            if (errors is not null)
            {
                var errorResponse = new ResponseDto(Success: false, Message:errors , Data:null );
                return BadRequest(errorResponse);
            }

            var successResponse = new ResponseDto(Success: true, Message: "Subject removed", Data: results);
            return Ok(successResponse);

        }
    }
}
