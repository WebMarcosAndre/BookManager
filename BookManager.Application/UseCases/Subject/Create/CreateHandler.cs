using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Subject.Create
{
    public class CreateHandler(ISubjectRepository subjectRepository) : IRequestHandler<CreateCommand, CreateResponse>
    {
        private readonly ISubjectRepository _subjectRepository = subjectRepository;
        public async Task<CreateResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var subject = request.ToEntity();

            if (subject.HasError())
            {
                return new CreateResponse(subject) { ErrorValidation = subject.ErrorValidation };
            }

            await _subjectRepository.InsertAsync(subject);

            return new CreateResponse(subject);
        }
    }
}
