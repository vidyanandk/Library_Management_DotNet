﻿namespace LibraryManagementAPI.DTOs;

public class BookDto
{
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int AuthorId { get; set; }
}
