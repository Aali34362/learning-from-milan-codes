using EF.ComplexTypes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace EF.ComplexTypes;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost;Database=EF.ComplexTypes;Trusted_Connection=True;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=true");
    
        options.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ComplexProperty(b => b.Author);
    }

    public DbSet<Book> Books { get; set; }
}