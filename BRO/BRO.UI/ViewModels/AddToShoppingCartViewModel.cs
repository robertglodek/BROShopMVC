using BRO.Domain.Command.ShoppingCart;
using BRO.Domain.Query.DTO.Comment;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.DTO.Review;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class AddToShoppingCartViewModel
    {
        public AddShoppingCartItemCommand Command { get; set; }
        public ReviewDTO OwnOpinion { get; set; }
        public ProductDetailsDTO Product {get;set;}
        public CommentsPagedResult<CommentDTO> Comments { get; set; }
        public IEnumerable<SelectListItem> Tastes { get; set; }

    }
}
