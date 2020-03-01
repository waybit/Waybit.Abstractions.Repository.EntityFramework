using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.EntityFramework
{
	/// <inheritdoc />
	public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
		where TKey : IEquatable<TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		private readonly DbContext _dbContext;

		/// <summary>
		/// Initialize instance of <see cref="Repository{TEntity,TKey}"/>
		/// </summary>
		/// <param name="dbContext">Entity framework database context</param>
		protected Repository(DbContext dbContext)
		{
			_dbContext = dbContext 
				?? throw new ArgumentNullException(nameof(dbContext));
		}

		/// <inheritdoc />
		public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
		{
			List<TEntity> entities = await _dbContext
				.Set<TEntity>()
				.ToListAsync(cancellationToken);

			return entities;
		}

		/// <inheritdoc />
		public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
		{
			TEntity entity = await _dbContext
				.Set<TEntity>()
				.FindAsync(cancellationToken, id);

			return entity;
		}

		/// <inheritdoc />
		public virtual async Task<TKey> AddAsync(TEntity entity, CancellationToken cancellationToken)
		{
			TEntity added = _dbContext
				.Set<TEntity>()
				.Add(entity);

			return added.Id;
		}

		/// <inheritdoc />
		public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			TEntity updating = await _dbContext
				.Set<TEntity>()
				.FindAsync(cancellationToken, entity.Id);
			
			_dbContext.Entry(updating).CurrentValues.SetValues(updating);
		}
	}
}
