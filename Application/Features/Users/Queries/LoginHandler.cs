using Application.Common.Models;
using Application.Features.Users.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class LoginHandler : IRequestHandler<LoginQuery, Result<string>>

    {
        private readonly AppDbContext dbContext;
        private readonly PasswordHasher<User> Hasher = new PasswordHasher<User>();
        public LoginHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            Result<string> Result;
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.loginDto.Email);
            if (user == null)
            {
                Result = new Result<string>
                {
                    Success = false,
                    Value = string.Empty,
                    Message = "Email Not Registered"
                };
                return await Task.FromResult(Result);
            }
            var passCheck = Hasher.VerifyHashedPassword(null!, user.Password, request.loginDto.Password);
            if(passCheck == PasswordVerificationResult.Success)
            {
                var ResultUser = new GetUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    RoleName = user.Role.Name,
                };
                var claim = new List<Claim>();
                claim.Add(new Claim("Name", user.Name));
                claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claim.Add(new Claim("Role", user.Role.Name));
                claim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("pCd-SYn_KAbGy6h0evY1yjUnd7HkYy8LWE1eUyTG5fQ"));
                var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    claims: claim,
                    issuer: "https://localhost:7166",
                    audience: "https://localhost:7166",
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: sc
                    );
                Result = new Result<string>
                {
                    Success = true,
                    Value = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "Verified"
                };
                return await Task.FromResult(Result);
            }
            else
            {
                Result = new Result<string>
                {
                    Success = false,
                    Value = string.Empty,
                    Message = "Incorrect Password"
                };
                return await Task.FromResult(Result);
            }
        }
    }
}
