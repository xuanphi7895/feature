using System.Security.AccessControl;
using System.Security.Claims;
using System.Net.Http;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.ComponentModel;
using System.Net.Mime;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs;
using Web.Errors;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Web.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
           _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault( x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }
        //email exsists
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email){
            return await _userManager.FindByEmailAsync(email) != null;
        }

        // //address
        // [HttpGet("address")]
        // public async Task<ActionResult<string>> GetUserAddress([FromQuery] string email){
        //     var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        //     var user = await _userManager.FindByEmailAsync(email);
        //     return user.Address;
        // }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
            //var user = await _userManager.FindByEmailAsync(registerDto.Email);
            //if (user != null) return Unauthorized(new ApiResponse(401));
           var user = new AppUser {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                DisplayName = registerDto.DisplayName

            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto {
                Email = user.Email,
                Token =  _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }
    }
}