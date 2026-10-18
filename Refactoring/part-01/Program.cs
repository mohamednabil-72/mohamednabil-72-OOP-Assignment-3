namespace RefactoringLab;

public class Program
{
    public static void Main()
    {
        // Shipping Cost Calculator
        Console.WriteLine("=== Shipping Costs ===");

        var calculator = new ShippingCostCalculator();

        IShippingCarrier aramex = new AramexCarrier();
        IShippingCarrier fedEx = new FedExCarrier();
        IShippingCarrier dhl = new DhlCarrier();
        IShippingCarrier bosta = new BostaCarrier();

        Console.WriteLine($"Aramex: {calculator.Calculate(aramex, 2)}");
        Console.WriteLine($"FedEx: {calculator.Calculate(fedEx, 2)}");
        Console.WriteLine($"DHL: {calculator.Calculate(dhl, 2)}");
        Console.WriteLine($"Bosta: {calculator.Calculate(bosta, 2)}");

        // Order Processing
        Console.WriteLine("\n=== Order Processing ===");

        IOrderRepository repo = new SqlOrderRepository();
        IEmailSender emailSender = new SmtpEmailSender();

        var processor = new OrderProcessor(repo, emailSender);
        processor.Process(101, "customer@example.com");

        // Notifications
        Console.WriteLine("\n=== Notifications ===");

        INotificationChannel email = new EmailNotification();
        INotificationChannel sms = new SmsNotification();
        INotificationChannel whatsapp = new WhatsAppNotification();

        INotificationDelivery immediate = new ImmediateDelivery();

        INotificationDelivery scheduled =
            new ScheduledDelivery(DateTime.Now.AddHours(1));

        INotificationModifier urgent = new UrgentModifier();

        // Email: all combinations
        Console.WriteLine("\n-- Email --");

        new Notification(email, immediate)
            .Send("customer@example.com", "Order confirmed");

        new Notification(email, immediate, [urgent])
            .Send("customer@example.com", "Order confirmed");

        new Notification(email, scheduled)
            .Send("customer@example.com", "Order confirmed");

        new Notification(email, scheduled, [urgent])
            .Send("customer@example.com", "Order confirmed");

        // SMS: all combinations
        Console.WriteLine("\n-- SMS --");

        new Notification(sms, immediate)
            .Send("01000000000", "Order confirmed");

        new Notification(sms, immediate, [urgent])
            .Send("01000000000", "Order confirmed");

        new Notification(sms, scheduled)
            .Send("01000000000", "Order confirmed");

        new Notification(sms, scheduled, [urgent])
            .Send("01000000000", "Order confirmed");

        // WhatsApp: all combinations
        Console.WriteLine("\n-- WhatsApp --");

        new Notification(whatsapp, immediate)
            .Send("01000000000", "Order confirmed");

        new Notification(whatsapp, immediate, [urgent])
            .Send("01000000000", "Order confirmed");

        new Notification(whatsapp, scheduled)
            .Send("01000000000", "Order confirmed");

        new Notification(whatsapp, scheduled, [urgent])
            .Send("01000000000", "Order confirmed");
    }
}