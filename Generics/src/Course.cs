namespace GenericsApp;

public class Course : IHasId
{
    public int Id { get; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Course(int id, string title, decimal price)
    {
        Id = id;
        Title = title;
        Price = price;
    }

    public override string ToString() => $"Course #{Id}: {Title} ({Price:0.00} EGP)";
}
