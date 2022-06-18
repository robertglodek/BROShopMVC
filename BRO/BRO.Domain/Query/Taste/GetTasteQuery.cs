using BRO.Domain.Query.DTO.Taste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Taste
{
    public class GetTasteQuery:IQuery<TasteDTO>
    {
        public Guid Id { get; set; }
    }
}
