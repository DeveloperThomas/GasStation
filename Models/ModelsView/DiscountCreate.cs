using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models.ModelsView
{
    public class DiscountCreate:Discount
    {
        public List<SelectListItem> TypeOfDiscount { get; set; }
    }
}
