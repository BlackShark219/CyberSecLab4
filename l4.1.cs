using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1_1_
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, double> alph = new Dictionary<char, double>();
            string path = @"D:\4year\cybersecurity\lab4\";// шлях до текстових файлів
            Console.WriteLine("Name of file: ");
            path = path + Console.ReadLine() + ".txt";
            string text = ReadFile(path);
            text = Regex.Replace(text, "[.!?,’:'\"—()\n-]", "");
            TextCheck(text, alph);
            ShowDict(alph);
            Console.ReadLine();

        }

        static string ReadFile(string pathFile)
        {
            using (StreamReader file = new StreamReader(pathFile, Encoding.GetEncoding("UTF-8")))
            {
                string ln;
                string text = "";

                while ((ln = file.ReadLine()) != null)
                {
                    text += ln + "\n";
                }
                return text;
            }
        }

        static void TextCheck(string text, Dictionary<char, double> dict)
        {
            var alphabet = text.ToCharArray().Distinct().ToList();
            var textlength = text.Length;

            for (int i = 0; i < alphabet.Count(); i++)
            {
                {
                    double quant = text.Count(x => x == alphabet[i]);
                    double freq = quant / textlength;
                    dict.Add(alphabet[i], freq);
                }

            }
        }

        static void ShowDict(Dictionary<char, double> dict)
        {

            Console.WriteLine("Letter      Frequency");
            foreach (var x in dict.OrderByDescending(x => x.Value))
            {
                Console.WriteLine("{0}      {1,23}", x.Key, x.Value);
            }
        }

    }
}
