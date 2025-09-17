﻿using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Models;
namespace LibraryManagementAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; } 
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
    }
}
