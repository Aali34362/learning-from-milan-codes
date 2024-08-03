namespace AwesomeCompany.Entities;

public class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime? LastSalaryUpdateUtc { get; set; }

    public List<Employee> Employees { get; set; }
}