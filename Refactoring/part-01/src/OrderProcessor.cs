namespace RefactoringLab;

public class OrderProcessor
{
    private readonly IOrderRepository _repo;
    private readonly IEmailSender _email;

    public OrderProcessor(IOrderRepository repo, IEmailSender email)
    {
        _repo = repo;
        _email = email;
    }

    public void Process(int orderId, string customerEmail)
    {
        var processedAt = DateTime.Now;

        _repo.Save(orderId, processedAt);
        _email.Send(customerEmail, $"Order {orderId} confirmed at {processedAt}");
    }
}