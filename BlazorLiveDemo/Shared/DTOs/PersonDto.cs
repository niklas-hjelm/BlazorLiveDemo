namespace BlazorLiveDemo.Shared.DTOs;
public record PersonDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public PersonDto(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
