using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        [MaxLength(250)]
        public string Content { get; set; }
        public DateTimeOffset PublishDateTime { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }
        [MaxLength(500)]
        public string CommentResponse { get; set; }
        public bool Allowed { get; set; }
    }
}
