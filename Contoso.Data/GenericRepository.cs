using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Contoso.Model.Common;
using Contoso.Utility;

namespace Contoso.Data
{
    public abstract class GenericRepository<T> : IRepository<T> where T : Entity
    {
        protected ContosoDbContext _dbContext;

        protected GenericRepository(ContosoDbContext context)
        {
            _dbContext = context;
        }

        public virtual void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbContext.Set<T>().Where(where).AsEnumerable();
            foreach (var obj in objects)
                _dbContext.Set<T>().Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return _dbContext.Set<T>().Where(where).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking().ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbContext.Set<T>().Where(where).ToList();
        }


        public IQueryable<T> GetQueryable()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetPagedList(out int totalCount, int? page = null, int? pageSize = null,
                                           Expression<Func<T, bool>> filter = null, string[] includePaths = null,
                                           params SortExpression<T>[] sortExpressions)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (filter != null)
                query = _dbContext.Set<T>().Where(filter);

            totalCount = query.Count();

            if (includePaths != null)
                for (var i = 0; i < includePaths.Count(); i++)
                    query = query.Include(includePaths[i]);

            if (sortExpressions != null)
            {
                IOrderedQueryable<T> orderedQuery = null;
                for (var i = 0; i < sortExpressions.Count(); i++)
                    if (i == 0)
                    {
                        if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                            orderedQuery = query.OrderBy(sortExpressions[i].SortBy);
                        else
                            orderedQuery = query.OrderByDescending(sortExpressions[i].SortBy);
                    }
                    else
                    {
                        if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                            orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
                        else
                            orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
                    }

                if (page != null)
                    query = orderedQuery.Skip(((int) page - 1) * (int) pageSize);
            }


            if (pageSize != null)
                query = query.Take((int) pageSize);

            return query.ToList();
        }

        public PagedResultSet<T> GetPagedData(int currentPage, int pageSize)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            var totalRowCount = query.Count();
            var currentPageIndex = currentPage;
            var skip = (currentPageIndex - 1) * pageSize;
            var data = query.Skip(skip).Take(pageSize).ToList();

            var pagedResult = new PagedResultSet<T>(currentPageIndex, pageSize, totalRowCount, data);
            return pagedResult;
        }
    }
}