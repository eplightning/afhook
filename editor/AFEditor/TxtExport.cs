using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AFEditor
{
    /**
     * TXT file export/import
     * 
     * Encoding: SHIFT-JIS
     */
    public class TxtExport
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp(byte[] b1, byte[] b2, long count);

        private static bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
        }

        public static int Import(CResourceManager resman, string filename, bool appendSoh = false)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);

            StreamReader reader = new StreamReader(fs, Encoding.GetEncoding("SHIFT-JIS"));

            bool english = false;
            StringBuilder engText = new StringBuilder();
            StringBuilder jpnText = new StringBuilder();
            int replaced = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                if (line.Length > 0 && line[0] == 'E')
                {
                    if (jpnText.Length == 0) continue;
                    english = true;
                    engText.AppendLine(line.Substring(1));
                }
                else
                {
                    if (english)
                    {
                        jpnText.Replace("\r\n", "\n");
                        jpnText.Remove(jpnText.Length - 1, 1);
                        jpnText.Append('\x00');
                        byte[] source = Encoding.GetEncoding("SHIFT-JIS").GetBytes(jpnText.ToString());

                        try
                        {
                            var value = resman.dialogue.First(k => TxtExport.ByteArrayCompare(source, k.Value.source));

                            value.Value.translation = engText.ToString();

                            if (value.Value.translation.Length >= 2)
                            {
                                string text = value.Value.translation.Substring(0, value.Value.translation.Length - 2);

                                if (appendSoh && text.Length > 0 && !text.EndsWith("{dc3}") && !text.EndsWith("{soh}") && !text.EndsWith("{dc3}\r\n") && !text.EndsWith("{soh}\r\n"))
                                {
                                    text = text + "{soh}";
                                }

                                value.Value.translation = text;
                            }

                            replaced++;
                        }
                        catch (InvalidOperationException)
                        {
                        }

                        engText = new StringBuilder();
                        jpnText = new StringBuilder();
                        english = false;
                    }

                    jpnText.AppendLine(line);
                }
            }

            fs.Close();

            return replaced;
        }

        public static void Export(CResourceManager resman, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);

            StreamWriter writer = new StreamWriter(fs, Encoding.GetEncoding("SHIFT-JIS"));

            foreach (KeyValuePair<UInt32, PackageText> kv in resman.dialogue)
            {
                if (kv.Value.sourceLen <= 2) continue;

                fs.Write(kv.Value.source, 0, ((int) kv.Value.sourceLen - 1));
                fs.Flush();
                writer.Write("\r\nE");
                writer.Write(kv.Value.translation.Replace("\r\n", "\n").Replace("\n", "\r\nE"));
                writer.Write("\r\n");
                writer.Flush();
                fs.Flush();
            }

            fs.Close();
        }
    }
}
