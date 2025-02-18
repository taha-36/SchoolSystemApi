using Application.Common.Models;
using Application.Common.Utilities;
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
    public class AddCourseHandler : IRequestHandler<AddCourseCommand, Result<GetCourseDto>>
    {
        private readonly AppDbContext dbContext;
        private readonly PasswordHasher<User> Hasher = new PasswordHasher<User>();
        public AddCourseHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Result<GetCourseDto>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            Result<GetCourseDto> Result;
            List<DepartmentCourse> allowedDepartments = new List<DepartmentCourse>();
            Course course = new Course()
            {
                Name = request.dto.Name,
                CreditHours = request.dto.CreditHours,
            };
            foreach (var id in request.dto.AllowedDepartmentsId)
            {
                var dep = await dbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
                if(dep == null)
                {
                    Result = new Result<GetCourseDto>
                    {
                        Success = false,
                        Value = null,
                        Message = "Invalid Department Id"
                    };
                    return Result;
                }
                DepartmentCourse _dep = new DepartmentCourse()
                {
                    Id = new Guid(),
                    Department = dep,
                    DepartmentId = dep.Id,
                    Course = course
                };
                allowedDepartments.Add(_dep);
            }
            var prereqList = new List<Course>();
            if(request.dto.PrerequisitesId != null && request.dto.PrerequisitesId.Any())
            {
                foreach (var id in request.dto.PrerequisitesId)
                {
                    var preq = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
                    if (preq == null)
                    {
                        Result = new Result<GetCourseDto>
                        {
                            Success = false,
                            Value = null,
                            Message = "Invalid Prerequisite Id"
                        };
                        return Result;
                    }
                    if(preq != null)
                        prereqList.Add(preq);
                }
            }
            course.AllowedDepartments = allowedDepartments;
            course.Prerequisites = prereqList;
            foreach(var dep in allowedDepartments)
            {
                dep.Department.DepartmentCourses.Add(dep);
            }
            await dbContext.AddAsync(course);
            await dbContext.SaveChangesAsync();
            var returned = new GetCourseDto
            {
                CourseName = course.Name,
                CourseId = course.Id,
                CreditHours = course.CreditHours
            };
            if(course.Prerequisites.Any())
            {
                foreach(var pre in course.Prerequisites)
                {
                    returned.prerequisitesNames.Add(pre.Name);
                }
            }
            Result = new Result<GetCourseDto>()
            {
                Success = true,
                Value = returned,
                Message = "Course added successfully"
            };
            return Result;
        }
    }
}
