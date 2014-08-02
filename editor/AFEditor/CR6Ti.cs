using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AFEditor
{
    /**
     * rUGP stores CG and other non-sprite stuff in R6Ti format (don't google, you won't find shit - it's custom format)
     * 
     * Sprites are stored in Rip00x format but yeah. We don't need those.
     * 
     * This class provides static methods that encodes image provided in Bitmap so it can be used as a replacement by AFHook
     */
    public class CR6Ti
    {
        /**
         * 1 opaque version
         */
        static public void WriteOpaque1(Bitmap bitmap, CBitWriter writer, Int16 width, Int16 height)
        {
            Color[] widthBuffer = new Color[width];

            // Every row
            for (int y = 0; y < height; y++)
            {
                // Clear stuff
                int x = 0;
                int repetition = -1;
                UInt16 n = 0;
                Color color = Color.FromArgb(0, 0, 0);
                int dr = 0;
                int dg = 0;
                int db = 0;

                // Every column
                while (x < width)
                {
                    // Find next duplicated pixels in this row (n is count of the pixels, repetition is relative location)
                    if (repetition == -1)
                    {
                        repetition = FindNextRepetition(bitmap, x, y, width, ref n);

                        writer.Write((UInt16)repetition);
                    }

                    // If this pixel is the same as the one in previous row - copy its RGBs
                    if (y > 0 && widthBuffer[x] == bitmap.GetPixel(x, y))
                    {
                        writer.Write(true);
                        dr = dg = db = 0;
                        color = widthBuffer[x];
                    }
                    // Otherwise write color difference and transform our current color
                    else
                    {
                        writer.Write(false);
                        WriteColorDiff(writer, ref color, bitmap.GetPixel(x, y), ref dr, ref dg, ref db);
                    }

                    widthBuffer[x++] = color;

                    // Process repetition
                    if (repetition-- == 0 && x < width)
                    {
                        if (n == 0)
                        {
                            writer.Write((byte)0);

                            widthBuffer[x++] = color;
                        }
                        else
                        {
                            writer.Write((UInt16)(n - 1));

                            for (int i = 0; i < n; i++)
                            {
                                widthBuffer[x++] = color;
                            }
                        }

                        dr = dg = db = 0;
                    }
                }
            }
        }

        /**
         * Transparent images
         */
        static public void WriteTransparent(Bitmap bitmap, CBitWriter writer, Int16 width, Int16 height)
        {
            Color[] widthBuffer = new Color[width];

            // Every row
            for (int y = 0; y < height; y++)
            {
                int x = 0;
                Color color = Color.FromArgb(0, 0, 0, 0);
                int alphaRepetition = 0;
                int ta = 0;
                int a = 0;
                int tempa = 0;
                int n = 0;
                int dg = 0;
                int frameRepetition = 0;
                bool repetition = true;
                UInt16 count = 0;

                while (x < width)
                {
                    // ALPHA CHANNEL STUFF
                    if (--alphaRepetition < 0)
                    {
                        ta = (int) Math.Round(bitmap.GetPixel(x, y).A * 0.125, 0);
                        ta -= (int)Math.Round(color.A * 0.125, 0);
                        a = FixAlpha(bitmap.GetPixel(x, y).A);
                        color = Color.FromArgb(a, color.R, color.G, color.B);

                        writer.Write((sbyte)ta);

                        if (a == 0)
                        {
                            n = 0;
                            tempa = 0;

                            widthBuffer[x++] = Color.FromArgb(0, 0, 0, 0); 

                            while (x < width)
                            {
                                tempa = FixAlpha(bitmap.GetPixel(x, y).A);

                                if (tempa != 0)
                                {
                                    break;
                                }

                                n++;
                                widthBuffer[x++] = Color.FromArgb(0, 0, 0, 0); 
                            }

                            writer.Write((UInt16)n);

                            continue;
                        }

                        if (a == 255)
                        {
                            n = 0;
                            tempa = 0;
                            int x2 = x + 1;

                            while (x2 < width)
                            {
                                tempa = FixAlpha(bitmap.GetPixel(x2, y).A);

                                if (tempa != 255)
                                {
                                    break;
                                }

                                n++;
                                x2++;
                            }

                            writer.Write((UInt16)n);
                            alphaRepetition = n;
                        }
                    }

                    // FRAME REPETITION
                    if (--frameRepetition < 0)
                    {
                        repetition = !repetition;
                        dg = 0;

                        if (repetition)
                        {
                            writer.Write((UInt16)(count - 1));
                            frameRepetition = count - 1;
                        }
                        else
                        {
                            frameRepetition = FindNextRepetition2(bitmap, x, y, width, ref count);
                            writer.Write((UInt16)frameRepetition);
                        }
                    }

                    // MAIN PART
                    if (repetition)
                    {
                        if (alphaRepetition < frameRepetition)
                        {
                            widthBuffer[x++] = color;
                        }
                        // All repeated frames have the same alpha
                        else
                        {
                            if (frameRepetition > 0)
                            {
                                alphaRepetition -= frameRepetition;
                            }

                            for (int i = 0; i < (frameRepetition + 1); i++)
                            {
                                widthBuffer[x++] = color;
                            }

                            frameRepetition = 0;
                        }
                    }
                    else
                    {
                        // If this pixel is the same as the one in previous row - copy its RGBs
                        if (y > 0 && widthBuffer[x].R == bitmap.GetPixel(x, y).R && widthBuffer[x].G == bitmap.GetPixel(x, y).G && widthBuffer[x].B == bitmap.GetPixel(x, y).B)
                        {
                            writer.Write(true);
                            dg = 0;
                            widthBuffer[x] = Color.FromArgb(color.A, widthBuffer[x].R, widthBuffer[x].G, widthBuffer[x].B);
                            color = widthBuffer[x++];
                        }
                        // Otherwise write color difference and transform our current color
                        else
                        {
                            writer.Write(false);
                            WriteColorDiff2(writer, ref color, bitmap.GetPixel(x, y), ref dg, color.A);
                            widthBuffer[x++] = color;
                        }
                    }
                }
            }
        }

        static private int FixAlpha(int alpha)
        {
            alpha = (int) Math.Round(alpha * 0.125, 0);
            alpha *= 8;

            if (alpha >= 256) alpha = 255;

            return alpha;
        }

        static private void WriteColorDiff(CBitWriter writer, ref Color color, Color next, ref int dr, ref int dg, ref int db)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;
            int t = g + (dg * 2);

            if (t > 255)
            {
                t = 255;
            }
            else if (t < 0)
            {
                t = 0;
            }

            t -= g;

            dg = (int) ((next.G - color.G) / 2);

            if (dg < -128) dg = -128;
            if (dg > 127) dg = 127;

            dr = db = dg;
            r += (dr * 2);
            g += (dg * 2);
            b += (db * 2);

            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;
            if (r < 0) r = 0;
            if (g < 0) g = 0;
            if (b < 0) b = 0;

            int dr2 = (next.R - r) / 2;
            int db2 = (next.B - b) / 2;

            if (dr2 < -128) dr2 = -128;
            if (dr2 > 127) dr2 = 127;
            if (db2 < -128) db2 = -128;
            if (db2 > 127) db2 = 127;

            r += (dr2 * 2);
            b += (db2 * 2);

            if (r > 255) r = 255;
            if (b > 255) b = 255;
            if (r < 0) r = 0;
            if (b < 0) b = 0;

            writer.Write((sbyte)(dg - (t/2)));
            writer.Write((sbyte)db2);
            writer.Write((sbyte)dr2);

            color = Color.FromArgb(r, g, b);
        }

        static private void WriteColorDiff2(CBitWriter writer, ref Color color, Color next, ref int dg, int alpha)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;
            int t = g + (dg * 2);

            if (t > 255)
            {
                t = 255;
            }
            else if (t < 0)
            {
                t = 0;
            }

            t -= g;

            if (t < -128) t += 256;
            if (t > 127) t -= 256;

            dg = (int)((next.G - color.G) / 2);

            if (dg < -128) dg = -128;
            if (dg > 127) dg = 127;

            r += (dg * 2);
            g += (dg * 2);
            b += (dg * 2);

            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;
            if (r < 0) r = 0;
            if (g < 0) g = 0;
            if (b < 0) b = 0;

            int dr2 = (next.R - r) / 2;
            int db2 = (next.B - b) / 2;

            if (dr2 < -128) dr2 = -128;
            if (dr2 > 127) dr2 = 127;
            if (db2 < -128) db2 = -128;
            if (db2 > 127) db2 = 127;

            r += (dr2 * 2);
            b += (db2 * 2);

            if (r > 255) r = 255;
            if (b > 255) b = 255;
            if (r < 0) r = 0;
            if (b < 0) b = 0;

            writer.Write((sbyte)(dg - (t / 2)));
            writer.Write((sbyte)db2);
            writer.Write((sbyte)dr2);

            color = Color.FromArgb(alpha, r & 0xFE, g & 0xFE, b & 0xFE);
        }

        static private int FindNextRepetition(Bitmap bitmap, int x, int y, Int16 width, ref UInt16 count)
        {
            Color previous;
            Color current;
            int ourX = x;

            if ((x + 1) == width)
            {
                count = 0;
                return 0;
            }

            do
            {
                previous = bitmap.GetPixel(ourX, y);
                current = bitmap.GetPixel(++ourX, y);
            }
            while (ourX < (width - 1) && (ourX - x) < 65535 && previous != current);

            if (previous != current)
            {
                count = 0;
                return (width - x - 1);
            }
            else
            {
                count = 1;
                int ourX2 = ourX;

                while ((ourX2 + 1) < width && count <= 65535 && bitmap.GetPixel(ourX, y) == bitmap.GetPixel(++ourX2, y))
                {
                    count++;
                }
            }

            return (UInt16) (ourX - x - 1);
        }

        /**
         * Slower but alpha-free version of the above function
         */
        static private int FindNextRepetition2(Bitmap bitmap, int x, int y, Int16 width, ref UInt16 count)
        {
            Color previous;
            Color current;
            int ourX = x;

            if ((x + 1) == width)
            {
                count = 0;
                return 0;
            }

            do
            {
                previous = bitmap.GetPixel(ourX, y);
                current = bitmap.GetPixel(++ourX, y);
                previous = Color.FromArgb(0, previous.R, previous.G, previous.B);
                current = Color.FromArgb(0, current.R, current.G, current.B);
            }
            while (ourX < (width - 1) && (ourX - x) < 65535 && previous != current);

            if (previous != current)
            {
                count = 0;
                return (width - x - 1);
            }
            else
            {
                count = 1;
                int ourX2 = ourX;

                while ((ourX2 + 1) < width && count <= 65535)
                {
                    previous = bitmap.GetPixel(ourX, y);
                    current = bitmap.GetPixel(++ourX2, y);
                    previous = Color.FromArgb(0, previous.R, previous.G, previous.B);
                    current = Color.FromArgb(0, current.R, current.G, current.B);

                    if (previous == current)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return (UInt16)(ourX - x - 1);
        }

        // Now this is basically 99% copied from alterdec

        static public void DecodeOpaque1( CBitReader s, Bitmap image, int width, int height, int depth)
        {
            int[] pr = new int[width];
            int[] pg = new int[width];
            int[] pb = new int[width];
	
	        for ( int y=0; y<height; y++ )
	        {
		        int		count = -1;
		        int 	r, g, b;
		        int		dr, dg, db;

		        r = g = b = 0;
		        dr = dg = db = 0;

		        for ( int x=0; x<width; )
		        {
			        if ( count == -1 )
				        count = (int) s.ReadUnsigned();

			        if ( s.ReadBit() == 1 )
			        {
				        r=pr[x]; g=pg[x]; b=pb[x];
				        dr = dg = db = 0;
			        }
			        else
			        {
				        int t = g + dg*2;
				        if ( t > 255 )  t = 255;
				        if ( t < 0 )  t = 0;
				        t -= g;

				        dg = s.ReadSigned();

				        dg += t/2;
				        if ( dg < -128 )  dg = -128;
				        if ( dg > 127 )  dg = 127;

				        db = dr = dg;
				        r += dr * 2;
				        g += dg * 2;
				        b += db * 2;
				
				        if ( r > 255 )  r = 255;
				        if ( g > 255 )  g = 255;
				        if ( b > 255 )  b = 255;
				        if ( r < 0 )  r = 0;
				        if ( g < 0 )  g = 0;
				        if ( b < 0 )  b = 0;

				        r += s.ReadSigned() * 2;
				        if ( r > 255 )  r = 255;
				        if ( r < 0 )  r = 0;

				        b += s.ReadSigned() * 2;
				        if ( b > 255 )  b = 255;
				        if ( b < 0 )  b = 0;
			        }

			        pr[x]=r; pg[x]=g; pb[x]=b;
                    image.SetPixel(x++, y, Color.FromArgb(b, g, r));

			        if ( count-- == 0  &&  x < width )
			        {
				        int n = (int) s.ReadUnsigned() + 1;

				        for ( int i=0; i<n; i++ )
				        {
					        pr[x]=r; pg[x]=g; pb[x]=b;
					        image.SetPixel(x++, y, Color.FromArgb(b, g, r));
				        }

				        dr = dg = db = 0;
			        }
		        }
	        }
        }

        static public void DecodeTransparent( CBitReader s, Bitmap image, int width, int height, int depth)
        {
            int[] pr = new int[width];
            int[] pg = new int[width];
            int[] pb = new int[width];
            int[] pa = new int[width];

	        for ( int y=0; y<height; y++ )
	        {
		        int		r = 0;
		        int		g = 0;
		        int		b = 0;
		        int		a = 0;
		        int		dg = 0;
		        int		ta = 0;
		        bool	f = true;
		        int		samea = 0;
		        int		samef = 0;

		        for ( int x=0; x<width; )
		        {
			        if ( --samea < 0 )
			        {
				        ta += s.ReadSigned();

				        if ( ta == 0 )
				        {
					        int n = (int) s.ReadUnsigned();
                            for (int i = 0; i < n + 1; i++)
                            {
                                pr[x] = 0; pg[x] = 0; pb[x] = 0; pa[x] = 0;
                                image.SetPixel(x++, y, Color.FromArgb(0, 0, 0, 0));
                            }
					        continue;
				        }

				        if ( ta == 32 )
					        samea = (int) s.ReadUnsigned();

				        a = ta * 8;
				        if ( a >= 256 )
					        a = 255;
			        }

			        if ( --samef < 0 )
			        {
				        f = f ? false : true;

				        samef = (int) s.ReadUnsigned();

				        dg = 0;
			        }
			
			        if ( f )
			        {
				        if ( samea < samef )
				        {
					        pr[x]=r; pg[x]=g; pb[x]=b; pa[x]=a;
                            image.SetPixel(x++, y, Color.FromArgb(a, b, g, r));
				        }
				        else
				        {
					        if ( samef > 0 )
						        samea -= samef;

                            for (int i = 0; i < samef + 1; i++)
                            {
                                pr[x] = r; pg[x] = g; pb[x] = b; pa[x] = a;
                                image.SetPixel(x++, y, Color.FromArgb(a, b, g, r));
                            }
					        samef = 0;
				        }
			        }
			        else
			        {
				        if ( s.ReadBit() == 1 )
				        {
					        r=pr[x]; g=pg[x]; b=pb[x];
					        pa[x] = a;
                            image.SetPixel(x++, y, Color.FromArgb(a, b, g, r));
					        dg = 0;
				        }
				        else
				        {
					        int tg = g + dg*2;
					        if ( tg < 0 )  tg = 0;
					        if ( tg > 255 )  tg = 255;
					        tg -= g;
					        if ( tg < -128 )  tg += 256;
					        if ( tg > 127 )  tg -= 256;

					        dg = tg/2 + s.ReadSigned();
					        if ( dg < -128 )  dg = -128;
					        if ( dg > 127 )  dg = 127;

					        r += dg*2;
					        g += dg*2;
					        b += dg*2;

					        r += s.ReadSigned() * 2;
					        b += s.ReadSigned() * 2;

					        if ( r > 255 )  r = 255;
					        if ( g > 255 )  g = 255;
					        if ( b > 255 )  b = 255;
					        if ( r < 0 )  r = 0;
					        if ( g < 0 )  g = 0;
					        if ( b < 0 )  b = 0;
					
					        r &= 0xfe;
					        g &= 0xfe;
					        b &= 0xfe;

					        pr[x]=r; pg[x]=g; pb[x]=b; pa[x]=a;
                            image.SetPixel(x++, y, Color.FromArgb(a, b, g, r));
				        }
			        }
		        }
	        }
        }
    }
}
