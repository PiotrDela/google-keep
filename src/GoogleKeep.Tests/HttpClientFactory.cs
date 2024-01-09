using Microsoft.AspNetCore.Mvc.Testing;

namespace GoogleKeep.Tests
{
    public class HttpClientFactory
    {
        private static readonly WebApplicationFactory<Program> WebApplicationFactory = new CustomWebApplicationFactory<Program>();

        public static HttpClient Create()
        { 
            return WebApplicationFactory.CreateDefaultClient();
        }
    }
}
