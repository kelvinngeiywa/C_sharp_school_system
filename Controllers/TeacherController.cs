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
    }
}
