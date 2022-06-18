using BRO.Domain.Query.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Category
{
    public class GetCategoryQuery:IQuery<CategoryDTO>
    {
        public Guid Id { get; set; }
    }
}
