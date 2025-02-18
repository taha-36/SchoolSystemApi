using Application.Common.Models;
using Application.Features.Users.DTOs;
using Domain.Entities;
using Domain.Repos;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class StudentEnrollCourseHandler : IRequestHandler<StudentEnrollCourseCommand, Result<List<GetCourseDto>>>
    {
        private readonly AppDbContext dbContext;
        private readonly PasswordHasher<User> Hasher = new PasswordHasher<User>();
        public StudentEnrollCourseHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Result<List<GetCourseDto>>> Handle(StudentEnrollCourseCommand request, CancellationToken cancellationToken)
        {
            Result<List<GetCourseDto>> Result;
            var student = dbContext.Students.FirstOrDefault(x => x.Id == request.studentId);
            var returned = new List<GetCourseDto>();
            if(student == null)
            {
                Result = new Result<List<GetCourseDto>>
                {
                    Success = false,
                    Value = null,
                    Message = "Invalid User"
                };
                return Result;
            }
            foreach(var c in request.dto.Courses)
            {
                var course = dbContext.Courses.FirstOrDefault(x => x.Id == c.CourseId);
                if(course == null)
                {
                    Result = new Result<List<GetCourseDto>>
                    {
                        Success = false,
                        Value = null,
                        Message = "Invalid Course Id"
                    };
                    return Result;
                }
                if(student.Courses.Any(x => x.Course == course))
                {
                    Result = new Result<List<GetCourseDto>>
                    {
                        Success = false,
                        Value = null,
                        Message = "Course Already Enrolled"
                    };
                    return Result;
                }
                if(!course!.AllowedDepartments.Any(x => x.DepartmentId == student.DepartmentId))
                {
                    Result = new Result<List<GetCourseDto>>
                    {
                        Success = false,
                        Value = null,
                        Message = "Not Eligable for This Course"
                    };
                    return Result;
                }
                foreach(var p in course.Prerequisites)
                {
                    if(!student.Courses.Any(x => x.CourseId == p.Id))
                    {
                        Result = new Result<List<GetCourseDto>>
                        {
                            Success = false,
                            Value = null,
                            Message = "Missing Prerequisites"
                        };
                        return Result;
                    }
                }
                var studentCourse = new StudentCourse
                {
                    Id = Guid.NewGuid(),

                    CourseId = course.Id,
                    Course = course,

                    StartTime = new TimeSpan(2, 30, 15),
                    EndTime = new TimeSpan(4, 30, 15),

                    Student = student,
                    StudentId = student.Id,

                    CourseStatus = CourseStatus.InProgress,
                    Group = c.Group
                };
                course.Students.Add(studentCourse);
                student.Courses.Add(studentCourse);
                await dbContext.StudentsCourses.AddAsync(studentCourse);
                await dbContext.SaveChangesAsync();
                var returnedCourse = new GetCourseDto
                {
                    CourseName = course.Name,
                    CourseId = course.Id,
                    CreditHours = course.CreditHours,
                    prerequisitesNames = course.Prerequisites.Select(x => x.Name).ToList(),
                    StartTime = studentCourse.StartTime,
                    EndTime = studentCourse.EndTime
                };
                returned.Add(returnedCourse);
            }
            Result = new Result<List<GetCourseDto>>
            {
                Success = true,
                Value = returned,
                Message = "Successfully Enrolled Courses!"
            };
            return Result;
        }
    }
}
