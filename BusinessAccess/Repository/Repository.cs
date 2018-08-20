using DataAccess.DBContext;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BusinessAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SampleNetCoreAPIContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(SampleNetCoreAPIContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public T Get(int id, bool isActive = true)
        {
            return entities.FirstOrDefault(s => s.Id == id && (s.Active || !isActive));
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return entities.Where(filter);
        }

        public void Insert(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;

            entities.Add(entity);

            if (saveChange)
                context.SaveChanges();

        }

        public void Update(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.UpdatedTime = DateTime.Now;
            if (saveChange)
                context.SaveChanges();
        }

        public void Delete(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            if (saveChange)
                context.SaveChanges();
        }
    }
}
