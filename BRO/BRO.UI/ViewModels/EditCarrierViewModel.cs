using BRO.Domain.Command.Carrier;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class EditCarrierViewModel
    {
        public EditCarrierCommand Command { get; set; }

        [Display(Name = "Grafika")]
        public IFormFile Image { get; set; }
    }
}
