#include	"CImageWriter.h"
#ifdef AFHOOK_DEVMODE

/**
 * http://code.google.com/p/regomne-chinesize/source/browse/trunk/Rugp/alterdec/src/stream.cpp
 */

/*
 *	CStream
 */
CStream::CStream()
{
	delta = 0;
	temp = 0;
	readclasslist = false;
	redbyte = 0;
}



/*
 *	readbyte/readword/readdword/readqword
 */
byte CStream::readbyte()
{
	byte b;
	read( &b, 1 );
	return b;
}

word CStream::readword()
{
	word w;
	read( &w, 2 );
	return w;
}

dword CStream::readdword()
{
	dword d;
	read( &d, 4 );
	return d;
}

qword CStream::readqword()
{
	qword q;
	read( &q, 8 );
	return q;
}



/*
 *	readbit
 */
int CStream::readbit()
{
	if ( delta == 0 )
		temp = readbyte();

	int bit = temp & 1;

	temp >>= 1;
	delta = ( delta + 1 ) % 8;
	
	return bit;
};



/*
 *	readunsigned
 */
int CStream::readunsigned()
{
	if ( readbit() == 0 )
		return 0;

	int t = 1;

	do
	{
		t = ( t << 1 ) | readbit();
	}
	while ( readbit() != 0 );

	return t - 1;
}



/*
 *	readsigned
 */
int CStream::readsigned()
{
	if ( readbit() == 0 )
		return 0;

	int s = 1 - readbit() * 2;

	int t = 1;

	for ( int i=0; i<6 && readbit()!=0; i++ )
		t = ( t << 1 ) | readbit();
	
	return s * t;
}



/*
 *	getreadbyte
 */
int CStream::getreadbyte()
{
	return redbyte;
}

/*
 *	CMemoryStream
 */
CMemoryStream::CMemoryStream( void *buf )
{
	b = (byte *)buf;
}

/*
 *	read
 */
void CMemoryStream::read( void *buf, int len )
{
	memcpy( buf, b, len );
	b += len;
	redbyte += len;
}

/*
 *	seek
 */
void CMemoryStream::seek( int n )
{
	b += n;
	redbyte += n;
};

/*
 *	CImageWriter
 */
CImageWriter::CImageWriter( int w, int h )
{
	width = w;
	height = h;
	buf = new byte[w*h*4];
}



/*
 *	set
 */
void CImageWriter::set( int x, int y, dword color )
{
	*(dword *)&buf[4*(y*width+x)] = color;
}

void CImageWriter::set( int x, int y, byte r, byte g, byte b, byte a )
{
	buf[4*(y*width+x)+0] = b;
	buf[4*(y*width+x)+1] = g;
	buf[4*(y*width+x)+2] = r;
	buf[4*(y*width+x)+3] = a;
}



/*
 *	write
 *		offsetはファイル名に使用
 */
void CImageWriter::write( FILE* f )
{
	png_structp	png_ptr = png_create_write_struct( PNG_LIBPNG_VER_STRING, NULL, NULL, NULL );
	png_infop info_ptr = png_create_info_struct( png_ptr );
	png_init_io( png_ptr, f );
	png_set_IHDR( png_ptr, info_ptr, width, height, 8, PNG_COLOR_TYPE_RGB_ALPHA,
							PNG_INTERLACE_NONE, PNG_COMPRESSION_TYPE_BASE, PNG_FILTER_TYPE_BASE );
	png_write_info( png_ptr, info_ptr );

	png_bytep *line = new png_bytep[height];
	for ( int i=0; i<height; i++ )
		line[i] = buf + 4 * width * i;
	png_write_image( png_ptr, line );
	delete[] line;

	png_write_end( png_ptr, info_ptr );
	png_destroy_write_struct( &png_ptr, &info_ptr );

	fclose( f );
}



/* 
 *	~CImageWriter
 */
CImageWriter::~CImageWriter()
{
	delete[] buf;
}

void CImageWriter::Opaque1( CStream *s, CImageWriter *image, int width, int height, dword depth )
{
	static int prbuf[2000];
	static int pgbuf[2000];
	static int pbbuf[2000];
	
	int *	pr = prbuf;
	int *	pg = pgbuf;
	int *	pb = pbbuf;

	if ( width > 2000 )
	{
		pr = new int[width];
		pg = new int[width];
		pb = new int[width];
	}
	
	for ( int y=0; y<height; y++ )
	{
		int		count = -1;
		int 	r, g, b;
		int		dr, dg, db;

		if (y == 209)
		{
			y++;
			y--;
		}

		r = g = b = 0;
		dr = dg = db = 0;

		for ( int x=0; x<width; )
		{
			if ( count == -1 )
				count = s->readunsigned();

			if ( s->readbit() == 1 )
			{
				r=pr[x], g=pg[x], b=pb[x];
				dr = dg = db = 0;
			}
			else
			{
				int t = g + dg*2;
				if ( t > 255 )  t = 255;
				if ( t < 0 )  t = 0;
				t -= g;

				dg = s->readsigned();

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

				r += s->readsigned() * 2;
				if ( r > 255 )  r = 255;
				if ( r < 0 )  r = 0;

				b += s->readsigned() * 2;
				if ( b > 255 )  b = 255;
				if ( b < 0 )  b = 0;
			}

			pr[x]=r, pg[x]=g, pb[x]=b;
			image->set( x++, y, r, g, b );

			if ( count-- == 0  &&  x < width )
			{
				int n = s->readunsigned() + 1;

				for ( int i=0; i<n; i++ )
				{
					pr[x]=r, pg[x]=g, pb[x]=b;
					image->set( x++, y, r, g, b );
				}

				dr = dg = db = 0;
			}
		}
	}

	if ( pr != prbuf )  delete[] pr;
	if ( pg != pgbuf )  delete[] pg;
	if ( pb != pbbuf )  delete[] pb;
}



