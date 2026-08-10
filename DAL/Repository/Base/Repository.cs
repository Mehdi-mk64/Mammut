using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Entities.Base;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {

        #region Properties & Fields

        protected readonly AppDbContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        #endregion

        #region Constructor

        public Repository(AppDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
        }

        #endregion

        #region Async Methods

        #region Add

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }


        public virtual async Task AddAsync<T>(T entity, CancellationToken cancellationToken, bool saveNow = true) where T : class
        {
            Assert.NotNull(entity, nameof(entity));

            await DbContext.Set<T>().AddAsync(entity, cancellationToken).ConfigureAwait(false);

            if (saveNow)
            {
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

        }

        public virtual async Task AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true) where T : class
        {
            Assert.NotNull(entities, nameof(entities));

            await DbContext.Set<T>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

            if (saveNow)
            {
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }


        #endregion




        #region Update
        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);

        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync<T>(T entity, CancellationToken cancellationToken, bool saveNow = true) where T : class
        {
            Assert.NotNull(entity, nameof(entity));

            DbContext.Set<T>().Update(entity);

            if (saveNow)
            {
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public virtual async Task UpdateRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true) where T : class
        {
            Assert.NotNull(entities, nameof(entities));

            DbContext.Set<T>().UpdateRange(entities);

            if (saveNow)
            {
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

        }


        #endregion



        #region Delete

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }


        public virtual async Task DeleteAsync<T>(T entity, CancellationToken cancellationToken, bool saveNow = true) where T : class
        {
            Assert.NotNull(entity, nameof(entity));

            DbContext.Set<T>().Remove(entity);

            if (saveNow)
            {
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public virtual async Task DeleteRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken, bool saveNow = true) where T : class
        {
            Assert.NotNull(entities, nameof(entities));

            DbContext.Set<T>().RemoveRange(entities);

            if (saveNow)
            {
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion



        #region GetID

        public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return Entities.FindAsync(ids, cancellationToken);
        }


        public virtual ValueTask<T> GetByIdAsync<T>(CancellationToken cancellationToken, params object[] ids) where T : class
        {
            return DbContext.Set<T>().FindAsync(ids, cancellationToken);
        }

        #endregion




        public IQueryable<T> TableOf<T>() where T : class
        {
            return DbContext.Set<T>();
        }

        public IQueryable<T> TableNoTrackingOf<T>() where T : class
        {
            return DbContext.Set<T>().AsNoTracking();
        }

        #endregion




        #region Sync Methods



        #region Add

        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Add(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }


        #endregion


        #region Update


        public virtual void Update(TEntity entity, bool saveNow = true)
        {


            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        #endregion


        #region Delete

        public virtual void Delete(TEntity entity, bool saveNow)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        #endregion



        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

   

 
        #endregion

        #region Attach & Detach

        public virtual void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }

        public virtual void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        #endregion

        #region Explicit Loading

        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);

            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();

        }








        #endregion
    }
}
