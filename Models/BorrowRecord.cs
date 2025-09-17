﻿namespace LibraryManagementAPI.Models;

public class BorrowRecord
{
    public int Id { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public int BookId { get; set; }
    public Book? Book { get; set; }
}
