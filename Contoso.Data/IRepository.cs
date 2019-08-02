using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contoso.Model.Common;
using Contoso.Utility;

namespace Contoso.Data
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        void Add(T entity);
        // Marks an entity as modified
        void Update(T entity);
        // Marks an entity to be removed
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        // Get an entity by int id
        T GetById(int id);
        // Get an entity using delegate
        T Get(Expression<Func<T, bool>> where);
        // Gets all entities of type T
        IEnumerable<T> GetAll();
        // Gets entities using delegate
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        //IQueryable entity
        IQueryable<T> GetQueryable();

        int GetCount(Expression<Func<T, bool>> filter = null);
        // Saving the changes to Database
        void SaveChanges();

       
        IEnumerable<T> GetPagedList(out int totalCount, int? page = null, int? pageSize = null,
            Expression<Func<T, bool>> filter = null, string[] includePaths = null,
            params SortExpression<T>[] sortExpressions);

        PagedResultSet<T> GetPagedData(int currentPage, int pageSize);


    }
}