using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoogleKeep.Api.Notes
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController: Controller
    {
        private readonly ISender sender;

        public NotesController(ISender sender)
        {
            this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteRequest request)
        {
            var noteId = await sender.Send(new CreateNoteCommandCommand(request.Title));

            return Created(string.Empty, noteId);
        }
    }
}
