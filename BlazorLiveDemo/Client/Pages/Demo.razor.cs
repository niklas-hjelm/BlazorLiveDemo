using BlazorLiveDemo.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorLiveDemo.Client.Pages;

public partial class Demo : ComponentBase
{
    int counter;

    List<string> Names { get; set; } = new() { "Peter", "Casandra", "Anna" };
    List<PersonDto> People { get; set; } = new();
    PersonDto CurrentPerson { get; set; } = new("", 0);

    private void OnClicked()
    {
        counter++;
    }

    private async Task SavePerson()
    {
        await _client.PostAsJsonAsync<PersonDto>("addPerson", CurrentPerson);
        var response = await _client.GetFromJsonAsync<IEnumerable<PersonDto>>("allPeople");
        People = response.ToList();
        CurrentPerson = new PersonDto("", 0);
    }
}