using BRO.Domain.Command.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class EditProductViewModel
    {
        public EditProductCommand Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Manufacturers { get; set; }
        public IEnumerable<SelectListItem> Tastes { get; set; }

        [Display(Name = "Grafika duża")]
        public IFormFile ImageUrlLarge { get; set; }

        [Display(Name = "Grafika mała")]
        public IFormFile ImageUrlMain { get; set; }
    }
}
