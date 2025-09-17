//using LibraryManagementAPI.DTOs;
//using LibraryManagementAPI.Services;
//namespace LibraryManagementAPI.EndPoints
//{
//    public static class BookEndpoint
//    {
//        public static RouteGroupBuilder MapBookEndPoints(this IEndpointRouteBuilder app)
//        {
//            var books = app.MapGroup("/books");
//            books.MapGet("/", GetAllBooks);
//            books.MapGet("/{id}", GetBookById);
//            books.MapPost("/", CreateBooks);
//            books.MapPut("/{id}", UpdateBook);
//            books.MapDelete("/{id}", DeleteBook);
//            return books;
//        }


//        static IResult GetAllBooks(IBookService bookService)
//        {
//            var books = bookService.GetAll();
//            return TypedResults.Ok(books);
//        }
//        static IResult GetBookById(int id, IBookService bookService)
//        {
//            var book = bookService.GetById(id);
//            return book is null ? TypedResults.NotFound() : TypedResults.Ok(book);
//        }

//        static IResult CreateBooks(BookDto book, IBookService bookService)
//        {
//            var books = bookService.Add(book);
//            return Results.Created($"/books/{books.Id}", books);
//        }

//        static IResult UpdateBook(int id, BookDto book, IBookService bookService)
//        {
//            var updatedBook = bookService.Update(id, book);
//            return updatedBook is null ? TypedResults.NotFound() : TypedResults.Ok(updatedBook);
//        }

//        static IResult DeleteBook(int id, IBookService bookService)
//        {
//            var deleted = bookService.Delete(id);
//            return deleted ? TypedResults.NoContent() : TypedResults.NotFound();
//        }
//    }
//}







using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Endpoints;

public static class BookEndpoints
{
    public static RouteGroupBuilder MapBookEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/books");

        group.MapGet("/", GetAllBooks);
        group.MapGet("/{id}", GetBookById);
        group.MapPost("/", CreateBook);
        group.MapPut("/{id}", UpdateBook);
        group.MapDelete("/{id}", DeleteBook);

        return group;
    }

    static IResult GetAllBooks(IBookService service) =>
        TypedResults.Ok(service.GetAll());

    static IResult GetBookById(int id, IBookService service) =>
        service.GetById(id) is Book book
            ? TypedResults.Ok(book)
            : TypedResults.NotFound();

    static IResult CreateBook(BookDto dto, IBookService service, IAuthorService authorService)
    {
        if (authorService.GetById(dto.AuthorId) is null)
            return TypedResults.BadRequest($"Author with Id {dto.AuthorId} does not exist.");

        var book = service.Add(dto);
        return TypedResults.Created($"/books/{book.Id}", book);
    }

    static IResult UpdateBook(int id, BookDto dto, IBookService service, IAuthorService authorService)
    {
        if (authorService.GetById(dto.AuthorId) is null)
            return TypedResults.BadRequest($"Author with Id {dto.AuthorId} does not exist.");

        var updated = service.Update(id, dto);
        return updated is not null
            ? TypedResults.Ok(updated)
            : TypedResults.NotFound();
    }

    static IResult DeleteBook(int id, IBookService service) =>
        service.Delete(id)
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
}
