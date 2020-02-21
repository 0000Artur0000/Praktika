using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VIN_LIB

{
    public class VIN_LIB
    {
        dic dic = new dic();
        public bool CheckVIN(string vin)
        {
            if (vin.Length != 17) return false;
            Regex regex = new Regex(@"[A-H,J-N,P,R-Z,\d]{13}[\d]{4}");
            if (!regex.IsMatch(vin))
                return false;
            if (!CheckKontrSumm(vin))
                return false;
            string country = CheckCountry(vin.Substring(0, 2));
            if (String.IsNullOrEmpty(country))
                return false;
            if (country== "не используется")
                return false;
            if (!(CheckYear(vin[9]) > 0))
                return false;
            return true;
        }
        public string GetVINCountry(string vin)
        {
            if (!CheckVIN(vin)) return null;
            return CheckCountry(vin.Substring(0, 2));
        }
        public int GetTransportYear(string vin)
        {
            if (!CheckVIN(vin)) return 0;
            return CheckYear(vin[9]);
        }
        private bool CheckKontrSumm(string vin)
        {
            Regex regex = new Regex(@"[\d,X]");
            if (!regex.IsMatch(vin[8].ToString()))
                return false;
            int[] j = new int[17];
            int[] ves = { 8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 17; i++)
                if (char.IsLetter(vin[i]))
                    if (i == 8) j[i] = 10;
                    else j[i] = dic.alf[vin[i]];
                else j[i] = Int16.Parse(vin[i].ToString());
            int b = 0;
            for (int i = 0; i < 17; i++)
            {
                if (i == 8) continue;
                b += (j[i] * ves[i]);
            }
            int l = b / 11;
            int CHK = b - (l * 11);
            //Console.Write("\n\n" +b+" "+ l + " " + CHK + " " + j[8]+"\n\n");
            if (!(j[8] == CHK)) return false;
            return true;
        }
        private string CheckCountry(string vin)
        {
            string c = vin.Substring(0, 1);
            string s;
            for (int i = 0; i < dic.slo.Count; i++)
                if (dic.slo.ElementAt(i).Key.Contains(c))
                {
                    if (dic.alfa.Substring(dic.alfa.IndexOf(dic.slo.ElementAt(i).Key.Substring(1, 1)), 1 + dic.alfa.IndexOf(dic.slo.ElementAt(i).Key.Substring(4, 1)) - dic.alfa.IndexOf(dic.slo.ElementAt(i).Key.Substring(1, 1))).Contains(vin.Substring(1, 1)))
                    {
                        s = dic.slo.ElementAt(i).Value;
                        return s;
                    }
                }
            return null;
        }
        private int CheckYear(char vin)
        {
            Regex rg = new Regex(@"[A-H,J-N,P,R-Y,1-9]");
            if (!rg.IsMatch(vin.ToString())) return 0;
            return dic.year[vin];
        }
    }
}

