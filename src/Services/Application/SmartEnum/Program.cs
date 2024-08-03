using SmartEnum;

var creditCard = CreditCard.FromName("Platinum");

Console.WriteLine($"Discount for {creditCard} is {creditCard.Discount:P}");

Console.ReadKey();
