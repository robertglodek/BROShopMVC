using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.StaticDetails
{
    public static class PagedResultSizes
    {
        public static IEnumerable<int> GetAllowedSizes()
        {
            var sizes = new List<int>();
            sizes.Add(8);
            sizes.Add(16);
            sizes.Add(32);
            return sizes;
        }
        public static int GetAllowedCommentSizes()
        {
            int size = 10;
            return size;
        }
    }
}
