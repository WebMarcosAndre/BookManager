using BookManager.Application.UseCases.Author.Create;
using BookManager.Application.UseCases.Author.Delete;
using BookManager.Application.UseCases.Author.GetById;
using BookManager.Application.UseCases.Author.GetByName;
using BookManager.Application.UseCases.Author.Update;
using MediatR;

namespace BookManager.API.Endpoints.AuthorEndpoint
{
    public static class AuthorExtensions
    {
        public static WebApplication AuthorConfigureEndpoint(this WebApplication app)
        {
            app.MapPost("/author", async (AuthorRequest request, IMediator mediator) =>
            {
                var response = await mediator.Send(new CreateCommand(request.Name));

                if (response.HasError())
                {
                    return Results.UnprocessableEntity(response.ErrorValidation);
                }

                return Results.Created($"/author/{response.Id}", response);
            })
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status422UnprocessableEntity)
                .WithSummary("Create Author")
                .WithDescription("This endpoint allows the insertion of new author data.")
                .WithName("InsertAuthor")
                .WithTags("Author")
                .WithOpenApi();

            app.MapDelete("/author/{id}", async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new DeleteCommand(id));

                return response.Found ? Results.NoContent() : Results.NotFound();
            })
              .Produces(StatusCodes.Status204NoContent)
              .Produces(StatusCodes.Status404NotFound)
              .Produces(StatusCodes.Status422UnprocessableEntity)
              .WithSummary("Delete Author")
              .WithDescription("This endpoint allows the deletion of author data.")
              .WithName("DeleteAuthor")
              .WithTags("Author")
              .WithOpenApi();

            app.MapPut("/author/{id}", async (int id, AuthorRequest request, IMediator mediator) =>
            {
                var response = await mediator.Send(new UpdateCommand(id, request.Name));

                if (response.HasError())
                {
                    return Results.UnprocessableEntity(response.ErrorValidation);
                }

                return response.Found ? Results.NoContent() : Results.NotFound();
            })
           .Produces(StatusCodes.Status204NoContent)
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status422UnprocessableEntity)
           .WithSummary("Update Author")
           .WithDescription("This endpoint allows the update of author data.")
           .WithName("UpdateAuthor")
           .WithTags("Author")
           .WithOpenApi();

            app.MapGet("/author/{id:int}", async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetByIdQuery(id));

                return response == null ? Results.NotFound() : Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithSummary("Get Author by Id")
            .WithDescription("This endpoint allows the get of author data.")
            .WithName("GetAuthorById")
            .WithTags("Author")
            .WithOpenApi();

            app.MapGet("/author/", async (string? name, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetByFilterQuery(name));
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithSummary("Get Author by name")
            .WithDescription("This endpoint allows the get of author data.")
            .WithName("GetAuthorByFilter")
            .WithTags("Author")
            .WithOpenApi();

            return app;
        }
    }
}
