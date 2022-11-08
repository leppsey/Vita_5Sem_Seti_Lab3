using System;
using System.IO;

namespace _5Sem_Seti_Lab3
{
    internal static class CRC
    {
        public static uint GetCRC(string filename, uint poly, int degreePoly)
        {
            var FileStream = File.Open(filename, FileMode.Open); /// открываем файл
            var register = new Register(degreePoly); // создаем регистр
            for (uint i = 0; i < FileStream.Length; i++) // проводим наши махинации до того момента пока не кончатся байты
            {
                // фигня снизу для очень больших программ, чтобы понимать насколько штуки выполнились
                //if (i % 1000000 == 0)
                //{
                //    Console.WriteLine($"обработка {i}-ого байта, всего байт - {FileStream.Length}, процент выполнения - {((i * 1.0) / FileStream.Length) * 100}%");
                //}

                var bytee = FileStream.ReadByte(); // читаем новый байт
                for (var j = 7; j >= 0; j--) // проход по битам из прочитанного байта
                {
                    //s += getBit(bytee, j);
                    //Console.WriteLine($"Регистр до вставки бита - {Convert.ToString(register.Value, 2)}");
                    register.InsertBit(getBit(bytee, j)); // вставляем бит из байта
                    //Console.WriteLine($"Регистр после вставки бита - {Convert.ToString(register.Value, 2)}");
                    if (register.FirtsBitWasOne) // если выдвинутый бит был единицей, то XOR'им регистр и полином
                    {
                        register = register ^ poly; // тут пригодилаь перегрузка оператора XOR
                        //Console.WriteLine($"Регистр после XOR - {Convert.ToString(register.Value, 16)}");
                    }
                }
            }

            for (var i = 0; i < degreePoly; i++) // тут добавляем нолики и делаем действия аналогичные тем, что были сверху
            {
                register.InsertBit(0);
                if (register.FirtsBitWasOne)
                {
                    register = register ^ poly;
                }
            }

            return register.Value;
        }


        /// <summary>
        ///     Функция получает определенный бит в байте
        /// </summary>
        /// <param name="bytee"> Байт, из которого надо получить бит</param>
        /// <param name="pos"> Номер бита, который надо получить</param>
        /// <returns> Бит на заданной позиции</returns>
        private static byte getBit(int bytee, int pos)
        {
            //Console.WriteLine($"Байт - {Convert.ToString(bytee, 2)}, позиция - {pos}, ответ - {(byte) ((bytee >> pos) & 1)}");

            return (byte) ((bytee >> pos) & 1);
        }
    }

    /// <summary>
    ///     Класс с регистром для более удобной работы с ним
    /// </summary>
    internal class Register
    {
        private readonly uint bitMask;
        private readonly int Length;
        public bool FirtsBitWasOne;
        public uint Value;


        public Register(int len)
        {
            Length = len;
            bitMask = (uint) (Math.Pow(2, Length) - 1);
        }


        /// <summary>
        ///     Выдает самый левый бит регистра
        /// </summary>
        private uint firstBit
        {
            get
            {
                //Console.WriteLine($"вытестенный бит - {Convert.ToString(Value >> Length, 2)}");
                return Value >> (Length - 1);
            }
        }


        //private Register(Register r) // потом нормально переписать конструктор, так мне не нравится
        //{
        //    Length = r.Length;
        //    bitMask = r.bitMask;
        //    Value = r.Value;
        //}
        /// <summary>
        ///     Вставляет справа бит и убирает самый левый
        /// </summary>
        /// <param name="bit"> Вставляемый бит</param>
        public void InsertBit(uint bit)
        {
            if (bit > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(bit), "Значение bit должно быть равно 0 или 1");
            }

            if (firstBit == 1)
            {
                FirtsBitWasOne = true;
            }
            else
            {
                FirtsBitWasOne = false;
            }

            Value = ((Value << 1) & bitMask) | bit;
        }


        /// <summary>
        ///     Перегрузка оператора XOR
        /// </summary>
        /// <param name="reg"> Регистр </param>
        /// <param name="val"> Число</param>
        /// <returns></returns>
        public static Register operator ^(Register reg, uint val)
        {
            reg.Value = reg.Value ^ val;

            return reg;
        }
    }
}