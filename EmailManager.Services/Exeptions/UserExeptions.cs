using System;

namespace EmailManager.Services.Exeptions
{
    public class UserExeptions : Exception
    {
        public UserExeptions(string message) 
            : base(String.Format(message))
        {
        }
    }
}
