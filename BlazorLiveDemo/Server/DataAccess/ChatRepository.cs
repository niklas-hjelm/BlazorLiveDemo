using BlazorLiveDemo.Server.DataAccess.Models;
using BlazorLiveDemo.Shared.DTOs;
using MongoDB.Driver;

namespace BlazorLiveDemo.Server.DataAccess;

public class ChatRepository :IRepository<ChatMessageDto>
{

    private readonly IMongoCollection<ChatMessageModel> _chatCollection;

    public ChatRepository()
    {
        var host = "localhost";
        var databaseName = "CloudChat";
        var connectionString = $"mongodb://{host}:27017";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _chatCollection = database.GetCollection<ChatMessageModel>
            ("Messages", new () {AssignIdOnInsert = true});
    }

    public async Task AddAsync(ChatMessageDto entity)
    {
        await _chatCollection.InsertOneAsync(new ChatMessageModel()
        {
            Sender = entity.Name,
            Message = entity.Message,
            TimeSent = entity.TimeSent
        });
    }

    public async Task DeleteAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<ChatMessageDto> GetAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ChatMessageDto>> GetAllAsync()
    {
        var filter = Builders<ChatMessageModel>.Filter.Empty;
        var allMessages = await _chatCollection.FindAsync(filter);

        return allMessages.ToEnumerable()
            .Select(m => new ChatMessageDto()
            {
                Name = m.Sender,
                Message = m.Message,
                TimeSent = m.TimeSent
            });
    }

    public async Task<ChatMessageDto> UpdateAsync(ChatMessageDto entity)
    {
        throw new NotImplementedException();
    }
}