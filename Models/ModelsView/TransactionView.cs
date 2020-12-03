using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models.ModelsView
{
    public class TransactionView:Transaction
    {
        public string NameOfPayment { get; set; }
    }
}
