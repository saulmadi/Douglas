using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Douglas.Domain.Entities;

namespace Douglas.Domain.Services
{
    public interface IReadOnlyRepository
    {
        T First<T>(Expression<Func<T, bool>> query) where T : class, IEntity;
        T FirstOrDefault<T>(Expression<Func<T, bool>> query) where T : class, IEntity;
        T GetById<T>(Guid id) where T : IEntity;
        IEnumerable<T> GetAll<T>() where T : IEntity;
        IEnumerable<T> Query<T>(Expression<Func<T, bool>> expression) where T : IEntity;
    }
}