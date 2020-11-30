using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models.ModelsView
{
    public class TransactionProduct:Product
    {
        public float Amount { get; set; }
        public bool InTransaction { get; set; }
        public float SumMoneyForProduct { get; set; }

    }
}
