using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RegexMacApp.Models;
using RegexMacApp.Exceptions;
using System.IO;
using RegexMacApp.Consts;

namespace RegexMacApp.Helpers
{
    public static class ParametersLoaderHelper
    {
        static readonly string FILE_REGEXP = "[a-zA-z0-9_]+\\.[a-zA-z]{1,3}"; // walidacja nazwy pliku
        static readonly string OPERATION_REGEXP = "[\\\\un]"; // operacje
        static readonly string MAC_REGEXP = "([0-9A-F]{2}-){5}[0-9A-F]{2}"; // bo minus 5 razy
        

        public static List<IExpressionElement> LoadParams(String[] call_args)
        {
            var expressions = new List<IExpressionElement>();

            foreach (string arg in call_args)
            {
                IsRegexMatchMethod(ref expressions, arg);
            }
            return expressions;
        }

        private static void IsRegexMatchMethod(ref List<IExpressionElement> expressions, string arg)
        {
            if (Regex.IsMatch(arg, FILE_REGEXP))
            {
                var filePath = Const.GetPath(arg);
                var adressesList = new AdressesList() { Address = LoadMacAdressesFromFile(filePath) };
                expressions.Add(ProducersFilterHelper.Filter(adressesList));
            }
            else if (Regex.IsMatch(arg, OPERATION_REGEXP))
            {
                expressions.Add(new Operation { Operator = arg });
            }
            else
            {
                throw new WrongInputParameterException("Podano zle parametry");
            }
        }

        public static List<string> LoadMacAdressesFromFile(string filePatch)
        {
            try
            {
                var macAdressesFromFile = new List<string>();
                var regex = new Regex(MAC_REGEXP);
                foreach (var line in File.ReadLines(filePatch).Where(line => regex.Matches(line).Count > 0))
                {
                   macAdressesFromFile.AddRange(regex.Matches(line).Cast<Match>().Select(match => match.Value).ToList());
                }
                return macAdressesFromFile;
            }
            catch
            {
                throw new IOException();
            }
        }
    }
}
