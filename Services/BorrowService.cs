//using LibraryManagementAPI.Models;
//using LibraryManagementAPI.DTOs;
//using LibraryManagementAPI.Data;
//using Microsoft.EntityFrameworkCore;
//using LibraryManagementAPI.Services;

//namespace LibraryManagementAPI.Services;

//public class BorrowService : IBorrowService
//{
//    private readonly List<BorrowRecord> _records = new();
//    private readonly IBookService _bookService;
//    private int _nextId = 1;

//    public BorrowService(IBookService bookService)
//    {
//        _bookService = bookService;
//    }

//    public IEnumerable<BorrowRecord> GetAll() => _records;

//    public BorrowRecord? GetById(int id) => _records.FirstOrDefault(r => r.Id == id);

//    public BorrowRecord Add(BorrowRecordDto dto)
//    {
//        // ensure Book exists
//        var book = _bookService.GetById(dto.BookId);
//        if (book is null)
//            throw new ArgumentException($"Book with Id {dto.BookId} not found");

//        var record = new BorrowRecord
//        {
//            Id = _nextId++,
//            BookId = dto.BookId,
//            Book = book,
//            BorrowerName = dto.BorrowerName,
//            BorrowDate = dto.BorrowDate,
//            ReturnDate = dto.ReturnDate
//        };

//        _records.Add(record);
//        book.BorrowRecords.Add(record); // maintain relationship

//        return record;
//    }

//    public BorrowRecord? Update(int id, BorrowRecordDto dto)
//    {
//        var record = GetById(id);
//        if (record is null) return null;

//        // ensure Book exists
//        var book = _bookService.GetById(dto.BookId);
//        if (book is null)
//            throw new ArgumentException($"Book with Id {dto.BookId} not found");

//        record.BookId = dto.BookId;
//        record.Book = book;
//        record.BorrowerName = dto.BorrowerName;
//        record.BorrowDate = dto.BorrowDate;
//        record.ReturnDate = dto.ReturnDate;

//        return record;
//    }

//    public bool Delete(int id)
//    {
//        var record = GetById(id);
//        if (record is null) return false;

//        record.Book?.BorrowRecords.Remove(record);
//        _records.Remove(record);

//        return true;
//    }
//}














using LibraryManagementAPI.Models;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Services;

public class BorrowService : IBorrowService
{
    private readonly AppDbContext _context;
    private readonly IBookService _bookService;
    public BorrowService(IBookService bookService, AppDbContext context)
    {
        _context = context;
        _bookService = bookService;
    }
    

    public IEnumerable<BorrowRecord> GetAll() => _context.BorrowRecords.ToList();


    public BorrowRecord? GetById(int id) => _context.BorrowRecords.Find(id);

    public BorrowRecord Add(BorrowRecordDto dto)
    {
        // ensure Book exists
        var book = _bookService.GetById(dto.BookId);
        if (book is null)
            throw new ArgumentException($"Book with Id {dto.BookId} not found");

        var record = new BorrowRecord
        {
            //Id = _nextId++,
            BookId = dto.BookId,
            Book = book,
            BorrowerName = dto.BorrowerName,
            BorrowDate = dto.BorrowDate,
            ReturnDate = dto.ReturnDate
        };

        _context.BorrowRecords.Add(record);
        book.BorrowRecords.Add(record); // maintain relationship
        _context.SaveChanges();

        return record;
    }

    public BorrowRecord? Update(int id, BorrowRecordDto dto)
    {
        var record = GetById(id);
        if (record is null) return null;

        // ensure Book exists
        var book = _bookService.GetById(dto.BookId);
        if (book is null)
            throw new ArgumentException($"Book with Id {dto.BookId} not found");

        record.BookId = dto.BookId;
        record.Book = book;
        record.BorrowerName = dto.BorrowerName;
        record.BorrowDate = dto.BorrowDate;
        record.ReturnDate = dto.ReturnDate;
        _context.SaveChanges();

        return record;
    }

    public bool Delete(int id)
    {
        var record = GetById(id);
        if (record is null) return false;

        record.Book?.BorrowRecords.Remove(record);
        //_records.Remove(record);
        _context.BorrowRecords.Remove(record);
        _context.SaveChanges();

        return true;
    }
}
