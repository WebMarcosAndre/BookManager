using BookManager.Domain.Interfaces;
using MediatR;

namespace BookManager.Application.UseCases.Subject.Update
{
    public class UpdateHandler(ISubjectRepository subjectRepository) : IRequestHandler<UpdateCommand, ResultResponse>
    {
        private readonly ISubjectRepository _subjectRepository = subjectRepository;
        public async Task<ResultResponse> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var subject = request.ToEntity();

            subject.Validate();

            if (subject.HasError())
            {
                return new ResultResponse(false) { ErrorValidation = subject.ErrorValidation };
            }

            var result = await _subjectRepository.UpdateAsync(subject);
            return new ResultResponse(result);
        }
    }
}
