using EfCore.Linq.ExtensionMethod;

using var context = new AppDbContext();

string nameFilter = "sample";
string emailFilter = "email";
int age = -1;

var customers = context.Customers
    .WhereIf(!string.IsNullOrEmpty(nameFilter), c => c.Name.Contains(nameFilter))
    .WhereIf(!string.IsNullOrEmpty(emailFilter), c =>  c.Email.Contains(emailFilter))
    .WhereIf(age > 0, c => c.Age >= age)
    .ToList();

Console.ReadKey();
