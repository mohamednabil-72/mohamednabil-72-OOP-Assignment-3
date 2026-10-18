namespace RefactoringLab;

public class BostaCarrier : IShippingCarrier
{
    public decimal Calculate(decimal weightKg)
    {
        return weightKg * 10m;
    }
}