using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared;

namespace services
{
    public class AuthService(UserManager<AppUser> userManager) : IAuthService
    {
     

     

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var flag = await userManager.FindByEmailAsync(registerDto.Email);
            if (flag is not null) throw new UnAuthorizedException();

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description);
                throw new ValidationException(errors);
            }
            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTokenAsync(user),
            };
        }

       async Task<UserResultDto> IAuthService.LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthorizedException();

            var flag = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!flag) throw new UnAuthorizedException();

            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token =await GenerateJwtTokenAsync(user),
            };
        }
        private async Task<string> GenerateJwtTokenAsync(AppUser user) {

            // Header
            // Payload
            // Signature

            var authClaim = new List<Claim>() {

            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            };

        var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaim.Add(item: new Claim(ClaimTypes.Role, role));
                    }



            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("STAYSTRONGSTAYSTRONGSTAYSTRONGSTAYSTRONGSTAYSTRONGSTAYSTRONGSTAYSTRONGSTAYSTRONG")); 

            var token = new JwtSecurityToken(
            issuer: "https://localhost:7035",
            audience: "MyAudienc",
            claims: authClaim,
            expires: DateTime.UtcNow.AddDays(value: 5),
            signingCredentials: new SigningCredentials(secretkey , SecurityAlgorithms.HmacSha256Signature)

            );

            return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
