using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public class JourneyException : SystemException
    {
        public JourneyException(string message) : base(message)
        {
        }
    }
}