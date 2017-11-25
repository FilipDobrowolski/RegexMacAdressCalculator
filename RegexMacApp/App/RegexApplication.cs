using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegexMacApp.Helpers;
using RegexMacApp.Exceptions;
using System.IO;

namespace RegexMacApp.App
{
    public static class RegexApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Start(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                log.Info("Nie podano parametrów");
                return;
            }
            WriteCalculatedRegexToConsole(args);
        }

        private static void WriteCalculatedRegexToConsole(string[] args)
        {
            try
            {
                var parameters = ParametersLoaderHelper.LoadParams(args);
                var regexMacAdressCalculator = new CalculatorHelper();
                var calculatedList = regexMacAdressCalculator.Calculate(parameters).Address;
                WriteToConsole(calculatedList);
            }
            catch (WrongInputParameterException ex)
            {
                log.Info("Podano zle parametry wejścia");
                log.Error(String.Format("Podano zle parametry wejścia"), ex);
            }
            catch (ExpressionNotCorrectException ex)
            {
                log.Info("Podany ciąg znakow nie jest w notacji postfiksowej");
                log.Error(String.Format("Podany ciąg znakow nie jest w notacji postfiksowej"), ex);
            }
            catch (FileNotFoundException ex)
            {
                log.Info("Nie znaleziono pliku o podanej nazwie");
                log.Error(String.Format("Nie znaleziono pliku o podanej nazwie"), ex);
            }
            catch (IOException ex)
            {
                log.Info("Blad operacji wejscia-wyjscia");
                log.Error(String.Format("Blad operacji wejscia-wyjscia"), ex);
            }
        }

        private static void WriteToConsole(List<string> calculatedList)
        {
            StringBuilder consoleText = new StringBuilder();
            consoleText.AppendLine("{");
            foreach (var element in calculatedList)
            {
                consoleText.AppendLine(element);
            }
            consoleText.AppendLine("}");
            Console.Write(consoleText);
        }
    }
}
