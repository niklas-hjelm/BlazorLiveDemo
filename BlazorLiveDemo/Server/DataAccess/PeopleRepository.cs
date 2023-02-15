using BlazorLiveDemo.Server.DataAccess.Models;
using BlazorLiveDemo.Shared;

namespace BlazorLiveDemo.Server.DataAccess;

public class PeopleRepository : IRepository<PersonDto>
{
    private readonly PeopleContext _context;

    public PeopleRepository(PeopleContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PersonDto entity)
    {
        var names = entity.Name.Split(' ');

        var newPersonModel = new PersonModel();
        newPersonModel.FirstName = names[0];
        newPersonModel.LastName = names[1];
        newPersonModel.Age = entity.Age;
        await _context.People.AddAsync(newPersonModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<PersonDto> GetAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync()
    {
        return _context.People
            .Select(pm =>
                new PersonDto($"{pm.FirstName} {pm.LastName}", pm.Age)
            );
    }

    public async Task<PersonDto> UpdateAsync(PersonDto entity)
    {
        throw new NotImplementedException();
    }
}