using LibraryManagementAPI.Models;
using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services;

public interface IAuthorService
{
    IEnumerable<Author> GetAll();
    Author? GetById(int id);
    Author Add(AuthorDto dto);
    Author? Update(int id, AuthorDto dto);
    bool Delete(int id);
}
