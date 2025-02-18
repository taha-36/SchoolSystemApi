using Application.Common.Models;
using Application.Features.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public record RegisterTeacherAssistantCommand(RegisterTeacherAssistantDto dto) : IRequest<Result<GetUserDto>>;
}
