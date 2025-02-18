using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Utilities
{
    public class GenerateCourseId
    {
        private readonly AppDbContext dbContext;
        public GenerateCourseId(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> GenerateId()
        {
            int returnedId = 0;
            while (true)
            {
                var random = new Random();

                int id = random.Next(100, 999);

                if(await dbContext.Courses.AnyAsync(x => x.Id == id))
                {
                    continue;
                }
                else
                {
                    returnedId = id;
                    break;
                }    
            }
            return returnedId;
        }
    }
}
