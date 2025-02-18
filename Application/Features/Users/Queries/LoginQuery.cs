using Application.Common.Models;
using Application.Features.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public record LoginQuery(LoginRequestDto loginDto) : IRequest<Result<string>>;
}
