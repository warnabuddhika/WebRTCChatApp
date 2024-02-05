namespace UserManagement.Infrastructure.Interfaces;
using System.Threading.Tasks;
using UserManagement.Domain.Interfaces;

public interface IUnitOfWork
{
	IUserRepository UserRepository { get; }
	Task<bool> Complete();
	bool HasChanges();
}
