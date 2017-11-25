using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexMacApp.Models
{
    public class Operation : IExpressionElement
    {
        public string Operator { get; set; }

        public Type ReturnTypeOfExpression()
        {
            return Type.OPERATION;
        }
    }
}
