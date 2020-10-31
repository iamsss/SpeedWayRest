using Microsoft.AspNetCore.Mvc;
using SpeedWayRest.Contracts;
using SpeedWayRest.Controllers.V1.Requests;
using SpeedWayRest.Controllers.V1.Responses;
using SpeedWayRest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWayRest.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityServices _identityServices;
        public IdentityController(IIdentityServices identityServices)
        {
            _identityServices = identityServices;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
        {

            var authResposne = await _identityServices.RegisterAsync(request.Email, request.Password);

            if (!authResposne.Success)
            {
                return BadRequest(new AuthFailResponse
                {
                    Errors =  authResposne.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResposne.Token
            });
        }
    }
}
