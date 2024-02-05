namespace Messaging.Domain.Entities;

using Common.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public interface IMessageRepository : IRepository<Message>
{
}
