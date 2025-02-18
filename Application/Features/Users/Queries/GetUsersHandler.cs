using Application.Common.Models;
using Application.Features.Users.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, Result<List<GetUserDto>>>
    {
        private readonly AppDbContext dbContext;
        public GetUsersHandler(AppDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<Result<List<GetUserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersDto = new List<GetUserDto>();
            var users = dbContext.Students.ToList();
            foreach ( var user in users )
            {
                GetUserDto userDto = new GetUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    RoleName = user.Role.Name,
                    Email = user.Email,
                };
                usersDto.Add(userDto);
            }
            var result = new Result<List<GetUserDto>>
            {
                Success = true,
                Value = usersDto,
                Message = "Successfully Retreived Users"
            };
            return await Task.FromResult(result);
        }
    }
}
