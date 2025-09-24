using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChezTartine;

public class StocksInsuffisantsException: Exception
{
    public StocksInsuffisantsException(string message) : base(message)
    {

    }
}
