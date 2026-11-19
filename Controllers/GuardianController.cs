using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardianController(IGuardian guardianServices) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> AddGuardian(AddGuardianDto addGuardian)
        {
            var (results,errors) = await guardianServices.AddGuardianAsync(addGuardian);

            if(errors is not null)
            {
                var errorResponse = new ResponseDto(Success: false, Message: errors, Data: null);
                return BadRequest(errorResponse);
            }

            var successResponse = new ResponseDto(Success:true,Message:"Guardian added",Data:results);
            return Ok(successResponse);
        }

        [HttpGet]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> GetAllGuardians()
        {
            var results = await guardianServices.GetAllGuardians();
            if(results is null)
            {
                var errorResponse = new ResponseDto(Success:false,Message:"Error occured",Data:results);
                return BadRequest(errorResponse);
            }

            var successResponse = new ResponseDto(Success:true,Message:"",Data:results);
            return Ok(successResponse);
        }
    }
}
