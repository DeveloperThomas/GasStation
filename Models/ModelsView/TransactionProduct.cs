using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models.ModelsView
{
    public class TransactionProduct
    {
        public Product Product { get; set; }
        public float Amount { get; set; }

        public TransactionProduct(Product product, float amount)
        {
            this.Product = product;
            this.Amount = amount;
        }
    }
}
