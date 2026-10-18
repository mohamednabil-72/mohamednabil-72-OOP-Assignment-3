using GenericsApp;

// 5 students in a Store<Student>
var students = new Store<Student>();
students.Add(new Student(1, "Ahmed"));
students.Add(new Student(2, "Mona"));
students.Add(new Student(3, "Youssef"));
students.Add(new Student(4, "Salma"));
students.Add(new Student(5, "Omar"));

// 3 courses in a Store<Course>
var courses = new Store<Course>();
courses.Add(new Course(10, "C# Basics", 1500m));
courses.Add(new Course(20, "Collections & Generics", 1800m));
courses.Add(new Course(30, "ASP.NET Core", 2500m));

// Get one of each by id
Console.WriteLine("--- GetById ---");
Console.WriteLine(students.GetById(3));
Console.WriteLine(courses.GetById(20));
Console.WriteLine($"Missing student 99 -> {(students.GetById(99) is null ? "null" : "found")}");

// Duplicate id: catch the exception and print its message
Console.WriteLine("--- Duplicate Id ---");
try
{
    students.Add(new Student(3, "Another Youssef"));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

// Page 2 (size 2) of the students
Console.WriteLine("--- Page 2, size 2 ---");
foreach (var student in students.GetAll().Page(2, 2))
{
    Console.WriteLine(student);
}

// FindById on a plain List<Course> (works because Course : IHasId)
Console.WriteLine("--- FindById on List<Course> ---");
var courseList = new List<Course>
{
    new Course(1, "Git & GitHub", 600m),
    new Course(2, "SQL Server", 1200m),
};
Console.WriteLine(courseList.FindById(2));
Console.WriteLine($"Missing course 99 -> {(courseList.FindById(99) is null ? "null" : "found")}");

// must NOT compile: string does not implement IHasId, so it violates the constraint on Store<T>
// var bad = new Store<string>();
