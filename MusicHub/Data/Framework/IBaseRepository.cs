using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Framework
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {

        T GetById(int id);
        //T Add(T entity);
        //IQueryable<T> GetAll();
        //IQueryable<T> GetAll(int numberOfObjectsPerPage, int pageNumber, string orderbyf);

        /// <summary>
        /// Gets all objects from database
        /// </summary>
        IQueryable<T> All();

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        IQueryable<T> Filter<Key>(Expression<Func<T, bool>> filter,
            out int total, int index = 0, int size = 50);

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        T Find(params object[] keys);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        T Create(T t);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>        
        void Delete(T t);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        int Update(T t);

        /// <summary>
        /// Get the total objects count.
        /// </summary>
        int Count { get; }

        //
        //
        //
        //
        /*           ASYNC METHODS          */
        //
        //
        //
        //

        /// <summary>
        /// Gets all objects from database
        /// </summary>
        Task<IQueryable<T>> AllAsync();

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        Task<IQueryable<T>> FilterAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        Task<bool> ContainsAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        Task<T> FindAsync(params object[] keys);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        Task<T> CreateAsync(T t);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        Task<int> UpdateAsync(T t);
    }
}
