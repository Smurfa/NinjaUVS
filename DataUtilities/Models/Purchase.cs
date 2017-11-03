using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtilities.Models
{
    public class Purchase : TransactionBase
    {
        public int NumOfShares { get; set; }
        public float SharePrice { get; set; }
    }
}
