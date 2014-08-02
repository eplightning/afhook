#pragma once

#include "targetver.h"
#include <windows.h>

#include <map>
#include <unordered_map>
#include <vector>
#include <fstream>

#ifdef AFHOOK_DEVMODE
#include "CImageWriter.h"
#endif

extern "C" {
	#include "sha1.h"
}
#include "CLogger.h"
#include "CUtils.h"

#define PACKAGE_MAGIC_HEADER 0x8D120AB6

struct PackageHeader {
	UINT32 magicHeader;
	UINT32 textOffset;
	UINT32 textCount;
	UINT32 charactersOffset; // deprecated
	UINT32 charactersCount; // deprecated
	UINT32 uiOffset;
	UINT32 uiCount;
	UINT32 imageOffset;
	UINT32 imageCount;
	UINT32 reserved1;
	UINT32 reserved2;
	UINT32 reserved3;
	UINT32 reserved4;
};

struct PackageText {
	UINT32 sourceLen;
	char* source;
	UINT32 translationLen;
	char* translation;
};

struct PackageImage {
	char* hash;
	UINT32 titleLen;
	char* title;
	INT16 width;
	INT16 height;
	INT32 flags;
	INT32 depth;
	UINT32 replacementLen;
	unsigned char* replacement;
};

struct CompareCString
{
   bool operator()(char const *a, char const *b)
   {
      return strcmp(a, b) < 0;
   }
};

struct ImageDimensions {
	INT16 width;
	INT16 height;
};

struct ImageInfo {
	INT32 depth;
	INT32 depth2;
	INT32 unknown;
	INT32 unknown2;
	INT32 unknown3;
	INT32 dataSize; // Data size or not, this helps a lot when creating hash
};

typedef std::unordered_map<int, std::vector<PackageText*>*> TextLookupMap;
typedef std::map<const char*, PackageImage*, CompareCString> ImageMap;

class CResourceManager {
public:
	// Generic functions
	CResourceManager();
	~CResourceManager();
	void SetLogger(CLogger* logger) { m_logger = logger; };
	void LoadPackage(const std::wstring& filename);
	void LoadPackage(const char* filename);
	void SavePackage(const std::wstring& filename);
	void SavePackage(const char* filename);
	bool IsModified() const { return m_modified; };

	// Used by afhook
	bool TranslateText(const char* original, char* buffer, int bufferSize);
	bool TranslateUserInterface(const char* original, char* buffer, int bufferSize);
	unsigned char* TranslateImage(const char* hash, void* imageData, ImageDimensions dimensions, INT32 depth, INT32 flags);

private:
	std::map<UINT32, PackageText*> m_textDatabase;
	std::map<UINT32, PackageText*> m_uiDatabase;
	ImageMap m_imageDatabase;
	TextLookupMap m_fastTextLookup;
	UINT32 m_nextTextId;
	UINT32 m_nextUIId;
	bool m_modified;

	CLogger* m_logger;
};