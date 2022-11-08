using System;
using System.Collections.Generic;
using System.IO;

using _5Sem_Seti_Lab3.NewImplementation;

using Microsoft.Win32;

namespace _5Sem_Seti_Lab3
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            List<byte> lettersInDecimal = new List<byte>() { 0b10010111, 0b11100011, 0b10101111, 0b10100000, 0b11100101, 0b10101000, 0b10101101, 0b10100000 };
            foreach (var letter in lettersInDecimal)
            {
                Console.WriteLine($"Горизонтальный контроль по паритету для символа {letter:X} - {Parity.ParityHor(letter)}");
            }
            Console.WriteLine($"Вертикальный контроль по паритету для заданного массива- 00{Convert.ToString(Parity.ParityVert(lettersInDecimal), 2)}");
            var f = new OpenFileDialog();
            f.ShowDialog();
            Console.WriteLine();
            uint poly = 517762881;
            var degree = 16;

            var p = new CRCParameters()
            {
                Polynom = 0x1EDC6F41,
                RegisterInitial = 0xFFFFFFFF,
                IsInputReflection = true,
                IsOutputReflection = true,
                OutputSum = 0xFFFFFFFF,
            };

            var crc = new CRC32C(p);
            var fs = File.Open(f.FileName, FileMode.Open);
            var hash = crc.CalculateHash(fs);
            Console.WriteLine($"Файл - {f.FileName} CRC - {Convert.ToString(hash,16)},Полином - {Convert.ToString(p.Polynom, 16)}");
            Console.ReadKey();
            
            //10010111 11100011 10101111 10100000 11100101 10101000 10101101 10100000
        }
    }
}