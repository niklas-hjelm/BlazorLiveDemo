using BlazorLiveDemo.Shared;
using Microsoft.AspNetCore.SignalR;

namespace BlazorLiveDemo.Server.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(ChatMessageDto message)
    {
        await Clients.All.SendAsync("SendMessage", message);
    }
}