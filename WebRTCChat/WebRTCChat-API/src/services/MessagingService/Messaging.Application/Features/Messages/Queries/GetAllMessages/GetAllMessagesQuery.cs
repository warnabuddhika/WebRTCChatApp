namespace Messaging.Application.Features.Messages.Queries.GetAllMessages;

using MediatR;
using Messaging.Application.Features.Messages.ViewModels;
using Messaging.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetAllMessagesQuery :IRequest<List<MessageViewModel>?>
{
}
