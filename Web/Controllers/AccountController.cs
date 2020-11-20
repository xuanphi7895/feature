using System.Runtime.Intrinsics.X86;
using System.ComponentModel;
using System.Net.Mime;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs;
using Web.Errors;

namespace Web.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
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
                Token = "This is be a token",
                DisplayName = user.DisplayName
            };
        }

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
                Token = "This is be a token",
                DisplayName = user.DisplayName
            };
        }
    }
}