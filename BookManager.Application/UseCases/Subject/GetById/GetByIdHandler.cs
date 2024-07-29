using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Subject.GetById
{
    public class GetByIdHandler(ISubjectQuery subjectQuery) : IRequestHandler<GetByIdQuery, GetByIdResponse>
    {
        private readonly ISubjectQuery _subjectQuery = subjectQuery;

        public async Task<GetByIdResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await _subjectQuery.Get(request.Id);

            if (subject == null)
            {
                return null;
            }

            var response = new GetByIdResponse(subject);

            return response;
        }
    }
}
