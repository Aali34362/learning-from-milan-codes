using Domain;

var order = new Order
{
    Id = 1,
    Status = "Pending",
    IsVerified = true,
    Items = new List<LineItem>
    {
        new() { Id = 1, Name = "LI #1", Quantity = 1, Price = 2.99m },
        new() { Id = 2, Name = "LI #2", Quantity = 2, Price = 3.99m },
        new() { Id = 3, Name = "LI #3", Quantity = 3, Price = 4.99m },
        new() { Id = 4, Name = "LI #4", Quantity = 4, Price = 5.99m },
        new() { Id = 5, Name = "LI #5", Quantity = 5, Price = 6.99m }
    }
};

var processor = new OrderProcessor();

processor.Process(order);

Console.WriteLine("Order was processed");
