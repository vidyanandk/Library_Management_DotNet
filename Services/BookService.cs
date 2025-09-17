using LibraryManagementAPI.Models;
using LibraryManagementAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _context;
    public  BookService(AppDbContext context, IAuthorService authorService)
    {
        _context = context;
        _authorService = authorService;
    }
    //private readonly List<Book> _books = new();
    //private int _nextId = 1;
    private readonly IAuthorService _authorService;

    //public BookService(IAuthorService authorService)
    //{
    //    _authorService = authorService;
    //}

    public IEnumerable<Book> GetAll() => _context.Books.ToList();

    public Book? GetById(int id) => _context.Books.Find(id);

    public Book Add(BookDto dto)
    {
        // ensure Author exists
        //var author = _authorService.GetById(dto.AuthorId);
        var author = _context.Authors.Find(dto.AuthorId);
        if (author is null)
            throw new ArgumentException($"Author with Id {dto.AuthorId} not found");

        var book = new Book
        {
            //Id = _nextId++,
            Title = dto.Title,
            Genre = dto.Genre,
            AuthorId = dto.AuthorId,
            Author = author
        };

        _context.Books.Add(book);
        author.Books.Add(book); // maintain relationship
        _context.SaveChanges();
        return book;
    }

    public Book? Update(int id, BookDto dto)
    {
        var book = _context.Books.Find(id);
        if (book is null) return null;

        // ensure Author exists
        var author = _context.Authors.Find(dto.AuthorId);
        if (author is null)
            throw new ArgumentException($"Author with Id {dto.AuthorId} not found");

        book.Title = dto.Title;
        book.Genre = dto.Genre;
        book.AuthorId = dto.AuthorId;
        book.Author = author;
        _context.SaveChanges();
        return book;
    }

    public bool Delete(int id)
    {
        var book = _context.Books.Find(id);
        if (book is null) return false;

        // remove from author's book list
        book.Author?.Books.Remove(book);
        _context.Books.Remove(book);
        _context.SaveChanges();
        return true;
    }
}
