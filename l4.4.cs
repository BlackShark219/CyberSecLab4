using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace lab4._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = Encoding.GetEncoding(866);
            Console.InputEncoding = Encoding.GetEncoding(866);
            Dictionary<char, double> alph = new Dictionary<char, double>();
            string path = @"D:\4year\cybersecurity\lab4\";// шлях до текстових файлів
            Console.WriteLine("Name of file: ");
            path = path + Console.ReadLine() + ".txt";
            string text = ReadFile(path);
            //text = Regex.Replace(text, "[.!?,’:'\"—()\n-]", "");
            //Console.WriteLine("Enter a:");
            //int a =int.Parse(Console.ReadLine());
            //Console.WriteLine("Enter b:");
            //int b = int.Parse(Console.ReadLine());
            //string cipher = EncryptAffine(text, a, b);
            //Console.WriteLine();
            //using (StreamWriter writer = new StreamWriter(@"D:\4year\cybersecurity\lab4\cyp3.txt"))
            //{
            //    writer.Write(cipher);
            //}
            //Console.WriteLine();
            //Console.WriteLine(DecryptAffine(cipher,a, b));
            TextCheck(text, alph);
            ShowDict(alph);
            Takeab(alph);
            Console.WriteLine("Try to encrypt");
            Console.WriteLine("Enter a:");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter b:");
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine(DecryptAffine(text, a, b));
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

        static string EncryptAffine(string text,int a,int b)
        {
            string alphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";
            alphabet += alphabet.ToUpper();
            alphabet += " ";
            int m = alphabet.Length;
            string cipher = "";
            for (int i = 0; i < text.Length; i++)
            {
                var char_num = alphabet.IndexOf(text[i]);
                var cipher_num = (a * char_num + b) % m;
                cipher += alphabet[cipher_num];                
            }
            return cipher;
        }

        static string DecryptAffine(string cipher, int a, int b)
        {
            string alphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";
            alphabet += alphabet.ToUpper();
            alphabet += " ";
            int m = alphabet.Length;
            int a_inverse =(ushort) BigInteger.ModPow(a, m - 2, m);
            string text = "";
            for (int i = 0; i < cipher.Length; i++)
            {
                var char_num = alphabet.IndexOf(cipher[i]);
                var cipher_num = (a_inverse * (char_num - b)) % m;
                if (cipher_num < 0)
                {
                    cipher_num = m + cipher_num;
                }
                text += alphabet[cipher_num];
            }
            return text;
        }

        static void TextCheck(string text, Dictionary<char, double> dict)
        {
            var alphabet = text.ToCharArray().Distinct().ToList();
            var textlength = text.Length;

            for (int i = 0; i < alphabet.Count(); i++)
            {
                    double quant = text.Count(x => x == alphabet[i]);
                    double freq = quant / textlength;
                    dict.Add(alphabet[i], freq);
            }
            dict.OrderByDescending(x => x.Value).Take(2);
        }

        static void ShowDict(Dictionary<char, double> dict)
        {
            Console.WriteLine("Letter      Frequency");
            var mostfreq = dict.OrderByDescending(x => x.Value).Take(2);
            foreach (var x in mostfreq)
            {
                Console.WriteLine("{0}      {1,23}", x.Key, x.Value);
            }
        }

        static void Takeab(Dictionary<char, double> dict)
        {
            string alphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";
            alphabet += alphabet.ToUpper();
            alphabet += " ";
            int m = alphabet.Length;
            int a;
            int b;
            string text = "";
            var mostfreq = dict.OrderByDescending(x => x.Value).Take(2);
            Console.WriteLine("Enter x1");
            string x1 = Console.ReadLine();
            Console.WriteLine("Enter x2");
            string x2 = Console.ReadLine();
            int x1i= alphabet.IndexOf(x1);
            int x2i = alphabet.IndexOf(x2);
            var y1 = mostfreq.Select(x => x.Key).FirstOrDefault();
            var y2 = mostfreq.Select(x => x.Key).LastOrDefault();
            int y1i= alphabet.IndexOf(y1);
            int y2i = alphabet.IndexOf(y2);
            int diffy = y2i - y1i;
            int diffx = x2i - x1i;
            if (diffy < 0) diffy = m + diffx;
            if(diffx < 0)
            {
                diffx = -diffx;
                diffy = m - diffy;
            }
            if (diffy % diffx != 0)
            {
                int diffx_inverse = (ushort)BigInteger.ModPow(diffx, m - 2, m);
                a = (diffy * diffx_inverse)% m;
            }
            else
            {
                a = diffy / diffx;
            }
            b = (y1i - x1i * a) % m;
            if (b < 0)
            {
                b = m + b;
            }
            Console.WriteLine("a = {0}, b = {1}", a, b);
        }
    }
}
