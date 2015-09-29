﻿using eLearning.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eLearning.Core.Services
{
    public interface IStudentServicesll
    {
        void Create(Student entity);
        void Delete(Student entity);

        void Update(Student entity);

        IQueryable<Student> Get(Expression<Func<Student, bool>> predication);

    }
}