void CImageWriter::Opaque2( CStream *s, CImageWriter *image, int width, int height, dword depth )
{
	static int prbuf[2000];
	static int pgbuf[2000];
	static int pbbuf[2000];
	
	int *	pr = prbuf;
	int *	pg = pgbuf;
	int *	pb = pbbuf;

	if ( width > 2000 )
	{
		pr = new int[width];
		pg = new int[width];
		pb = new int[width];
	}

	int rdepth = depth >> 8 & 0xff;
	int gdepth = depth >> 16 & 0xff;
	int bdepth = depth >> 24 & 0xff;
	
	int 	r, g, b;
	for ( int y=0; y<height; y++ )
	{
		int		count = -1;

		r = g = b = 0;

		for ( int x=0; x<width; )
		{
			if ( count < 0 )
				count = s->readunsigned();

			if ( s->readbit() == 1 )
			{
				r = pr[x];
				g = pg[x];
				b = pb[x];
			}
			else
			{
				int d = s->readsigned() << (8-gdepth);
				r += d;
				g += d;
				b += d;
				if ( r > 255 )  r = 255;
				if ( g > 255 )  g = 255;
				if ( b > 255 )  b = 255;
				if ( r < 0 )  r = 0;
				if ( g < 0 )  g = 0;
				if ( b < 0 )  b = 0;

				r += s->readsigned() << (8-rdepth);
				if ( r > 255 )  r = 255;
				if ( r < 0 )  r = 0;

				b += s->readsigned() << (8-bdepth);
				if ( b > 255 )  b = 255;
				if ( b < 0 )  b = 0;

				r &= 0x100 - (1<<(8-rdepth));
				g &= 0x100 - (1<<(8-gdepth));
				b &= 0x100 - (1<<(8-bdepth));

				pr[x] = r;
				pg[x] = g;
				pb[x] = b;
			}

			image->set( x++, y, r, g, b );

			if ( --count < 0  &&  x < width )
			{
				int n = s->readunsigned() + 1;

				for ( int i=0; i<n; i++ )
				{
					pr[x] = r;
					pg[x] = g;
					pb[x] = b;
					image->set( x++, y, r, g, b );
				}
			}
		}
	}

	if ( pr != prbuf )  delete[] pr;
	if ( pg != pgbuf )  delete[] pg;
	if ( pb != pbbuf )  delete[] pb;
}



void CImageWriter::Transparent( CStream *s, CImageWriter *image, int width, int height, dword depth )
{
	int *	pr = new int[width];
	int *	pg = new int[width];
	int *	pb = new int[width];
	int *	pa = new int[width];

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
				ta += s->readsigned();

				if ( ta == 0 )
				{
					int n = s->readunsigned();
					for ( int i=0; i<n+1; i++ )
						pr[x]=0, pg[x]=0, pb[x]=0, pa[x]=0,
						image->set( x++, y, 0, 0, 0, 0 );
					continue;
				}

				if ( ta == 32 )
					samea = s->readunsigned();

				a = ta * 8;
				if ( a >= 256 )
					a = 255;
			}

			if ( --samef < 0 )
			{
				f = f ? false : true;

				samef = s->readunsigned();

				dg = 0;
			}
			
			if ( f )
			{
				if ( samea < samef )
				{
					pr[x]=r, pg[x]=g, pb[x]=b, pa[x]=a;
					image->set( x++, y, r, g, b, a );
				}
				else
				{
					if ( samef > 0 )
						samea -= samef;

					for ( int i=0; i<samef+1; i++ )
						pr[x]=r, pg[x]=g, pb[x]=b, pa[x]=a,
						image->set( x++, y, r, g, b, a );
					samef = 0;
				}
			}
			else
			{
				if ( s->readbit() == 1 )
				{
					r=pr[x], g=pg[x], b=pb[x];
					pa[x] = a;
					image->set( x++, y, r, g, b, a );
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

					dg = tg/2 + s->readsigned();
					if ( dg < -128 )  dg = -128;
					if ( dg > 127 )  dg = 127;

					r += dg*2;
					g += dg*2;
					b += dg*2;

					r += s->readsigned() * 2;
					b += s->readsigned() * 2;

					if ( r > 255 )  r = 255;
					if ( g > 255 )  g = 255;
					if ( b > 255 )  b = 255;
					if ( r < 0 )  r = 0;
					if ( g < 0 )  g = 0;
					if ( b < 0 )  b = 0;
					
					r &= 0xfe;
					g &= 0xfe;
					b &= 0xfe;

					pr[x]=r, pg[x]=g, pb[x]=b, pa[x]=a;
					image->set( x++, y, r, g, b, a );
				}
			}
		}
	}

	delete[] pr;
	delete[] pg;
	delete[] pb;
	delete[] pa;
}
#endif