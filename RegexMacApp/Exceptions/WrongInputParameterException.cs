using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexMacApp.Exceptions
{
    class WrongInputParameterException : Exception
    {
        public WrongInputParameterException(string errorText) : base(errorText) { }
    }
}
