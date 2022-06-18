using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Review
{
    public class AddReviewCommand : ICommand
    {
        public string Content { get; set; }
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public Guid UserId { get; set; }
    }
}
