using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Query.DTO.Taste
{
    public class TasteDTO
    {
        public TasteDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Guid Id { get; }
        public string Name { get; }
    }
}
