namespace Messaging.Infrastructure.Repositories;

using Common.Domain.Repositories;
using Messaging.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class MessageRepository : IMessageRepository
{
	public IUnitOfWork UnitOfWork => throw new NotImplementedException();

	public Task<List<Message>> GetAllAsync(Expression<Func<Message, bool>>? predicate, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<bool> GetAnyAsync(Expression<Func<Message, bool>> predicate, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Message?> GetAsync(Expression<Func<Message, bool>> predicate, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Message> InsertAsync(Message entity, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Message> RemoveAsync(Message entity, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Message> UpdateAsync(Message entity, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
