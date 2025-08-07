using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string msg) : base(msg) { }
    }

    public class ValidationException : DomainException
    {
        public ValidationException(string msg) : base(msg) { }
    }
}
