using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUser userServices) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginDTO loginDTO)
        {
            var results = await userServices.LoginUserAsync(loginDTO);

            if (results is null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:"Invalid email or password", Data:results);
                return BadRequest(errorResponse);
            }

            var responseDto = new ResponseDto(Success:true,Message:"Login successful",Data:results);
            return Ok(responseDto);

        }
    }
}
