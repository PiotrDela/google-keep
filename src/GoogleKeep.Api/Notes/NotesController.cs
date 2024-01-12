using GoogleKeep.Api.Notes.ApiModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleKeep.Api.Notes
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController: Controller
    {
        private readonly ISender sender;
        private readonly IHttpContextAccessor httpContextAccessor;

        public NotesController(ISender sender, IHttpContextAccessor httpContextAccessor)
        {
            this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNote([FromBody]CreateNoteRequest request)
        {
            var requestingUserId = httpContextAccessor.ParseUserId();
            if (requestingUserId.HasValue == false)
            {
                return Unauthorized();
            }

            var noteId = await sender.Send(new CreateNoteCommandCommand(request.Title, requestingUserId.Value));

            return CreatedAtRoute("GetNoteRoute", new { id = noteId.Value }, null);
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetNoteRoute")]
        [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNote([FromRoute]Guid id)
        {
            var requestingUserId = httpContextAccessor.ParseUserId();
            if (requestingUserId.HasValue == false)
            {
                return Unauthorized();
            }

            try
            {
                var note = await sender.Send(new GetNoteByIdQuery(id, requestingUserId.Value));
                if (note == null)
                {
                    return NotFound();
                }

                return Ok(note);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NoteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotes()
        {
            var requestingUserId = httpContextAccessor.ParseUserId();
            if (requestingUserId.HasValue == false)
            {
                return Unauthorized();
            }

            var notes = await sender.Send(new GetUserNotesQuery(requestingUserId.Value));

            return Ok(notes);
        }
    }
}
