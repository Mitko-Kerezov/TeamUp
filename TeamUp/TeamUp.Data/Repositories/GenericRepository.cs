﻿namespace TeamUp.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private ITeamUpContext context;

        public GenericRepository(ITeamUpContext context)
        {
            this.context = context;
        }

        public IQueryable<T> All()
        {
            return this.context.Set<T>();
        }

        public virtual T GetById(object id)
        {
            return this.context.Set<T>().Find(id);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions)
        {
            return this.All().Where(conditions);
        }

        public void Add(T entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public T Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
            return entity;
        }

        public void Delete(object id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public void Detach(T entity)
        {
            this.ChangeState(entity, EntityState.Detached);
        }

        private void ChangeState(T entity, EntityState state)
        {
            this.context.Set<T>().Attach(entity);
            this.context.Entry(entity).State = state;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
