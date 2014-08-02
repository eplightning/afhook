using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AFEditor
{
    /**
     * Used by R6Ti writer
     * 
     * Horrible code but it works!
     */
    public class CBitWriter
    {
        private List<bool> bits = new List<bool>();

        public void Write(bool bit)
        {
            bits.Add(bit);
        }

        public void Write(UInt16 bytes)
        {
            if (bytes == 0)
            {
                bits.Add(false);
                return;
            }

            if (bytes == UInt16.MaxValue)
            {
                throw new ArgumentOutOfRangeException("bytes", "too big");
            }

            bytes++;
            bool started = false;

            for (int i = 15; i >= 0; i--)
            {
                if ((bytes & (1 << i)) != 0 && !started)
                {
                    started = true;
                    bits.Add(true);
                }
                else if (started)
                {
                    bits.Add((bytes & (1 << i)) != 0 ? true : false);
                    if (i != 0) bits.Add(true);
                }
            }

            bits.Add(false);
        }

        public void Write(sbyte bytes)
        {
            if (bytes == 0)
            {
                bits.Add(false);
                return;
            }

            bool started = false;
            int startedi = 0;
            byte unsigned = 0;

            if (bytes > 0)
            {
                unsigned = (byte)bytes;
            }
            else
            {
                unsigned = (byte)-bytes;
            }

            for (int i = 6; i >= 0; i--)
            {
                if ((unsigned & (1 << i)) != 0 && !started)
                {
                    started = true;
                    startedi = i;
                    bits.Add(true);

                    if (bytes < 0) bits.Add(true);
                    else bits.Add(false);
                }
                else if (started)
                {
                    bits.Add(true);
                    bits.Add((unsigned & (1 << i)) != 0 ? true : false);
                }
            }

            if (startedi != 6) bits.Add(false);
        }

        public void Write(byte bytes)
        {
            if (bytes == 0)
            {
                bits.Add(false);
                return;
            }

            if (bytes == 255)
            {
                // 10101010101010100
                bits.Add(true); bits.Add(false); bits.Add(true); bits.Add(false);
                bits.Add(true); bits.Add(false); bits.Add(true); bits.Add(false);
                bits.Add(true); bits.Add(false); bits.Add(true); bits.Add(false);
                bits.Add(false);
                return;
            }

            bytes++;
            bool started = false;

            for (int i = 7; i >= 0; i--)
            {
                if ((bytes & (1 << i)) != 0 && !started)
                {
                    started = true;
                    bits.Add(true);
                }
                else if (started)
                {
                    bits.Add((bytes & (1 << i)) != 0 ? true : false);
                    if (i != 0) bits.Add(true);
                }
            }

            bits.Add(false);
        }

        public void CopyToStream(Stream stream)
        {
            byte currentByte = 0;
            int space = 0;

            foreach (bool flag in bits)
            {
                if (space > 7)
                {
                    stream.WriteByte(currentByte);
                    currentByte = 0;
                    space = 0;
                }

                if (flag)
                {
                    currentByte |= (byte)(1 << space);
                }

                space++;
            }

            stream.WriteByte(currentByte);
        }
    }
}
