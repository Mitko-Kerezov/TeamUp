﻿using TeamUp.Data.Repositories;
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

        public IGenericRepository<TeamUpUser> Users
        {
            get
            {
                return this.GetRepository<TeamUpUser>();
            }
        }

        public IGenericRepository<ChatMessage> ChatMessages
        {
            get
            {
                return this.GetRepository<ChatMessage>();
            }
        }

        public IGenericRepository<Category> Category
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IGenericRepository<Skill> Skills
        {
            get
            {
                return this.GetRepository<Skill>();
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
