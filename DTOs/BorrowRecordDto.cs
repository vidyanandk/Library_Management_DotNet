
namespace LibraryManagementAPI.DTOs
{
    public class BorrowRecordDto
    {
        public int BookId { get; set; }
        public string BorrowerName { get; set; } = string.Empty;
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
