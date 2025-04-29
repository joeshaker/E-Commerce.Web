using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Execptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDto;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration) : IAuthenticationService
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
                    Token = await CreateTokenAsync(User)
                };

            }
            else
            {
                throw new UnauthorizedException();
            }
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
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                var errors= result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }

        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new (ClaimTypes.Email,user.Email!),
                new (ClaimTypes.Name,user.UserName!),
                new (ClaimTypes.NameIdentifier,user.Id!),
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
