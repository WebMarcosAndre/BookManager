using BookManager.API.Endpoints.AuthorEndpoint;
using BookManager.API.Endpoints.BookEndpoint;
using BookManager.API.Endpoints.SubjectEndpoint;

namespace BookManager.API.Endpoints
{
    public static class ConfigureEndpointExtensions
    {
        public static WebApplication ConfigureEndpoints(this WebApplication app)
        {
            return app.AuthorConfigureEndpoint()
                .BookConfigureEndpoint()
                .SubjectConfigureEndpoint();
        }
    }
}
