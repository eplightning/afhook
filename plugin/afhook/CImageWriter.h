#pragma once
#ifdef AFHOOK_DEVMODE

#include <fstream>
#include "targetver.h"
#include <windows.h>

#include <png.h>

#pragma comment(lib, "zlib.lib")
#pragma comment(lib, "libpng16.lib")

typedef unsigned char		byte;
typedef unsigned short		word;
typedef unsigned long		dword;
typedef unsigned __int64	qword;

class CStream
{
	int		delta;
	byte	temp;

public:
	bool	readclasslist;
	int		redbyte;

			CStream();

	virtual void	read( void *buf, int len ) = 0;
	virtual void	seek( int n ) = 0;

	byte	readbyte();
	word	readword();
	dword	readdword();
	qword	readqword();
	int		readbit();
	int		readunsigned();
	int		readsigned();
	int		getreadbyte();
};



class CMemoryStream : public CStream
{
	byte *	b;

public:
			CMemoryStream( void *buf );
	void	read( void *buf, int len );
	void	seek( int n );
};

class CImageWriter
{
	int		width, height;
	byte *	buf;

public:
			CImageWriter( int w, int h );
	void	set( int x, int y, dword color );
	void	set( int x, int y, byte r, byte g, byte b, byte a=0xff );
	void	write( FILE* f );
			~CImageWriter();

	static void		Opaque1( CStream* s, CImageWriter *image, int width, int height, dword depth );
	static void		Opaque2( CStream *s, CImageWriter *image, int width, int height, dword depth );
	static void		Transparent( CStream *s, CImageWriter *image, int width, int height, dword depth );
};

#endif