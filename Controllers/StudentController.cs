using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IStudent studentService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> AddStudent(AddStudentDTO studentDTO)
        {
            var (results, error) = await studentService.AddStudentAsync(studentDTO);

            if(error is not null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:error,Data:null);
                return BadRequest(errorResponse);
            }

            var successResponse = new ResponseDto(true,"",results);
            return Ok(successResponse);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetAllStudents()
        {
            var results = await studentService.GetAllStudents();

            if(results is null)
            {
                var errorResponse = new ResponseDto(false, "Error occured", results);
                return BadRequest(errorResponse);
            }

            var successResponse = new ResponseDto(true,"",results);
            return Ok(successResponse);
        }

        [HttpGet("{**regNumber}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetStudentByRegNumber(string regNumber)
        {
            var (results, errors) = await studentService.GetStudentByRegNumber(regNumber);

            if(errors is not null)
            {
                var errorResponse = new ResponseDto(false,errors,null);
                return NotFound(errorResponse);
            }

            var successResponse = new ResponseDto(true,"",results);
            return Ok(successResponse);
        }
    }
}
