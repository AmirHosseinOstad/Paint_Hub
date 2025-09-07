using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Paint_Hub
{

#pragma warning disable

    public class AtomicData
    {
        public string Part1Text { get; set; }
        public string Part2Text { get; set; }
        public string Part1Symbol { get; set; }
        public string Part2Symbol { get; set; }
        public string Part1Number { get; set; }
        public string Part2Number { get; set; }
        public (int StartIndex, int EndIndex) Part1Index { get; set; }
        public (int StartIndex, int EndIndex) Part2Index { get; set; }

    }

    internal class GetAtomicData
    {
        private string[] symbols =
        {
            "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne",
            "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar", "K", "Ca",
            "Sc", "Ti", "V", "Cr", "Mn", "Fe", "Co", "Ni", "Cu", "Zn",
            "Ga", "Ge", "As", "Se", "Br", "Kr", "Rb", "Sr", "Y", "Zr",
            "Nb", "Mo", "Tc", "Ru", "Rh", "Pd", "Ag", "Cd", "In", "Sn",
            "Sb", "Te", "I", "Xe", "Cs", "Ba", "La", "Ce", "Pr", "Nd",
            "Pm", "Sm", "Eu", "Gd", "Tb", "Dy", "Ho", "Er", "Tm", "Yb",
            "Lu", "Hf", "Ta", "W", "Re", "Os", "Ir", "Pt", "Au", "Hg",
            "Tl", "Pb", "Bi", "Po", "At", "Rn", "Fr", "Ra", "Ac", "Th",
            "Pa", "U", "Np", "Pu", "Am", "Cm", "Bk", "Cf", "Es"
        };

        private string[] atomicNumbers =
        {
            "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
            "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
            "21", "22", "23", "24", "25", "26", "27", "28", "29", "30",
            "31", "32", "33", "34", "35", "36", "37", "38", "39", "40",
            "41", "42", "43", "44", "45", "46", "47", "48", "49", "50",
            "51", "52", "53", "54", "55", "56", "57", "58", "59", "60",
            "61", "62", "63", "64", "65", "66", "67", "68", "69", "70",
            "71", "72", "73", "74", "75", "76", "77", "78", "79", "80",
            "81", "82", "83", "84", "85", "86", "87", "88", "89", "90",
            "91", "92", "93", "94", "95", "96", "97", "98", "99"
        };

        public AtomicData GetData(string Text, bool IsRepoName)
        {
            string[] Input;
            if (IsRepoName)
            {
                Input = SplitRepoName(Text);
            }
            else
            {
                Input = new string[] { Text };
            }

            AtomicData Data = new AtomicData();

            if (Input.Length == 1)
            {
                (string symbol, int index, int StartIndex, int EndIndex) Object = FindSymbol(Input[0]);

                Data.Part1Symbol = Object.symbol;
                Data.Part1Number = atomicNumbers[Object.index];
                Data.Part1Index = (Object.StartIndex, Object.EndIndex);


                Data.Part1Text = Input[0];
                Data.Part2Text = "Repo";

                if (Data.Part1Symbol == "P")
                {
                    Data.Part2Symbol = "O";
                    Data.Part2Number = "08";
                    Data.Part2Index = (3, 3);
                }
                else
                {
                    Data.Part2Symbol = "P";
                    Data.Part2Number = "17";
                    Data.Part2Index = (2, 2);

                }
            }
            else
            {
                (string symbol, int index, int StartIndex, int EndIndex) Object = FindSymbol(Input[0]);

                Data.Part1Symbol = Object.symbol;
                Data.Part1Number = atomicNumbers[Object.index];
                Data.Part1Text = Input[0];
                Data.Part1Index = (Object.StartIndex, Object.EndIndex);

                (string symbol, int index, int StartIndex, int EndIndex) Object_2 = FindSymbol(Input[1], Object.symbol.ToLower());

                Data.Part2Symbol = Object_2.symbol;
                Data.Part2Number = atomicNumbers[Object_2.index];
                Data.Part2Text = Input[1];
                Data.Part2Index = (Object_2.StartIndex, Object_2.EndIndex);
            }

            return Data;
        }

        private static string[] SplitRepoName(string repoName)
        {
            if (string.IsNullOrWhiteSpace(repoName))
                return Array.Empty<string>();

            // 1- جدا کردن با علائم رایج
            string[] parts = Regex.Split(repoName, @"[-_.\/]");

            // حذف خالی‌ها و حذف بخش‌های عددی
            parts = Array.FindAll(parts, s => !string.IsNullOrWhiteSpace(s) && !Regex.IsMatch(s, @"^\d+$"));

            if (parts.Length == 0)
                return Array.Empty<string>();

            // 2- اگر فقط یه بخش باقی موند → بررسی CamelCase
            if (parts.Length == 1)
            {
                var camelParts = Regex.Split(parts[0], @"(?=[A-Z])");

                // حذف خالی‌ها و بخش‌های عددی
                camelParts = Array.FindAll(camelParts, s => !string.IsNullOrWhiteSpace(s) && !Regex.IsMatch(s, @"^\d+$"));

                if (camelParts.Length == 0)
                    return Array.Empty<string>();
                else if (camelParts.Length == 1)
                    return new string[] { camelParts[0] };
                else if (camelParts.Length >= 2)
                    return new string[] { camelParts[0], camelParts[1] };
            }

            // 3- اگر بیشتر از یه بخش بود
            if (parts.Length == 1)
                return new string[] { parts[0] };
            else if (parts.Length == 2)
                return new string[] { parts[0], parts[1] };
            else
                return new string[] { parts[0], parts[1] }; // بیشتر → فقط دو بخش اول
        }
        private (string symbol, int index, int StartIndex, int EndIndex) FindSymbol(string input, string? preSymbol = null)
        {
            string[] symbolsLower = new string[symbols.Length];

            for (int i = 0; i < symbolsLower.Length; i++)
            {
                symbolsLower[i] = symbols[i].ToLower();
            }

            // مرحله 1: یک حرف یک حرف بررسی کن
            for (int i = 0; i < input.Length; i++)
            {
                string oneChar = input[i].ToString().ToLower();
                int idx = Array.IndexOf(symbolsLower, oneChar);
                if (idx >= 0 && oneChar != preSymbol)
                {
                    return (symbols[idx], idx, i, i);
                }
            }

            // مرحله 2: دو حرف دو حرف بررسی کن
            for (int i = 0; i < input.Length - 1; i++)
            {
                string twoChars = input.Substring(i, 2).ToLower();
                int idx = Array.IndexOf(symbolsLower, twoChars);
                if (idx >= 0 && twoChars != preSymbol)
                {
                    return (symbols[idx], idx, i, i + 1);
                }
            }

            // چیزی پیدا نشد
            for (int i = 0; i < input.Length; i++)
            {
                string oneChar = input[i].ToString();
                if (oneChar != preSymbol)
                {
                    return (oneChar, 0, i, i);
                }
            }

            return ("", 0, 0, 0);
        }
    }
}
