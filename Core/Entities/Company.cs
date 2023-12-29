using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Company : IEntity
{
    public int Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    private static int id;

    public Company(string name, string description)
    {
        Id = id++;
        Name = name;
        Description = description;
    }
    public override string ToString()
    {
        return $"ID: {Id} /Name: {Name.ToUpper()} \n ";
    }
}
