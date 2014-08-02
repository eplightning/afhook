using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AFEditor
{
    public class PackageText
    {
        public UInt32 sourceLen;
        public byte[] source;
        public string sourceString;
        public string translation;
    }

    public class PackageImage
    {
        public string hash;
        public string title;
        public Int16 width;
        public Int16 height;
        public Int32 flags;
        public Int32 depth;
        public UInt32 replacementLen;
        public byte[] replacement;

        public override string ToString()
        {
            return title;
        }
    }

    public class CResourceManager
    {
        public Dictionary<UInt32, PackageText> dialogue = new Dictionary<UInt32, PackageText>();
        public Dictionary<UInt32, PackageText> ui = new Dictionary<UInt32, PackageText>();
        public Dictionary<string, PackageImage> images = new Dictionary<string, PackageImage>();

        public string Dehumanize(string original)
        {
            StringBuilder build = new StringBuilder();

            for (int i = 0; i < original.Length; i++)
            {
                if (original[i] == '\r')
                {
                    continue;
                }

                if (original[i] == '{' && (i <= (original.Length - 5)))
                {
                    if (original[i + 1] == 's' && original[i + 2] == 'o' && original[i + 3] == 'h' && original[i + 4] == '}')
                    {
                        build.Append('\x01'); // SOH
                        i += 4;
                        continue;
                    }
                    else if (original[i + 1] == 'e' && original[i + 2] == 't' && original[i + 3] == 'x' && original[i + 4] == '}')
                    {
                        build.Append('\x03'); // ETX
                        i += 4;
                        continue;
                    }
                    else if (original[i + 1] == 'd' && original[i + 2] == 'c' && original[i + 3] == '2' && original[i + 4] == '}')
                    {
                        build.Append('\x12'); // DC2
                        i += 4;
                        continue;
                    }
                    else if (original[i + 1] == 'd' && original[i + 2] == 'c' && original[i + 3] == '3' && original[i + 4] == '}')
                    {
                        build.Append('\x13'); // DC3
                        i += 4;
                        continue;
                    }
                    else if (original[i + 1] == 's' && original[i + 2] == 't' && original[i + 3] == 'x' && original[i + 4] == '}')
                    {
                        build.Append('\x02'); // STX
                        i += 4;
                        continue;
                    }
                    else if (original[i + 1] == 'e' && original[i + 2] == 'n' && original[i + 3] == 'q' && original[i + 4] == '}')
                    {
                        build.Append('\x05'); // ENQ
                        i += 4;
                        continue;
                    }
                }

                build.Append(original[i]);
            }

            return build.ToString();
        }

        public string Humanize(string original)
        {
            StringBuilder build = new StringBuilder();

            foreach (char c in original)
            {
                if (c == '\x00')
                {
                    break;
                }
                else if (c == '\x01')
                {
                    build.Append("{soh}");
                }
                else if (c == '\x03')
                {
                    build.Append("{etx}");
                }
                else if (c == '\x12')
                {
                    build.Append("{dc2}");
                }
                else if (c == '\x13')
                {
                    build.Append("{dc3}");
                }
                else if (c == '\x02')
                {
                    build.Append("{stx}");
                }
                else if (c == '\x05')
                {
                    build.Append("{enq}");
                }
                else
                {
                    build.Append(c);
                }
            }

            return build.ToString();
        }

        public void SavePackage(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(fs);

            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);

            UInt32 textOffset = (UInt32) fs.Position;
            UInt32 textCount = 0;

            foreach (KeyValuePair<UInt32, PackageText> kv in dialogue)
            {
                if (kv.Value.sourceLen <= 1) continue;

                writer.Write(kv.Key);
                writer.Write(kv.Value.sourceLen);

                if (kv.Value.translation.Length <= 0)
                {
                    writer.Write((UInt32)0);
                    writer.Write(kv.Value.source);
                }
                else
                {
                    string dehumanized = Dehumanize(kv.Value.translation);

                    byte[] encoded = Encoding.GetEncoding("SHIFT-JIS").GetBytes(dehumanized);

                    UInt32 translatedLen = (UInt32) encoded.Length;
                    translatedLen++;

                    writer.Write(translatedLen);
                    writer.Write(kv.Value.source);
                    writer.Write(encoded);
                    writer.Write('\x00');
                }

                textCount++;
            }

            UInt32 uiOffset = (UInt32)fs.Position;
            UInt32 uiCount = 0;

            foreach (KeyValuePair<UInt32, PackageText> kv in ui)
            {
                if (kv.Value.sourceLen <= 1) continue;

                writer.Write(kv.Key);
                writer.Write(kv.Value.sourceLen);

                if (kv.Value.translation.Length <= 0)
                {
                    writer.Write((UInt32)0);
                    writer.Write(kv.Value.source);
                }
                else
                {
                    string dehumanized = Dehumanize(kv.Value.translation);

                    byte[] encoded = Encoding.GetEncoding("SHIFT-JIS").GetBytes(dehumanized);

                    UInt32 translatedLen = (UInt32)encoded.Length;
                    translatedLen++;

                    writer.Write(translatedLen);
                    writer.Write(kv.Value.source);
                    writer.Write(encoded);
                    writer.Write('\x00');
                }

                uiCount++;
            }

            UInt32 imageOffset = (UInt32)fs.Position;
            UInt32 imageCount = 0;

            foreach (KeyValuePair<string, PackageImage> kv in images)
            {
                writer.Write(Encoding.GetEncoding("ASCII").GetBytes(kv.Value.hash));

                if (kv.Value.title.Length <= 0)
                {
                    kv.Value.title = kv.Value.hash;
                }

                writer.Write((UInt32) (kv.Value.title.Length + 1));
                writer.Write(Encoding.GetEncoding("ASCII").GetBytes(kv.Value.title));
                writer.Write('\x00');

                writer.Write(kv.Value.width);
                writer.Write(kv.Value.height);
                writer.Write(kv.Value.flags);
                writer.Write(kv.Value.depth);
                writer.Write(kv.Value.replacementLen);

                if (kv.Value.replacementLen > 0)
                {
                    writer.Write(kv.Value.replacement);
                }

                imageCount++;
            }

            fs.Seek(0, SeekOrigin.Begin);

            writer.Write((UInt32)0x8D120AB6);
            writer.Write(textOffset);
            writer.Write(textCount);
            writer.Write((UInt32)0);
            writer.Write((UInt32)0);
            writer.Write(uiOffset);
            writer.Write(uiCount);
            writer.Write(imageOffset);
            writer.Write(imageCount);

            fs.Close();
        }

        public CResourceManager(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);

            BinaryReader reader = new BinaryReader(fs);

            UInt32 magic = reader.ReadUInt32();
            UInt32 textOffset = reader.ReadUInt32();
            UInt32 textCount = reader.ReadUInt32();
            UInt32 charactersOffset = reader.ReadUInt32();
            UInt32 charactersCount = reader.ReadUInt32();
            UInt32 uiOffset = reader.ReadUInt32();
            UInt32 uiCount = reader.ReadUInt32();
            UInt32 imageOffset = reader.ReadUInt32();
            UInt32 imageCount = reader.ReadUInt32();

            if (magic != 0x8D120AB6)
            {
                return;
            }

            fs.Seek(textOffset, SeekOrigin.Begin);

            for (uint i = 0; i < textCount; i++)
            {
                UInt32 id;
                UInt32 sourceLen;
                UInt32 translatedLen;

                id = reader.ReadUInt32();
                sourceLen = reader.ReadUInt32();
                translatedLen = reader.ReadUInt32();

                if (sourceLen <= 1) continue;

                PackageText pkg = new PackageText();

                pkg.source = reader.ReadBytes((int) sourceLen);
                pkg.sourceLen = sourceLen;
                pkg.sourceString = Encoding.GetEncoding("SHIFT-JIS").GetString(pkg.source);
                pkg.sourceString = Humanize(pkg.sourceString);

                if (translatedLen > 1)
                {
                    pkg.translation = Encoding.GetEncoding("SHIFT-JIS").GetString(reader.ReadBytes((int) translatedLen - 1));
                    pkg.translation = Humanize(pkg.translation);
                    reader.ReadByte();
                }
                else
                {
                    pkg.translation = "";
                }

                dialogue.Add(id, pkg);
            }

            fs.Seek(uiOffset, SeekOrigin.Begin);

            for (uint i = 0; i < uiCount; i++)
            {
                UInt32 id;
                UInt32 sourceLen;
                UInt32 translatedLen;

                id = reader.ReadUInt32();
                sourceLen = reader.ReadUInt32();
                translatedLen = reader.ReadUInt32();

                if (sourceLen <= 1) continue;

                PackageText pkg = new PackageText();

                pkg.source = reader.ReadBytes((int)sourceLen);
                pkg.sourceLen = sourceLen;
                pkg.sourceString = Encoding.GetEncoding("SHIFT-JIS").GetString(pkg.source);

                if (translatedLen > 1)
                {
                    pkg.translation = Encoding.GetEncoding("SHIFT-JIS").GetString(reader.ReadBytes((int)translatedLen - 1));
                    reader.ReadByte();
                }
                else
                {
                    pkg.translation = "";
                }

                ui.Add(id, pkg);
            }

            fs.Seek(imageOffset, SeekOrigin.Begin);

            for (uint i = 0; i < imageCount; i++)
            {
                PackageImage img = new PackageImage();
                UInt32 titleLen;

                img.hash = Encoding.GetEncoding("ASCII").GetString(reader.ReadBytes(40));

                titleLen = reader.ReadUInt32();

                img.title = Encoding.GetEncoding("ASCII").GetString(reader.ReadBytes((int)titleLen - 1));
                reader.ReadByte();
                img.width = reader.ReadInt16();
                img.height = reader.ReadInt16();
                img.flags = reader.ReadInt32();
                img.depth = reader.ReadInt32();

                img.replacementLen = reader.ReadUInt32();

                if (img.replacementLen > 0)
                {
                    img.replacement = reader.ReadBytes((int) img.replacementLen);
                }

                images.Add(img.hash, img);
            }

            fs.Close();
        }
    }
}
