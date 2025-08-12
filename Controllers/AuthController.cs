using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdamyCourse.Model.DTOs;
using UdamyCourse.Repositories;

namespace UdamyCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                if(identityResult.Succeeded)
                {
                    return Ok(new { Message = "User registered successfully" });
                }
                
                
            }

            return BadRequest(new { Message = "User registration failed", Errors = identityResult.Errors.Select(e => e.Description) });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);
            if(user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {

                    //Get Roles
                    var roles = await _userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        //Token
                        var jwtToken = _tokenRepository.createJWTToken(user, roles.ToList());

                        var response = new LoginRespionseDto
                        {
                            jwtToken = jwtToken,
                            username = user.UserName,
                        };
                        return Ok(response);

                    }
                    
                }

            }

            return BadRequest("username or password is incorrect");
        }
    }
}
