using System;

namespace EmailManager.Services.Exeptions
{
   public class EmailExeptions : Exception
    {
        public EmailExeptions(string message)
            : base(String.Format(message))
        {

        }
    }
}
