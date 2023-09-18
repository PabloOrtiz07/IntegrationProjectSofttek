using AlkemyUmsa.Infrastructure;
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

        /// <summary>
        /// Log in using email and password.
        /// </summary>
        /// <param name="dto">
        /// **A model containing your login credentials.**</param>
        /// <returns>Returns an "Ok" response with your data and an authorization token</returns>
        /// 

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateDto dto)
        {
            var userCredentials = await _unitOfWork.UserRepository.AuthenticateCredentials(dto);
            if (userCredentials is null) ResponseFactory.CreateErrorResponse(401, "The credentials are incorrect");

            var token = _tokenJwtHelper.GenerateToken(userCredentials);

            var user = new UserLoginDTO()
            {
                FirstName = userCredentials.FirstName,
                LastName = userCredentials.LastName,
                Email = userCredentials.Email,
                Token = token
            };


            return ResponseFactory.CreateSuccessResponse(200, user);

        }
    }
}