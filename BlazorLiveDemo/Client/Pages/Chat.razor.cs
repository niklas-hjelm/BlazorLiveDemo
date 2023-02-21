using System.Net.Http.Json;
using BlazorLiveDemo.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorLiveDemo.Client.Pages;

public partial class Chat : ComponentBase
{
    ChatMessageDto CurrentMessage { get; set; }
    List<ChatMessageDto> AllMessages { get; set; } = new();

    HubConnection _chatConnection;

    protected override async Task OnInitializedAsync()
    {
        CurrentMessage = new ChatMessageDto();
        AllMessages = await _client.GetFromJsonAsync<List<ChatMessageDto>>(_client.BaseAddress + "getAllChat");

        _chatConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.BaseUri + "hubs/ChatHub")
            .Build();

        _chatConnection.On<ChatMessageDto>("SendMessage", (message) =>
        {
            AllMessages.Add(message);
            StateHasChanged();
        });

        await _chatConnection.StartAsync();

        await base.OnInitializedAsync();
    }

    private async Task SendMessage()
    {
        var user = await _authenticationStateProvider.GetAuthenticationStateAsync();

        CurrentMessage.Name = user.User.Identity.Name;

        CurrentMessage.TimeSent = DateTime.UtcNow;
        _chatConnection.SendAsync("SendMessage", CurrentMessage);
        CurrentMessage = new ChatMessageDto();
    }

}