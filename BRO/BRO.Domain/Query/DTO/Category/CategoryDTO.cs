using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Query.DTO.Category
{
    public class CategoryDTO
    {
        public CategoryDTO(Guid id,string name)
        {
            Id = id;
            Name = name;

        }

        public Guid Id { get; }
        public string Name { get;  }
    }
}
