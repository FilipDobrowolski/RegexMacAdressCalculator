using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegexMacApp.Models;

namespace RegexMacApp.Helpers
{
    public static class ProducersFilterHelper
    {
        //dane mac producers: http://standards-oui.ieee.org/oui/oui.txt
        static readonly string[] MacProducers = {"94-EB-2C", "98-D2-93", "70-3A-CB",
            "88-3D-24", "F4-F5-D8", "F4-F5-E8", "F8-8F-CA", "94-95-A0", "F4-03-04", "00-03-7F",
            "00-13-74", "B0-26-28", "3C-5A-B4", "00-1A-11", "08-9E-08",
            "48-D6-D5", "54-60-08", "A4-77-33",
            "E4-F0-42", "D8-6C-63", "20-DF-B9" };

        public static AdressesList Filter(AdressesList addressesList)
        {
            AdressesList filteredAdresses = new AdressesList() { Address = new List<string> { } };

            foreach (string adress in addressesList.Address)
            {
                FilterCorrectMacAdressFromList(ref filteredAdresses, adress);
            }

            return filteredAdresses;
        }

        private static void FilterCorrectMacAdressFromList(ref AdressesList filteredAdresses, string adress)
        {
            if (MacProducers.Where(mac => adress.StartsWith(mac)).SingleOrDefault() != null)
            {
                filteredAdresses.Address.Add(adress);
            }
        }
    }
}
