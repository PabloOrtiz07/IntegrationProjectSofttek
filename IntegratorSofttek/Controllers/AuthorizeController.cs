using IntegratorSofttek.DTOs;
using IntegratorSofttek.Helper;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class AuthorizeController : ControllerBase
    {
        private TokenJwtHelper _tokenJwtHelper;
        private readonly IUnitOfWork _unitOfWork;
        public AuthorizeController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _tokenJwtHelper = new TokenJwtHelper(configuration);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateDto dto)
        {
            var userCredentials = await _unitOfWork.UserRepository.AuthenticateCredentials(dto);
            if (userCredentials is null) return Unauthorized("The credentials are incorrect");

            var token = _tokenJwtHelper.GenerateToken(userCredentials);

            var user = new UserLoginDTO()
            {
                FirstName = userCredentials.FirstName,
                LastName = userCredentials.LastName,
                Email = userCredentials.Email,
                Token = token
            };


            return Ok(user);

        }
    }
}