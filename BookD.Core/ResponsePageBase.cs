using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookD.Core
{
    public class ResponsePageBase
    {
        public int Total { get; set; }
        public object Pages { get; set; }
    }
}
