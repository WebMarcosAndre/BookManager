using BookManager.Application.UseCases.Book.Create;
using BookManager.Application.UseCases.Book.Delete;
using BookManager.Application.UseCases.Book.GetByFilter;
using BookManager.Application.UseCases.Book.GetById;
using BookManager.Application.UseCases.Book.Update;
using MediatR;

namespace BookManager.API.Endpoints.BookEndpoint
{
    public static class BookExtensions
    {
        public static WebApplication BookConfigureEndpoint(this WebApplication app)
        {
            app.MapPost("/book", async (BookRequest request, IMediator mediator) =>
            {
                var command = new CreateCommand(
                                request.Title,
                                request.PublisherBook,
                                request.Edition,
                                request.YearPublication,
                                request.AuthorIds,
                                request.SubjectIds);
                var response = await mediator.Send(command);

                if (response.HasError())
                {
                    return Results.UnprocessableEntity(response.ErrorValidation);
                }

                return Results.Created($"/book/{response.Id}", response);

            }).Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status422UnprocessableEntity)
                .WithName("InsertBook")
                .WithTags("Book");

            app.MapDelete("/book/{id}", async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new DeleteCommand(id));

                return response.Found ? Results.NoContent() : Results.NotFound();
            })
           .Produces(StatusCodes.Status204NoContent)
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status422UnprocessableEntity)
           .WithSummary("Delete Book")
           .WithDescription("This endpoint allows the deletion of book data.")
           .WithName("DeleteBook")
           .WithTags("Book")
           .WithOpenApi();

            app.MapPut("/book/{id}", async (int Id, BookRequest request, IMediator mediator) =>
            {
                var command = new UpdateCommand(Id,
                                request.Title,
                                request.PublisherBook,
                                request.Edition,
                                request.YearPublication,
                                request.AuthorIds,
                                request.SubjectIds);

                var response = await mediator.Send(command);

                if (response.HasError())
                {
                    return Results.UnprocessableEntity(response.ErrorValidation);
                }

                return response.Found ? Results.NoContent() : Results.NotFound();

            }).Produces(StatusCodes.Status201Created)
              .Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status422UnprocessableEntity)
              .WithName("UpdateBook")
              .WithTags("Book")
              .WithOpenApi();

            app.MapGet("/book/{id:int}", async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetByIdQuery(id));

                return response == null ? Results.NotFound() : Results.Ok(response);
            })
           .Produces(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status422UnprocessableEntity)
           .WithSummary("Get Book by Id")
           .WithDescription("This endpoint allows the get of book data.")
           .WithName("GetBookById")
           .WithTags("Book")
           .WithOpenApi();

            app.MapGet("/book/",
                async (string? title, string? publisherBook, int? edition, string? yearOfPublication, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetByFilterQuery(title, publisherBook, edition, yearOfPublication));
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithSummary("Get Book by Filters")
            .WithDescription("This endpoint allows the get of book data.")
            .WithName("GetBookByFilter")
            .WithTags("Book")
            .WithOpenApi();

            return app;
        }
    }
}
