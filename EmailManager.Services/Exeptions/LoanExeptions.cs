using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Services.Exeptions
{
    public class LoanExeptions:Exception
    {
        public LoanExeptions(string message) : base(String.Format(message))
        {

        }
    }
}
