using BlazorLiveDemo.Server.DataAccess.Models;
using BlazorLiveDemo.Shared;

namespace BlazorLiveDemo.Server.DataAccess;

public class PeopleRepository
{
    private readonly PeopleContext _context;

    public PeopleRepository(PeopleContext context)
    {
        _context = context;
    }
    public void Add(PersonDto newPerson)
    {
        var names = newPerson.Name.Split(' ');

        var newPersonModel = new PersonModel();
        newPersonModel.FirstName = names[0];
        newPersonModel.LastName = names[1];
        newPersonModel.Age = newPerson.Age;
        _context.People.Add(newPersonModel);
        _context.SaveChanges();
    }

    public IEnumerable<PersonDto> GetAllPeople()
    {
        return _context.People
            .Select(pm => 
                new PersonDto($"{pm.FirstName} {pm.LastName}", pm.Age)
            );
    }
}