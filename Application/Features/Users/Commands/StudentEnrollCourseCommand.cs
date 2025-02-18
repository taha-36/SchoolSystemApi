using Application.Common.Models;
using Application.Features.Users.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public record StudentEnrollCourseCommand(StudentEnrollCourseDto dto, Guid studentId) : IRequest<Result<List<GetCourseDto>>>;
}
