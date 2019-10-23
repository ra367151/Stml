using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Applications.Exceptions
{
    public class FriendlyException : Exception
    {
        public FriendlyException()
            : base("系统发生了一个致命的错误！")
        {

        }

        public FriendlyException(string message)
            : base(message)
        {

        }

        public FriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
