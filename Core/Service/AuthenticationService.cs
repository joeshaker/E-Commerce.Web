using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Execptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDto;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email)?? throw new UserNotFoundException(loginDto.Email);

            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);

            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    Token = CreateTokenAsync(User)
                };

            }
            else
            {
                throw new UnauthorizedException();
            }
        }

        private static string CreateTokenAsync(ApplicationUser user)
        {
            return "Token-ToDo";
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(User, registerDto.Password);

            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    Token = CreateTokenAsync(User)
                };
            }
            else
            {
                var errors= result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }

        }
    }
}
