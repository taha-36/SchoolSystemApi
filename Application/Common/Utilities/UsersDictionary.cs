using Application.Features.Users.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Utilities
{
    public static class UsersDictionary
    {
        public static readonly Dictionary<Title, Func<User>> TitleToEntity = new()
        {
            {Title.Student,() => new Student()},
            {Title.Teacher,() => new Teacher()},
            {Title.TeacherAssistant,() => new TeacherAssistant()},
            {Title.Other,() => new User()}
        };
    }
}
