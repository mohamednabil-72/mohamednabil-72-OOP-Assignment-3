namespace RefactoringLab;

public interface INotificationChannel
{
    string Name { get; }

    void Send(string to, string message);
}

public class EmailNotification : INotificationChannel
{
    public string Name => "email";

    public void Send(string to, string message) =>
        Console.WriteLine($"[email] {to}: {message}");
}

public class SmsNotification : INotificationChannel
{
    public string Name => "sms";

    public void Send(string to, string message) =>
        Console.WriteLine($"[sms] {to}: {message}");
}

public class Notification
{
    private readonly INotificationChannel _channel;
    private readonly bool _isUrgent;
    private readonly DateTime? _sendAt;

    public Notification(
        INotificationChannel channel,
        bool isUrgent = false,
        DateTime? sendAt = null)
    {
        _channel = channel;
        _isUrgent = isUrgent;
        _sendAt = sendAt;
    }

    public void Send(string to, string message)
    {
        if (_isUrgent)
        {
            message = $"[URGENT] {message}";
        }

        if (_sendAt.HasValue)
        {
            Console.WriteLine(
                $"[{_channel.Name} scheduled {_sendAt.Value:g}] {to}: {message}");
        }
        else
        {
            _channel.Send(to, message);
        }
    }
}