using System;
using System.Collections.Generic;
using System.Linq;
using Dt.EscritorioProxy.Backend.Interfaces.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dt.EscritorioProxy.Backend.Repository.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext Context;

        protected DbSet<T> Entities;


        protected BaseRepository(DbContext context)
        {
            Context = context;
            Entities = context.Set<T>();
        }


        public IEnumerable<T> GetAll()
        {
            return Entities.AsEnumerable();

        }

        public T GetById(long id)
        {
            return Context.Find<T>(id);
        }


        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Context.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            
            Context.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Context.Remove(entity);
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Context.Remove(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }


    }
}
