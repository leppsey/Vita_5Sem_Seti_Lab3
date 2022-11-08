using System;
using System.Collections.Generic;
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
                Console.WriteLine($"Горизонтальный контроль по паритету для символа {letter.ToString("X")} - {Parity.ParityHor(letter)}");
            }
            Console.WriteLine($"Вертикальный контроль по паритету для заданного массива- 00{Convert.ToString(Parity.ParityVert(lettersInDecimal), 2)}");
            var f = new OpenFileDialog();
            f.ShowDialog();
            Console.WriteLine();
            uint poly = 517762881;
            var degree = 16;
            Console.WriteLine($"Файл - {f.FileName} CRC - {Convert.ToString(CRC.GetCRC(f.FileName, poly, 16), degree)},Полином - {Convert.ToString(poly, 16)}");
            Console.ReadKey();
            
            //10010111 11100011 10101111 10100000 11100101 10101000 10101101 10100000
        }
    }
}