using Application.Common.Models;
using Application.Features.Users.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class RegisterTeacherAssistantHandler : IRequestHandler<RegisterTeacherAssistantCommand, Result<GetUserDto>>
    {
        private readonly AppDbContext dbContext;
        private readonly PasswordHasher<User> Hasher = new PasswordHasher<User>();
        public RegisterTeacherAssistantHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Result<GetUserDto>> Handle(RegisterTeacherAssistantCommand request, CancellationToken cancellationToken)
        {
            //Initiate The Result Object
            Result<GetUserDto> Result;

            //Check if this email already exists in the database
            if (await dbContext.Users.AnyAsync(x => x.Email == request.dto.Email))
            {
                Result = new Result<GetUserDto>
                {
                    Success = false,
                    Value = null,
                    Message = "Email Already Exists",
                };
                return Result;
            }
            var r = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name == "Staff");
            var newUser = new TeacherAssistant
            {
                Id = Guid.NewGuid(),
                Email = request.dto.Email,
                Name = request.dto.Name,
                Password = Hasher.HashPassword(null!, request.dto.Password),
                Role = r!,
                RoleId = r!.Id,
            };
            r.Users.Add(newUser);
            await dbContext.Users.AddAsync(newUser);
            await dbContext.SaveChangesAsync();
            var returnedUser = new GetUserDto
            {
                Id = newUser.Id,
                Name = newUser.Name,
                RoleName = newUser.Role.Name,
                Email = newUser.Email,
            };

            Result = new Result<GetUserDto>
            {
                Success = true,
                Value = returnedUser,
                Message = "Registered Successfully"
            };
            return Result;
        }
    }
}
