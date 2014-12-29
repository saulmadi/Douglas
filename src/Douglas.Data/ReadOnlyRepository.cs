using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Douglas.Domain;
using Douglas.Domain.Entities;
using Douglas.Domain.Exceptions;
using Douglas.Domain.Services;
using NHibernate;
using NHibernate.Linq;

namespace Douglas.Data
{
    public class ReadOnlyRepository : IReadOnlyRepository
    {
        readonly ISession _session;

        public ReadOnlyRepository(ISession session)
        {
            _session = session;
        }

        #region IRepository Members

        public T First<T>(Expression<Func<T, bool>> query) where T : class, IEntity
        {
            T item = _session.QueryOver<T>().Where(query).List().FirstOrDefault();

            if (item == null)
            {
                throw new ItemNotFoundException<T>();
            }

            return item;
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> query) where T : class, IEntity
        {
            T item = _session.QueryOver<T>().Where(query).List().FirstOrDefault();
            return item;
        }

        public T GetById<T>(Guid id) where T : IEntity
        {
            var item = _session.Get<T>(id);

            if (item == null)
            {
                throw new ItemNotFoundException<T>(id);
            }

            return item;
        }

        public IEnumerable<T> GetAll<T>() where T : IEntity
        {
            var items = _session.Query<T>();
            return items;
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> expression) where T : IEntity
        {
            var session = _session;
            return session.Query<T>().Where(expression);
        }

        #endregion
    }
}