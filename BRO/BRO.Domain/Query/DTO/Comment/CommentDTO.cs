using BRO.Domain.Query.DTO.ApplicationUser;
using BRO.Domain.Query.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.Comment
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset PublishDateTime { get; set; }

        public string ShortDateTimeOffset { get; set; }
        public Guid ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int Rating { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUserDTO User { get; set; }
        public bool Allowed { get; set; }
        public string CommentResponse { get; set; }


    }
}
