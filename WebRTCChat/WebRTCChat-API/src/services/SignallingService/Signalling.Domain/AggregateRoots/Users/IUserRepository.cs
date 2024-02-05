using Common.Domain.Repositories;
using System.Linq.Expressions;

namespace Users.Domain.AggregateRoots.Users
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
