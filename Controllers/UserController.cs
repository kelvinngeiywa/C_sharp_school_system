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
        [Authorize(Roles = "SUPER_ADMIN")]
        public async Task<ActionResult<ResponseDto>> GetAllUsers()
        {
            var results = await userServices.getAllUserAsync();
            var response = new ResponseDto(Success:true,Message:"Users found!!",Data:results);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles ="SUPER_ADMIN")]
        public async Task<ActionResult<ResponseDto>> CreateAdmin(CreateUserDto createAdminDto)
        {
            var results = await userServices.CreateAdmin(createAdminDto);
            if(results is null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:"",Data:results);
                return BadRequest(errorResponse);
            }
            var response = new ResponseDto(Success: true, Message: "Admin created!!", Data: results);
            return Ok(response);

        }
    }
}
