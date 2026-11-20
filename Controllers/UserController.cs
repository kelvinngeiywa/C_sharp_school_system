using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUser userServices) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetAllUsers()
        {
            var results = await userServices.getAllUserAsync();
            var response = new ResponseDto(Success:true,Message:"Users found!!",Data:results);
            return Ok(response);
        }

        [HttpGet("admins")]
        public async Task<ActionResult<ResponseDto>> GetAllAdminUsers()
        {
            var results = await userServices.GetAllAdminUsers();

            if(results is null)
            {
                var errorResponse = new ResponseDto(Success: false, Message: "Admin users not found", Data: results);
                return BadRequest(errorResponse);
            }
            var responseDto = new ResponseDto(Success:true,Message:"Admin users found!",Data:results);
            return Ok(responseDto);
        }

        [HttpPost("create-account")]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> CreateUserAccount(CreateUserCredentialsDTO createUserCredentials)
        {
            var (results, errors) = await userServices.CreateUserCredentials(createUserCredentials);

            if(errors is not null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:errors,Data:null);
                return BadRequest(errorResponse);
            }

            var successResponse = new ResponseDto(Success:true,Message:"",Data:results);
            return Ok(successResponse);

        }

        [HttpPost]
        [Authorize(Roles ="SUPER_ADMIN")]
        public async Task<ActionResult<ResponseDto>> CreateAdmin(CreateUserDto createAdminDto)
        {
            var results = await userServices.CreateAdmin(createAdminDto);
            if(results is null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:"Admin creation failed, check if admin role exists or if admin with entered email or phone number exists",Data:results);
                return BadRequest(errorResponse);
            }
            var response = new ResponseDto(Success: true, Message: "Admin created!!", Data: results);
            return Ok(response);

        }
    }
}
