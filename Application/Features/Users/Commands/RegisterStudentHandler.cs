using Application.Features.Users.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity;
using Microsoft.AspNetCore.Identity;
using Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Common.Utilities;

namespace Application.Features.Users.Commands
{
    public class RegisterStudentHandler : IRequestHandler<RegisterStudentCommand, Result<GetUserDto>>
    {
        private readonly AppDbContext dbContext;
        private readonly PasswordHasher<User> Hasher = new PasswordHasher<User>();
        public RegisterStudentHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Result<GetUserDto>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
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
            var r = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name == "Student");
            var d = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == request.dto.DepartmentId);
            var newUser = new Student
            {
                Id = Guid.NewGuid(),
                Email = request.dto.Email,
                Name = request.dto.Name,
                Password = Hasher.HashPassword(null!, request.dto.Password),
                Role = r!,
                RoleId = r!.Id,
                DepartmentId = d!.Id,
                Department = d
            };
            d.Students.Add(newUser);
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
