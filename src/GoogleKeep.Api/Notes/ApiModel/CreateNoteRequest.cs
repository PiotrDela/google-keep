using System.ComponentModel.DataAnnotations;

namespace GoogleKeep.Api.Notes.ApiModel
{
    public class CreateNoteRequest
    {
        [Required]
        public string Title { get; set; }
    }
}
