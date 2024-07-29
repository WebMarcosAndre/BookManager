using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Subject.GetByName
{
    public class GetByFilterHandler(ISubjectQuery subjectQuery) : IRequestHandler<GetByFilterQuery, GetByFilterResponse>
    {
        private readonly ISubjectQuery _subjectQuery = subjectQuery;

        public async Task<GetByFilterResponse> Handle(GetByFilterQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectQuery.Get(request.Description);

            var response = new GetByFilterResponse(subjects);

            return response;
        }
    }
}
