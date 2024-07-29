
using BookManager.Application.UseCases.Subject.GetById;

namespace BookManager.Application.UseCases.Subject.GetByName
{
    public class GetByFilterResponse(IEnumerable<Domain.Entities.Subject> subjects)
    {
        public IEnumerable<GetByIdResponse> Subjects { get; set; } = subjects.Select(subject => new GetByIdResponse(subject));
    }
}
