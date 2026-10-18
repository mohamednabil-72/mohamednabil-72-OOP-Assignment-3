namespace RefactoringLab.Part03.Students;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public static class StudentCatalog
{
    // Lazy: a student is created only when the caller asks for the next one,
    // so stopping after 3 students creates 3 objects, not 1_000_000.
    public static IEnumerable<Student> GetAllStudents()
    {
        for (var i = 1; i <= 1_000_000; i++)
            yield return new Student { Id = i, Name = $"Student {i}" };
    }
}
