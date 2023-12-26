using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Company : IEntity
{
    public int Id { get; }
    public string Name { get; set; }
    private static int id;

    public Company(string name)
    {
        Id = id++;
        Name = name;
    }
    public override string ToString()
    {
        return $"ID: {Id} /Name: {Name.ToUpper()} \n ";
    }
}
