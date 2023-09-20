using AlkemyUmsa.Infrastructure;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Helper;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<UsersController> _logger;

        public AuthorizeController(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<UsersController> logger)
        {
            _unitOfWork = unitOfWork;
            _tokenJwtHelper = new TokenJwtHelper(configuration);
            _logger = logger;

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
            try
            {
                var userCredentials = await _unitOfWork.UserRepository.AuthenticateCredentials(dto);
                if (userCredentials is null)
                {
                    return ResponseFactory.CreateErrorResponse(401, "The credentials are incorrect");
                }

                if (userCredentials.IsDeleted != false)
                {
                    return ResponseFactory.CreateErrorResponse(401, "The user is inactive");
                }
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            
            }
 

        }
    }
}