using Microsoft.AspNetCore.Http;

namespace VSphere.Services
{
    public class RequestHandler
    {
        public IHttpContextAccessor _httpContextAccessor;

        public RequestHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
