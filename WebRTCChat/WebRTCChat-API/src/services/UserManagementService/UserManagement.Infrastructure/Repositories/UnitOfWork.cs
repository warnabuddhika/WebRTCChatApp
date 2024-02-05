namespace UserManagement.Infrastructure.Repositories;

using AutoMapper;
using System.Threading.Tasks;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Interfaces;

public class UnitOfWork : IUnitOfWork
{
	UserDbContext _context;
	IMapper _mapper;

	public UnitOfWork(UserDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public IUserRepository UserRepository => new UserRepository(this._context, this._mapper);


	public async Task<bool> Complete()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public bool HasChanges()
	{
		return _context.ChangeTracker.HasChanges();
	}
}
