using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Comment
{
    public class DeleteCommentCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
