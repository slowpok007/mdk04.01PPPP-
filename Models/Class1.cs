using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPP.Models
{
    internal class Class1
    {
        public static D1E c;
        public static D1E context
        {
            get
            {
                if (c == null)
                    c = new D1E();
                return c;
            }
        }
    }
}
