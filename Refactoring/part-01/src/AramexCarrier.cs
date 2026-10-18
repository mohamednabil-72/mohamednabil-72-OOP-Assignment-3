namespace RefactoringLab;

public class AramexCarrier : IShippingCarrier
{
    public decimal Calculate(decimal weightKg)
    {
        return weightKg * 12m;
    }
}