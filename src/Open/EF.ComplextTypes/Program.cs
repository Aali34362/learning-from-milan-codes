using EF.ComplexTypes;
using EF.ComplexTypes.Entities;
using System;
using System.Linq;

using var dbContext = new ApplicationDbContext();

//var author = new Author("Robert", "Martin", "US");

//var book = new Book
//{
//    Name = "Clean Architecture",
//    ISBN = "CA12345",
//    Published = new DateTime(2017, 1, 1),
//    Author = author
//};

//var book2 = new Book
//{
//    Name = "Clean Code",
//    ISBN = "CA123456",
//    Published = new DateTime(2008, 1, 1),
//    Author = author
//};

//dbContext.Books.Add(book);

//dbContext.Books.Add(book2);

//dbContext.SaveChanges();

//book.Author = book.Author with { LastName = "C. Martin" };

//dbContext.SaveChanges();

var author = dbContext.Books.Select(b => b.Author).First();

var books = dbContext.Books.Where(b => b.Author.Country == "US").ToList();

Console.ReadKey();