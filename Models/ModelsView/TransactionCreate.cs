using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models.ModelsView
{
    public class TransactionCreate
    {
        public Transaction Transaction { get; set; }
        public List<TransactionProduct> TransactionProduct { get; set; }
        public List<DistributorInTransaction> DistributorInTransaction { get; set; }
        public List<SelectListItem> TypesOfPayment { get; set; }
        public string NameOfPayment { get; set; }
    }

}
