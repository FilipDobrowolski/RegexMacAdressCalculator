using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexMacApp.Exceptions
{
    class ExpressionNotCorrectException : Exception 
    {
        public ExpressionNotCorrectException (string errorText) : base(errorText) { }
    }
}
