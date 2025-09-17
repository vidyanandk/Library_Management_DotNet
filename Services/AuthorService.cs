//using LibraryManagementAPI.Models;
//using LibraryManagementAPI.DTOs;

//namespace LibraryManagementAPI.Services;

//public class AuthorService : IAuthorService
//{
//    private readonly List<Author> _authors = new();
//    private int _nextId = 1;

//    public IEnumerable<Author> GetAll() => _authors;

//    public Author? GetById(int id) => _authors.FirstOrDefault(a => a.Id == id);

//    public Author Add(AuthorDto dto)
//    {
//        var author = new Author
//        {
//            Id = _nextId++,
//            Name = dto.Name,
//            Country = dto.Country
//        };
//        _authors.Add(author);
//        return author;
//    }

//    public Author? Update(int id, AuthorDto dto)
//    {
//        var author = GetById(id);
//        if (author is null) return null;

//        author.Name = dto.Name;
//        author.Country = dto.Country;

//        return author;
//    }

//    public bool Delete(int id)
//    {
//        var author = GetById(id);
//        if (author is null) return false;
//        _authors.Remove(author);
//        return true;
//    }
//}








using LibraryManagementAPI.Models;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Services;

public class AuthorService : IAuthorService
{
    private readonly AppDbContext _context;
    public AuthorService(AppDbContext context)
    {
        _context = context;
    }

    //private readonly List<Author> _authors = new();
    //private int _nextId = 1;

    public IEnumerable<Author> GetAll() => _context.Authors.ToList();

    public Author? GetById(int id) => _context.Authors.Find(id);

    public Author Add(AuthorDto dto)
    {
        var author = new Author
        {
            //Id = _nextId++,
            Name = dto.Name,
            Country = dto.Country
        };
        _context.Authors.Add(author);
        _context.SaveChanges();
        return author;
    }

    public Author? Update(int id, AuthorDto dto)
    {
        var author = _context.Authors.Find(id);
        if (author is null) return null;

        author.Name = dto.Name;
        author.Country = dto.Country;

        _context.SaveChanges();
        return author;
    }

    public bool Delete(int id)
    {
        var author = _context.Authors.Find(id);
        if (author is null) return false;
        _context.Authors.Remove(author);
        _context.SaveChanges();
        return true;
    }
}
