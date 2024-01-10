using GoogleKeep.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleKeep.Tests
{
    public class NotesControllerTestCase
    {
        private static readonly NotesWebApplicationFactory webApplicationFactory = new NotesWebApplicationFactory();

        public NotesControllerTestCase WithNote(Note note)
        {
            var repository = webApplicationFactory.Services.GetService<INoteRepository>();
            repository.AddAsync(note).GetAwaiter().GetResult();
            return this;
        }

        public HttpClient CreateClient()
        {
            return webApplicationFactory.CreateClient();
        }
    }
}
