namespace RefactoringLab;

public class ShippingCostCalculator 
{
    public decimal Calculate(IShippingCarrier carrier, decimal weightKg)
    {
        return carrier.Calculate(weightKg);
    }

}
