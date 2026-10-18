namespace GenericsApp;

public class Student : IHasId
{
    public int Id { get; }
    public string Name { get; set; } = string.Empty;

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => $"Student #{Id}: {Name}";
}
