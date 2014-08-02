using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFEditor
{
    public class CBitReader
    {
        private byte[] bits;
        private int index;
        private int readbits;
        private int currentbyte;

        public CBitReader(byte[] stream)
        {
            bits = stream;
            index = 0;
            readbits = 8;
            currentbyte = 0;
        }

        public int ReadBit()
        {
            if (readbits == 8)
            {
                currentbyte = bits[index];
                index++;
                readbits = 0;
            }

            var bit = (currentbyte & 1);
            currentbyte = (currentbyte >> 1);

            readbits++;

            return (bit != 0 ? 1 : 0);
        }

        public Int32 ReadSigned()
        {
            if (ReadBit() == 0)
            {
                return 0;
            }

            int s = 1 - ReadBit() * 2;

            int t = 1;

            for (int i = 0; i < 6 && ReadBit() != 0; i++)
                t = (t << 1) | ReadBit();

            return s * t;
        }

        public UInt32 ReadUnsigned()
        {
            if (ReadBit() == 0)
            {
                return 0;
            }

            int t = 1;

            do
            {
                t = (t << 1) | ReadBit();
            }
            while (ReadBit() != 0);

            return (UInt32) t - 1;
        }
    }
}
