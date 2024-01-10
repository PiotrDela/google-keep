using GoogleKeep.Api.Notes.ApiModel;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNote([FromBody]CreateNoteRequest request)
        {
            var noteId = await sender.Send(new CreateNoteCommandCommand(request.Title));

            return CreatedAtRoute("GetNoteRoute", new { id = noteId.Value }, null);
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetNoteRoute")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNote([FromRoute]Guid id)
        {
            var note = await sender.Send(new GetNoteByIdQuery(id));
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }
    }
}
