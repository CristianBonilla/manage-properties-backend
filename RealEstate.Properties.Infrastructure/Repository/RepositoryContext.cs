using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealEstate.Properties.Contracts.Repository;

namespace RealEstate.Properties.Infrastructure.Repository
{
    /// <inheritdoc cref="IRepositoryContext{TContext}"/>
    public class RepositoryContext<TContext> : IRepositoryContext<TContext> where TContext : DbContext
    {
        readonly TContext context;
        bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryContext{TContext}"/> class
        /// </summary>
        /// <param name="context">Context</param>
        public RepositoryContext(TContext context) => this.context = context;

        /// <inheritdoc/>
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return context.Entry(entity);
        }

        /// <inheritdoc/>
        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return context.Set<TEntity>();
        }

        /// <inheritdoc/>
        public int Save() => context.SaveChanges();

        /// <inheritdoc/>
        public Task<int> SaveAsync() => context.SaveChangesAsync();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs defined tasks to release unmanaged resources,
        /// according to the boolean received to do so
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                context.Dispose();

            disposed = true;
        }
    }
}
