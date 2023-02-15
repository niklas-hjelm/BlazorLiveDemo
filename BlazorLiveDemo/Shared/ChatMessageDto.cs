namespace BlazorLiveDemo.Shared;

public class ChatMessageDto
{
    public string Name { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime TimeSent { get; set; } = DateTime.Now;
}