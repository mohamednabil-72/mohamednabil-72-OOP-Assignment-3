namespace RefactoringLab;

public class DhlCarrier : IShippingCarrier
{
    public decimal Calculate(decimal weightKg)
    {
        return weightKg * 18m;
    }
}