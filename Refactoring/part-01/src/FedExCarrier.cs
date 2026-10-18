namespace RefactoringLab;

public class FedExCarrier : IShippingCarrier
{
    public decimal Calculate(decimal weightKg)
    {
        return weightKg * 15m;
    }
}