//using LibraryManagementAPI.DTOs;
//using LibraryManagementAPI.Services;
//namespace LibraryManagementAPI.EndPoints
//{
//    public static class BorrowRecordEndpoints
//    {
//        public static void MapBorrowRecordEndPoints(WebApplication app)
//        {
//            var borrowRecords = app.MapGroup("/borrowrecords");
//            borrowRecords.MapGet("/", GetAllBorrowRecords);
//            borrowRecords.MapGet("/{id}", GetBorrowRecordById);
//            borrowRecords.MapPost("/", CreatBorrowRecord);
//            borrowRecords.MapPut("/{id}", UpdateBorrowRecord);
//            borrowRecords.MapDelete("/{id}", DeleteBorrowRecord);

//        }

//        static IResult GetAllBorrowRecords(IBorrowService borrowService)
//        {
//            return TypedResults.Ok(borrowService.GetAll());
//        }

//        static IResult GetBorrowRecordById(int id, IBookService bookService) {
//            var borrowRecord = bookService.GetById(id);
//            return borrowRecord is not null?TypedResults.Ok(borrowRecord) : TypedResults.NotFound();
//        }

//        static IResult CreatBorrowRecord(BorrowRecordDto dto, IBorrowService borrowService)
//        {
//            var newBorrowRecord = borrowService.Add(dto);
//            return TypedResults.Created($"/borrowrecords/{newBorrowRecord.Id}", newBorrowRecord);
//        }

//        static IResult UpdateBorrowRecord(int id, BorrowRecordDto dto, IBorrowService borrowService)
//        {
//            var updatedBorrowRecord = borrowService.Update(id, dto);
//            return updatedBorrowRecord is not null ? TypedResults.Ok(updatedBorrowRecord) : TypedResults.NotFound();
//        }

//        static IResult DeleteBorrowRecord(int id, IBorrowService borrowService)
//        {
//            var deleted = borrowService.Delete(id);
//            return deleted ? TypedResults.NoContent() : TypedResults.NotFound();
//        }

//    }

//}





using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;


namespace LibraryManagementAPI.Endpoints;

public static class BorrowEndpoints
{
    public static RouteGroupBuilder MapBorrowEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/borrows");

        group.MapGet("/", GetAllBorrows);
        group.MapGet("/{id}", GetBorrowById);
        group.MapPost("/", CreateBorrow);
        group.MapPut("/{id}", UpdateBorrow);
        group.MapDelete("/{id}", DeleteBorrow);

        return group;
    }

    static IResult GetAllBorrows(IBorrowService borrowService) =>
        TypedResults.Ok(borrowService.GetAll());

    static IResult GetBorrowById(int id, IBorrowService borrowService) =>
        borrowService.GetById(id) is BorrowRecord record
            ? TypedResults.Ok(record)
            : TypedResults.NotFound();

    static IResult CreateBorrow(BorrowRecordDto dto, IBorrowService borrowService, IBookService bookService)
    {
        if (bookService.GetById(dto.BookId) is null)
            return TypedResults.BadRequest($"Book with Id {dto.BookId} does not exist.");

        var record = borrowService.Add(dto);
        return TypedResults.Created($"/borrows/{record.Id}", record);
    }

    static IResult UpdateBorrow(int id, BorrowRecordDto dto, IBorrowService borrowService, IBookService bookService)
    {
        if (bookService.GetById(dto.BookId) is null)
            return TypedResults.BadRequest($"Book with Id {dto.BookId} does not exist.");

        var updated = borrowService.Update(id, dto);
        return updated is not null
            ? TypedResults.Ok(updated)
            : TypedResults.NotFound();
    }

    static IResult DeleteBorrow(int id, IBorrowService borrowService) =>
        borrowService.Delete(id)
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
}
