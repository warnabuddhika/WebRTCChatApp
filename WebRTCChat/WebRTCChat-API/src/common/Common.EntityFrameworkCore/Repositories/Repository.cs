using Common.Domain.Entities;
using Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.EntityFrameworkCore.Repositories;

    public abstract class Repository<TRoot> : IRepository<TRoot> where TRoot : class, IRoot
    {
        private readonly CommonDbContext _dbContext;

        protected abstract IQueryable<TRoot> IncludeAll(IQueryable<TRoot> query);

        public IUnitOfWork UnitOfWork => _dbContext;

        protected Repository(CommonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TRoot> InsertAsync(TRoot entity, CancellationToken cancellationToken = default)
        {
            var savedEntity = await _dbContext.Set<TRoot>().AddAsync(entity, cancellationToken);
            return savedEntity.Entity;
        }

        public async Task<TRoot?> GetAsync(Expression<Func<TRoot, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            TRoot? entity = await IncludeAll(_dbContext.Set<TRoot>())
                .Where(predicate)
                .FirstOrDefaultAsync(cancellationToken);

            return entity;
        }

        public async Task<List<TRoot>> GetAllAsync(Expression<Func<TRoot, bool>>? predicate,
            CancellationToken cancellationToken = default)
        {
            if (predicate == null)
            {
                List<TRoot> allEntities = await IncludeAll(_dbContext.Set<TRoot>())
                    .ToListAsync(cancellationToken);

                return allEntities;
            }

            List<TRoot> filteredEntities = await IncludeAll(_dbContext.Set<TRoot>())
                .Where(predicate)
                .ToListAsync(cancellationToken);

            return filteredEntities;
        }
		

	public Task<TRoot> UpdateAsync(TRoot entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Attach(entity);

            var updated = _dbContext.Update(entity).Entity;

            return Task.FromResult(updated);
        }

        public Task<TRoot> RemoveAsync(TRoot entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Attach(entity);

            var removed = _dbContext.Remove(entity).Entity;

            return Task.FromResult(removed);
        }

        public async Task<bool> GetAnyAsync(Expression<Func<TRoot, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            bool filteredEntities = await _dbContext.Set<TRoot>().AnyAsync(predicate, cancellationToken);
            return filteredEntities;
        }
    }