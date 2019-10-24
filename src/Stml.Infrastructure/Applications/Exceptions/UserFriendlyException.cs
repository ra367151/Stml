using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException()
            : base("系统发生了一个致命的错误！")
        {

        }

        public UserFriendlyException(string message)
            : base(message)
        {

        }

        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
