using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Services.Exeptions
{
    public class UserExeptions : Exception
    {
        public UserExeptions(string masege)
        : base(String.Format(masege))
        {
        }
    }
}
