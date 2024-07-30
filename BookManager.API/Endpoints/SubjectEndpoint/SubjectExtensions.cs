using BookManager.Application.UseCases.Subject.Create;
using BookManager.Application.UseCases.Subject.Delete;
using BookManager.Application.UseCases.Subject.GetById;
using BookManager.Application.UseCases.Subject.GetByName;
using BookManager.Application.UseCases.Subject.Update;
using MediatR;

namespace BookManager.API.Endpoints.SubjectEndpoint
{
    public static class SubjectExtensions
    {
        public static WebApplication SubjectConfigureEndpoint(this WebApplication app)
        {
            app.MapPost("/subject", async (SubjectRequest request, IMediator mediator) =>
            {
                var response = await mediator.Send(new CreateCommand(request.Description));

                if (response.HasError())
                {
                    return Results.UnprocessableEntity(response.ErrorValidation);
                }

                return Results.Created($"/subject/{response.Id}", response);
            })
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status422UnprocessableEntity)
                .WithSummary("Create Subject")
                .WithDescription("This endpoint allows the insertion of new author data.")
                .WithName("InsertSubject")
                .WithTags("Subject")
                .WithOpenApi();

            app.MapDelete("/subject/{id}", async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new DeleteCommand(id));

                return response.Found ? Results.NoContent() : Results.NotFound();
            })
              .Produces(StatusCodes.Status204NoContent)
              .Produces(StatusCodes.Status404NotFound)
              .Produces(StatusCodes.Status422UnprocessableEntity)
              .WithSummary("Delete Subject")
              .WithDescription("This endpoint allows the deletion of subject data.")
              .WithName("DeleteSubject")
              .WithTags("Subject")
              .WithOpenApi();

            app.MapPut("/subject/{id}", async (int id, SubjectRequest request, IMediator mediator) =>
            {
                var response = await mediator.Send(new UpdateCommand(id, request.Description));

                if (response.HasError())
                {
                    return Results.UnprocessableEntity(response.ErrorValidation);
                }

                return response.Found ? Results.NoContent() : Results.NotFound();
            })
           .Produces(StatusCodes.Status204NoContent)
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status422UnprocessableEntity)
           .WithSummary("Update Subject")
           .WithDescription("This endpoint allows the update of subject data.")
           .WithName("UpdateSubject")
           .WithTags("Subject")
           .WithOpenApi();

            app.MapGet("/subject/{id:int}", async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetByIdQuery(id));

                return response == null ? Results.NotFound() : Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithSummary("Get Subject by Id")
            .WithDescription("This endpoint allows the get of subject data.")
            .WithName("GetSubjectById")
            .WithTags("Subject")
            .WithOpenApi();

            app.MapGet("/subject/", async (string? description, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetByFilterQuery(description));
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithSummary("Get Subject by description")
            .WithDescription("This endpoint allows the get of subject data.")
            .WithName("GetSubjectByFilter")
            .WithTags("Subject")
            .WithOpenApi();

            return app;
        }
    }
}
