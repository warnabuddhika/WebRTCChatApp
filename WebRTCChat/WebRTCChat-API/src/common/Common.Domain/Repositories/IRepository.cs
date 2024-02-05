using Common.Domain.Entities;
using System.Linq.Expressions;

namespace Common.Domain.Repositories;

    public interface IRepository<TRoot> where TRoot : class, IRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<TRoot?> GetAsync(Expression<Func<TRoot, bool>> predicate, CancellationToken cancellationToken = default);

        Task<List<TRoot>> GetAllAsync(Expression<Func<TRoot, bool>>? predicate, CancellationToken cancellationToken = default);

        Task<TRoot> InsertAsync(TRoot entity, CancellationToken cancellationToken = default);
        Task<TRoot> UpdateAsync(TRoot entity, CancellationToken cancellationToken = default);
        Task<TRoot> RemoveAsync(TRoot entity, CancellationToken cancellationToken = default);
        Task<bool> GetAnyAsync(Expression<Func<TRoot, bool>> predicate, CancellationToken cancellationToken = default);
    }
