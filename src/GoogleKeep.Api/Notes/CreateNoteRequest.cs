using System.ComponentModel.DataAnnotations;

namespace GoogleKeep.Api.Notes
{
    public class CreateNoteRequest
    {
        [Required]
        public string Title { get; set; }
    }
}
