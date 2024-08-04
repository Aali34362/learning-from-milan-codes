namespace CreditCalculator.After;

public class CreditLimitCalculator(CustomerCreditServiceClient creditService)
{
    public (bool HasCreditLimit, decimal? CreditLimit) Calculate(
        Company company,
        Customer customer)
    {
        return company.Type switch
        {
            CompanyType.VeryImportantClient => (false, null),
            CompanyType.ImportantClient => (true, GetCreditLimit(customer) * 2),
            _ => (true, GetCreditLimit(customer))
        };
    }

    private decimal GetCreditLimit(Customer customer)
    {
        return creditService.GetCreditLimit(
            customer.FirstName,
            customer.LastName,
            customer.DateOfBirth);
    }
}