namespace BookManager.Application.UseCases
{
    public class ResultResponse(bool found) : ResponseBase
    {
        public bool Found { get; } = found;
    }
}
