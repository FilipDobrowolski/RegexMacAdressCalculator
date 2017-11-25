using log4net;
using RegexMacApp.Exceptions;
using RegexMacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//odworcona notacja polska
// algorytm czyta czyta dopki nie znajdzie znaku, jak znajdzie to sciaga ostatnie 2 ze stostu
// plik1.txt plik2.txt u
namespace RegexMacApp.Helpers
{
    public class CalculatorHelper
    {
        private static readonly ILog LOG = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AdressesList Calculate(List<IExpressionElement> expression)
        {
            var stack = new Stack<IExpressionElement>();
            foreach (IExpressionElement element in expression)
            {
                if (element.ReturnTypeOfExpression() == Models.Type.OPERAND)
                {
                    stack.Push(element);
                }
                else if (element.ReturnTypeOfExpression() == Models.Type.OPERATION)
                {
                    if (stack.Count < 2)
                    {
                        throw new ExpressionNotCorrectException("Na stosie musi byc wiecej niz 2 operandy");
                    }
                    else
                    {
                        stack.Push(MakeOperation((AdressesList)stack.Pop(),(AdressesList)stack.Pop(), (Operation) element));
                    }
                }
            }
            if (stack.Count != 1)
            {
                throw new ExpressionNotCorrectException("Na stosie został jakiś element");
            }
            return (AdressesList)stack.Pop();
        }

        public AdressesList MakeOperation(AdressesList parameterListOne, AdressesList paramListTwo, Operation operation)
        {
            AdressesList result = new AdressesList() { Address = new List<string>() { } };
            switch (operation.Operator)
            {
                case "u":
                    result = UnionOperation(paramListTwo, parameterListOne);
                    break;
                case "n":
                    result = IntersectionOperation(paramListTwo, parameterListOne);
                    break;
                case "\\":
                    result = RelativeComplementOperation(paramListTwo, parameterListOne);
                    break;
            }
            return result;
        }
        private AdressesList IntersectionOperation(AdressesList parameterListOne, AdressesList paramListTwo)
        {
            return new AdressesList() { Address = parameterListOne.Address.Intersect(paramListTwo.Address).ToList() };
        }

        private AdressesList UnionOperation(AdressesList parameterListOne, AdressesList paramListTwo)
        {
            return new AdressesList() { Address = parameterListOne.Address.Union(paramListTwo.Address).ToList() };
        }

        private AdressesList RelativeComplementOperation(AdressesList parameterListOne, AdressesList paramListTwo)
        {
            return new AdressesList() { Address = parameterListOne.Address.Except(paramListTwo.Address).ToList() };
        }
    }
}
