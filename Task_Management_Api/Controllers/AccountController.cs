using Microsoft.AspNetCore.Mvc;
using Task_Management_Api.DTOs;
using Task_Management_Api.Services.Interfaces;

namespace Task_Management_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _cauntService;

        public AccountController(IAccountService acauntService)
        {
            _cauntService = acauntService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequestDto request)
        {
            var response = await _cauntService.Login(request);
            if (response == null)
            {
                return Unauthorized("Email or password incorrect");
            }
            return Ok(response);
        }
    }
}