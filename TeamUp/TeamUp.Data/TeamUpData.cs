using TeamUp.Data.Repositories;
using System;
using System.Collections.Generic;
using TeamUp.Models;

namespace TeamUp.Data
{
    public class TeamUpData: ITeamUpData
    {
        private ITeamUpContext context;

        private IDictionary<Type, object> repositories;

        public TeamUpData(ITeamUpContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public ITeamUpContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IGenericRepository<Message> Messages
        {
            get
            {
                return this.GetRepository<Message>();
            }
        }

        public IGenericRepository<TeamUpUser> Users
        {
            get
            {
                return this.GetRepository<TeamUpUser>();
            }
        }

        public IGenericRepository<Skill> Skills
        {
            get
            {
                return this.GetRepository<Skill>();
            }
        }

        public IGenericRepository<ProgrammingCategory> ProgrammingCategories
        {
            get
            {
                return this.GetRepository<ProgrammingCategory>();
            }
        }

        public IGenericRepository<Project> Projects
        {
            get
            {
                return this.GetRepository<Project>();
            }
        }
       
        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
