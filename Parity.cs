
using System;
using System.Collections.Generic;

namespace _5Sem_Seti_Lab3
{
    internal class Parity
    {
        public static byte ParityHor(byte num)
        {
            while (num > 1)
            {
                var lastBit = num ^ ((num >> 1) << 1);
                num = Convert.ToByte((num >> 1) ^ lastBit);
            }

            return num;
        }


        public static byte ParityVert(List<byte> bytes)
        {
            var checkSum = Convert.ToByte(bytes[0] ^ bytes[1]);
            for (var i = 2; i < bytes.Count; i++)
            {
                checkSum = Convert.ToByte(checkSum ^ bytes[i]);
            }

            return checkSum;
        }
    }
}