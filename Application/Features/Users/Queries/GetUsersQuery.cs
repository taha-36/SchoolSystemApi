using Application.Common.Models;
using Application.Features.Users.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public record GetUsersQuery : IRequest<Result<List<GetUserDto>>>;
}
