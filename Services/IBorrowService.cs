using LibraryManagementAPI.Models;
using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services;

public interface IBorrowService
{
    IEnumerable<BorrowRecord> GetAll();
    BorrowRecord? GetById(int id);
    BorrowRecord Add(BorrowRecordDto dto);
    BorrowRecord? Update(int id, BorrowRecordDto dto);
    bool Delete(int id);
}
