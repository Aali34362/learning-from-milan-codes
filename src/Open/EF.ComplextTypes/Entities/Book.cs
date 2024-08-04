using System;

namespace EF.ComplexTypes.Entities;

public class Book
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Author Author { get; set; }

    public DateTime Published { get; set; }

    public string ISBN { get; set; }
}