using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Comment
{
    public class EditCommentCommand:ICommand
    {  
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        [Display(Name = "Zawartość")]
        public string Content { get; set; }

        [Display(Name = "Odpowiedź")]
        public string CommentResponse { get; set; }

        [Display(Name = "Zaakceptowany")]
        public bool Allowed { get; set; }

        [Display(Name = "Data publikacji")]
        public DateTimeOffset PublishDateTime { get; set; }
    }
}
