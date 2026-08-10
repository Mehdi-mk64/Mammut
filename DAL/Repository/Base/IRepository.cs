using Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        IQueryable<T> TableOf<T>() where T : class;

        IQueryable<T> TableNoTrackingOf<T>() where T : class;

        void Add(TEntity entity, bool saveNow = true);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task AddAsync<T>(T entity, CancellationToken cancellationToken, bool saveNow = true)where T : class;

        void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);

        Task AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true) where T : class;


        void Attach(TEntity entity);
        void Delete(TEntity entity, bool saveNow);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteAsync<T>(T entity, CancellationToken cancellationToken, bool saveNow = true)where T :class;

        void DeleteRange(IEnumerable<TEntity> entities, bool saveNow);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true)where T :class;

        void Detach(TEntity entity);
        TEntity GetById(params object[] ids);
        ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

        ValueTask<T> GetByIdAsync<T>(CancellationToken cancellationToken, params object[] ids) where T:class;

        void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
        Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
        void Update(TEntity entity, bool saveNow = true);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task UpdateAsync<T>(T entity, CancellationToken cancellationToken, bool saveNow = true)where T :class;


        void UpdateRange(IEnumerable<TEntity> entities, bool saveNow);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task UpdateRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true)where T : class;

    }
}