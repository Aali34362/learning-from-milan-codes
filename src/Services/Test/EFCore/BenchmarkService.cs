using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using Microsoft.EntityFrameworkCore;
using OptimizeMePlease.Context;
using System.Collections.Generic;
using System.Linq;

namespace OptimizeMePlease
{
    [MemoryDiagnoser]
    [Config(typeof(Config))]
    [HideColumns(Column.StdErr, Column.RatioSD, Column.AllocRatio, Column.StdDev)]
    public class BenchmarkService
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
            }
        }

        /// <summary>
        /// Get top 2 Authors (FirstName, LastName, UserName, Email, Age, Country) 
        /// from country Serbia aged 27, with the highest BooksCount
        /// and all his/her books (Book Name/Title and Publishment Year) published before 1900
        /// </summary>
        /// <returns></returns>
        [Benchmark(Baseline = true)]
        public List<AuthorDTO> GetAuthors()
        {
            using var dbContext = new AppDbContext();

            var authors = dbContext.Authors
                .Include(author => author.User)
                .ThenInclude(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role)
                .Include(author => author.Books)
                .ThenInclude(book => book.Publisher)
                .ToList()
                .Select(author => new AuthorDTO
                {
                    UserCreated = author.User.Created,
                    UserEmailConfirmed = author.User.EmailConfirmed,
                    UserFirstName = author.User.FirstName,
                    UserLastActivity = author.User.LastActivity,
                    UserLastName = author.User.LastName,
                    UserEmail = author.User.Email,
                    UserName = author.User.UserName,
                    UserId = author.User.Id,
                    RoleId = author.User.UserRoles
                        .FirstOrDefault(y => y.UserId == author.UserId).RoleId,
                    BooksCount = author.BooksCount,
                    AllBooks = author.Books.Select(book => new BookDto
                    {
                        Id = book.Id,
                        Name = book.Name,
                        Published = book.Published,
                        ISBN = book.ISBN,
                        PublisherName = book.Publisher.Name
                    }).ToList(),
                    AuthorAge = author.Age,
                    AuthorCountry = author.Country,
                    AuthorNickName = author.NickName,
                    Id = author.Id
                })
                .ToList()
                .Where(authorDto =>
                    authorDto.AuthorCountry == "Serbia" && authorDto.AuthorAge == 27)
                .ToList();

            var orderedAuthors = authors
                .OrderByDescending(x => x.BooksCount)
                .ToList()
                .Take(2)
                .ToList();

            List<AuthorDTO> finalAuthors = new List<AuthorDTO>();
            foreach (var author in orderedAuthors)
            {
                List<BookDto> books = new List<BookDto>();

                var allBooks = author.AllBooks;

                foreach (var book in allBooks)
                {
                    if (book.Published.Year < 1900)
                    {
                        book.PublishedYear = book.Published.Year;
                        books.Add(book);
                    }
                }

                author.AllBooks = books;
                finalAuthors.Add(author);
            }

            return finalAuthors;
        }

        [Benchmark]
        public List<AuthorDTO> GetAuthors_Optimized()
        {
            using var dbContext = new AppDbContext();

            var authors = dbContext
                .Authors
                .Where(author =>
                    author.Country == "Serbia" &&
                    author.Age == 27)
                .OrderByDescending(author => author.BooksCount)
                .Select(author => new AuthorDTO
                {
                    UserFirstName = author.User.FirstName,
                    UserLastName = author.User.LastName,
                    UserEmail = author.User.Email,
                    UserName = author.User.UserName,
                    AllBooks = author.Books
                        .Select(book => new BookDto
                        {
                            Id = book.Id,
                            Name = book.Name,
                            Published = book.Published
                        }).ToList(),
                    AuthorAge = author.Age,
                    AuthorCountry = author.Country,
                    Id = author.Id
                })
                .Take(2)
                .ToList();

            foreach (var author in authors)
            {
                author.AllBooks = author.AllBooks
                    .Where(bookDto => bookDto.Published.Year < 1900)
                    .ToList();
            }

            return authors;
        }

        [Benchmark]
        public List<AuthorDTO> GetAuthors_Optimized_Compiled()
        {
            using var context = new AppDbContext();

            var authors = CompiledQuery(context).ToList();

            foreach (var author in authors)
            {
                author.AllBooks = author.AllBooks
                    .Where(bookDto => bookDto.Published.Year < 1900)
                    .ToList();
            }

            return authors;
        }

        private static readonly Func<AppDbContext, IEnumerable<AuthorDTO>> CompiledQuery =
            EF.CompileQuery(
                (AppDbContext context) =>
                    context.Authors
                        .Where(author =>
                            author.Country == "Serbia" &&
                            author.Age == 27)
                        .OrderByDescending(author => author.BooksCount)
                        .Select(author => new AuthorDTO
                        {
                            UserFirstName = author.User.FirstName,
                            UserLastName = author.User.LastName,
                            UserEmail = author.User.Email,
                            UserName = author.User.UserName,
                            AllBooks = author.Books
                                .Select(book => new BookDto
                                {
                                    Id = book.Id,
                                    Name = book.Name,
                                    Published = book.Published
                                }).ToList(),
                            AuthorAge = author.Age,
                            AuthorCountry = author.Country,
                            Id = author.Id
                        })
                        .Take(2));
    }
}