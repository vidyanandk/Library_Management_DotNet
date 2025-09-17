using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
namespace LibraryManagementAPI.EndPoints
{
    public static class AuthorEndpoints
    {
        public static RouteGroupBuilder MapAuthorEndpoints(this IEndpointRouteBuilder routes)
        {
            var Authors = routes.MapGroup("/authors");
            Authors.MapGet("/", GetAllAuthors);
            Authors.MapGet("/{id}", GetAuthorById);
            Authors.MapPost("/", CreateAuthor);
            Authors.MapPut("/{id}", UpdateAuthor);
            Authors.MapDelete("/{id}", DeleteAuthor);

            return Authors;
        }

        static IResult GetAllAuthors(IAuthorService authorService)
        {
            var authors=authorService.GetAll();
            return TypedResults.Ok(authors);
        }

        static IResult GetAuthorById(int id, IAuthorService authorService)
        {
            var author = authorService.GetById(id);
            if (author is null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(author);
        }

        static IResult CreateAuthor(AuthorDto authorDto, IAuthorService authorService)
        {
            var newAuthor = authorService.Add(authorDto);
            return TypedResults.Created($"/authors/{newAuthor.Id}", newAuthor);
        }

        static IResult UpdateAuthor(int id, AuthorDto authorDto, IAuthorService authorService)
        {
            var updatedAuthor = authorService.Update(id, authorDto);
            
            return updatedAuthor is null? TypedResults.NotFound() : TypedResults.Ok(updatedAuthor);
        }

        static IResult DeleteAuthor(int id, IAuthorService authorService)
        {
            var deleted = authorService.Delete(id);
            return deleted ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
