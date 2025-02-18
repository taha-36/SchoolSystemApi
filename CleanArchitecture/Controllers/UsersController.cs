using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.Users.Queries;
using Application.Features.Users.DTOs;
using Application.Features.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Domain.Entities;

namespace SchoolApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator med;

        public UsersController(IMediator mediator)
        {
            med = mediator;
        }

        [Authorize(Roles = "SystemUser")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await med.Send(new GetUsersQuery());
            return Ok(users);
        }

        [Authorize(Roles = "SystemUser")]
        [HttpPost("RegisterStudent")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentDto userDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await med.Send(new RegisterStudentCommand(userDto));
            if(result.Success == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Roles = "SystemUser")]
        [HttpPost("RegisterSystemUser")]
        public async Task<IActionResult> RegisterSystemUser([FromBody] RegisterSystemUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await med.Send(new RegisterSystemUserCommand(userDto));
            if (result.Success == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Roles = "SystemUser")]
        [HttpPost("RegisterTeacher")]
        public async Task<IActionResult> RegisterTeacherUser([FromBody] RegisterTeacherDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await med.Send(new RegisterTeacherCommand(userDto));
            if (result.Success == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Roles = "SystemUser")]
        [HttpPost("RegisterTeacherAssistant")]
        public async Task<IActionResult> RegisterTeacherAssistantUser([FromBody] RegisterTeacherAssistantDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await med.Send(new RegisterTeacherAssistantCommand(userDto));
            if (result.Success == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await med.Send(new LoginQuery(loginDto));
            if(result.Success == true)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [Authorize]
        [HttpPost("EnrollCourses")]
        public async Task<IActionResult> StudentEnroll([FromBody] StudentEnrollCourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            Guid studentId = Guid.Parse(id);
            var result = await med.Send(new StudentEnrollCourseCommand(dto, studentId));

            return Ok(result);
        }
        [Authorize]
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await med.Send(new AddCourseCommand(dto));

            return Ok(result);
        }
    }
}
