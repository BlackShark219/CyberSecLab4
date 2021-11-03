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
            Dictionary<string, double> alph = new Dictionary<string, double>();
            string path = @"D:\4year\cybersecurity\lab4\";// шлях до текстових файлів
            Console.WriteLine("Name of file: ");
            string text = ReadFile(path + Console.ReadLine() + ".txt");
            text = Regex.Replace(text, "[.!?,’:'\"—()\n-]", "");
            text = Regex.Replace(text, @"\s+", "");
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

        static void TextCheck(string text, Dictionary<string, double> dict)
        {

            List<string> bigramtext = new List<string>();
            for (int i = 0; i < text.Length; i ++)
            {
                try
                {
                    bigramtext.Add(text.Substring(i, 2));
                }

                catch
                {
                    break;
                }
               
            }
            List<string> unbigram = bigramtext.Distinct().ToList();
            for (int i = 0; i < unbigram.Count(); i++)
            {
                {
                    double quant = bigramtext.Count(x => x == unbigram[i]);
                    double freq = quant / text.Length;
                    dict.Add(unbigram[i], freq);
                }

            }
        }

        static void ShowDict(Dictionary<string, double> dict)
        {
            Console.WriteLine("Bigram      Frequency");
            foreach (var x in dict.OrderByDescending(x => x.Value).Take(30))
            {
                Console.WriteLine("{0}      {1,23}", x.Key, x.Value);
            }
        }

    }
}
