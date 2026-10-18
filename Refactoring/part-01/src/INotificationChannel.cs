namespace RefactoringLab;

public interface INotificationChannel
{
    void Send(string to, string message);
}