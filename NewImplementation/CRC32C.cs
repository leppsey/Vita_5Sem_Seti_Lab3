using System.IO;


namespace _5Sem_Seti_Lab3.NewImplementation
{
    public class CRC32C
    {
        public CRC32C(CRCParameters p)
        {
            _parameters = p;
            GenerateTable();
        }
        private CRCParameters _parameters;
        private uint[] Crc32Table = new uint[256];
        
        // https://github.com/dariogriffo/Crc32/blob/master/Net452/Crc32Algorithm.cs lines 148-163
        private void GenerateTable()
        {
            var poly = _parameters.IsInputReflection ? reflect(_parameters.Polynom, 32) : _parameters.Polynom;
            for (uint i = 0; i < 256; i++)
            {
                uint entry = i;
                
                for (var j = 0; j < 8; j++)
                    entry = ((entry & 1) == 1) ? (entry >> 1) ^ poly : (entry >> 1);
                
                Crc32Table[i] = entry;
            }
        }
        private static uint reflect(uint crc, int bitnum)
        {

            // reflects the lower 'bitnum' bits of 'crc'

            uint i, j = 1, crcout = 0;

            for (i = (uint)1 << (bitnum - 1); i > 0; i >>= 1)
            {
                if ((crc & i) > 0)
                    crcout |= j;
                j <<= 1;
            }
            return (crcout);
        }

        //https://github.com/sahlberg/libiscsi/blob/master/lib/crc32c.c lines 112-119
        public uint CalculateHash(FileStream fs)
        {
            var hash = _parameters.RegisterInitial;
            
            for (uint i = 0; i < fs.Length; i++)
            {
                var buffer = fs.ReadByte();
                hash = Crc32Table[(hash ^ buffer) & 0xFF] ^ (hash >> 8);
            }

            //hash = _parameters.IsOutputReflection ? reflect(hash, 32) : hash;
            return hash^_parameters.OutputSum;
        }
    }
}
