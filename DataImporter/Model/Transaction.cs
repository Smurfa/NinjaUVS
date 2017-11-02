using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Model
{
    public class Transaction
    {
        public string TransactionType { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
    }
}
