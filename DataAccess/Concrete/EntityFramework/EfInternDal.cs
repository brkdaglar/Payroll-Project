﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfInternDal: EfEntityRepositoryBase<Intern,EmployeeContext>,IInternDal
    {
        public List<InternDetailDto> GetInternDetails()
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                var result = from i in context.Interns
                    join s in context.SoftwareDevelopers
                        on i.MentorId equals s.Id
                    select new InternDetailDto
                    {
                        Id = i.Id,
                        MentorId = s.Id,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        MentorName = s.FirstName + " " + s.LastName,
                        Wage = i.Wage
                    };
                return result.ToList();
            }
        }
    }
}