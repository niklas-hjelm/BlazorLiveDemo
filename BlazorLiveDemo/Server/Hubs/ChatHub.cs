using BlazorLiveDemo.Server.DataAccess;
using BlazorLiveDemo.Shared;
using Microsoft.AspNetCore.SignalR;

namespace BlazorLiveDemo.Server.Hubs;

public class ChatHub : Hub
{
    private readonly IRepository<ChatMessageDto> _messageRepository;

    public ChatHub(IRepository<ChatMessageDto> messageRepository)
    {
        _messageRepository = messageRepository;
    }
    public async Task SendMessage(ChatMessageDto message)
    {
        await _messageRepository.AddAsync(message);
        await Clients.All.SendAsync("SendMessage", message);
    }
}