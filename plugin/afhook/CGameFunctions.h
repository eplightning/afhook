#pragma once

#include "CResourceManager.h"
#include "CUtils.h"

#define TEXT_BUFFER 8192

class CGameFunctions {
public:
	// Common functions
	static void* __stdcall HandleCharacterColor(void* unknownPointer, const char* text);
	static void* __cdecl DecodeR6TiOpaque1(void* imageData, ImageInfo* info, INT32 unknown, INT32 unknown2, INT32 unknown3, ImageDimensions dimensions);
	static void* __cdecl DecodeR6TiTransparent(void* imageData, ImageInfo* info, INT32 unknown, INT32 unknown2, INT32 unknown3, INT32 unknown4, INT32 unknown5, INT32 unknown6, INT32 unknown7);

	// Patched Chronicles 04 and up
	static void __fastcall HandleText7(void* thisPointer, void* junk, const char* text, void* p1, void* p2, void* p3, void* p4, void* p5, void* p6, void* p7);
	
	// <Chronicles 01; Unpatched Chronicles 04)
	static void __fastcall HandleText6(void* thisPointer, void* junk, const char* text, void* p1, void* p2, void* p3, void* p4, void* p5, void* p6);

	// <Altered Fable; Chronicles 01)
	static void __fastcall HandleText5(void* thisPointer, void* junk, const char* text, void* p1, void* p2, void* p3, void* p4, void* p5);

	// Older
	static void __fastcall HandleText3(void* thisPointer, void* junk, const char* text, void* p1, void* p2, void* p3);

	// rUGP real functions
	static void* (__stdcall* Orig_HandleCharacterColor) (void*, const char*);
	static void (__fastcall* Orig_HandleText7) (void*, void*, const char*, void*, void*, void*, void*, void*, void*, void*);
	static void (__fastcall* Orig_HandleText6) (void*, void*, const char*, void*, void*, void*, void*, void*, void*);
	static void (__fastcall* Orig_HandleText5) (void*, void*, const char*, void*, void*, void*, void*, void*);
	static void (__fastcall* Orig_HandleText3) (void*, void*, const char*, void*, void*, void*);
	static void* (__cdecl* Orig_DecodeR6TiOpaque1) (void*, ImageInfo*, INT32, INT32, INT32, ImageDimensions);
	static void* (__cdecl* Orig_DecodeR6TiTransparent) (void*, ImageInfo*, INT32, INT32, INT32, INT32, INT32, INT32, INT32);

	static CResourceManager* m_resources;

private:
	static bool m_fixCharacters;
	static char m_lastLine[TEXT_BUFFER];
	static int  m_lastLinePosition;
	static char m_textBuffer[TEXT_BUFFER];
};