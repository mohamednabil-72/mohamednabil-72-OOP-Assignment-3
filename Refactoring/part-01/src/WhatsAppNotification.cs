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

// Message modifiers

public interface INotificationModifier
{
    string Apply(string message);
}

public class UrgentModifier : INotificationModifier
{
    public string Apply(string message) =>
        $"[URGENT] {message}";
}

// Delivery strategies

public interface INotificationDelivery
{
    void Deliver(
        INotificationChannel channel,
        string to,
        string message);
}

public class ImmediateDelivery : INotificationDelivery
{
    public void Deliver(
        INotificationChannel channel,
        string to,
        string message)
    {
        channel.Send(to, message);
    }
}

public class ScheduledDelivery : INotificationDelivery
{
    public DateTime SendAt { get; }

    public ScheduledDelivery(DateTime sendAt)
    {
        SendAt = sendAt;
    }

    public void Deliver(
        INotificationChannel channel,
        string to,
        string message)
    {
        Console.WriteLine(
            $"[{channel.Name} scheduled {SendAt:g}] {to}: {message}");
    }
}

// Notification composition

public class Notification
{
    private readonly INotificationChannel _channel;
    private readonly INotificationDelivery _delivery;
    private readonly IEnumerable<INotificationModifier> _modifiers;

    public Notification(
        INotificationChannel channel,
        INotificationDelivery delivery,
        IEnumerable<INotificationModifier>? modifiers = null)
    {
        _channel = channel;
        _delivery = delivery;
        _modifiers = modifiers ?? Enumerable.Empty<INotificationModifier>();
    }

    public void Send(string to, string message)
    {
        foreach (var modifier in _modifiers)
        {
            message = modifier.Apply(message);
        }

        _delivery.Deliver(_channel, to, message);
    }
}