using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Framework
{
    public interface IBaseRepository<T>
    {

        T GetById(int id);
        string Delete(T entity);
        string Update(T entity);
        T Add(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(int numberOfObjectsPerPage, int pageNumber, string orderbyf);
    }
}
