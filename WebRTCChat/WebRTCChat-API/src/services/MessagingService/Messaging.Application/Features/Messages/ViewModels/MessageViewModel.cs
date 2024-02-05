namespace Messaging.Application.Features.Messages.ViewModels;

    public class MessageViewModel
    {
	public Guid MessageId { get; set; }
	public Guid SenderId { get; set; }
	public Guid ReciverId { get; set; }
	public string SenderDisplayName { get; set; }
	public string SenderUsername { get; set; }
	public string Content { get; set; }

}
