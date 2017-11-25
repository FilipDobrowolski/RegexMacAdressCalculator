using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexMacApp.Models
{
    public class AdressesList : IExpressionElement
    {
        public List<string> Address { get; set; }

        public Type ReturnTypeOfExpression()
        {
            return Type.OPERAND;
        }
    }
}
