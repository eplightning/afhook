#include "CGameFunctions.h"

bool CGameFunctions::m_fixCharacters = false;
char CGameFunctions::m_lastLine[TEXT_BUFFER];
int  CGameFunctions::m_lastLinePosition = 0;
char CGameFunctions::m_textBuffer[TEXT_BUFFER];

CResourceManager* CGameFunctions::m_resources = NULL;
void* (__stdcall* CGameFunctions::Orig_HandleCharacterColor) (void*, const char*) = NULL;
void (__fastcall* CGameFunctions::Orig_HandleText7) (void*, void*, const char*, void*, void*, void*, void*, void*, void*, void*) = NULL;
void (__fastcall* CGameFunctions::Orig_HandleText6) (void*, void*, const char*, void*, void*, void*, void*, void*, void*) = NULL;
void (__fastcall* CGameFunctions::Orig_HandleText5) (void*, void*, const char*, void*, void*, void*, void*, void*) = NULL;
void (__fastcall* CGameFunctions::Orig_HandleText3) (void*, void*, const char*, void*, void*, void*) = NULL;
void* (__cdecl* CGameFunctions::Orig_DecodeR6TiOpaque1) (void*, ImageInfo*, INT32, INT32, INT32, ImageDimensions) = NULL;
void* (__cdecl* CGameFunctions::Orig_DecodeR6TiTransparent) (void*, ImageInfo*, INT32, INT32, INT32, INT32, INT32, INT32, INT32) = NULL;

void __fastcall CGameFunctions::HandleText7(void* thisPointer, void* junk, const char* text, void* p1, void* p2, void* p3, void* p4, void* p5, void* p6, void* p7)
{
	if (*text == '\x00' || (*text == '\x05' && *(text + 1) == '\x00'))
	{
		Orig_HandleText7(thisPointer, junk, text, p1, p2, p3, p4, p5, p6, p7);
		return;
	}

	m_fixCharacters = false;
	strncpy(m_lastLine, text, TEXT_BUFFER);
	m_lastLinePosition = 0;
	bool result = m_resources->TranslateText(text, m_textBuffer, TEXT_BUFFER);

	if (result)
	{
		if (m_lastLine[0] == '\x81' && m_lastLine[1] == '\x79')
		{
			m_fixCharacters = true;
		}

		Orig_HandleText7(thisPointer, junk, m_textBuffer, p1, p2, p3, p4, p5, p6, p7);
	}
	else
	{
		Orig_HandleText7(thisPointer, junk, text, p1, p2, p3, p4, p5, p6, p7);
	}
}

void __fastcall CGameFunctions::HandleText6(void* thisPointer, void* junk, const char* text, void* p1, void* p2, void* p3, void* p4, void* p5, void* p6)
{
	if (*text == '\x00' || (*text == '\x05' && *(text + 1) == '\x00'))
	{
		Orig_HandleText6(thisPointer, junk, text, p1, p2, p3, p4, p5, p6);
		return;
	}

	m_fixCharacters = false;
	strncpy(m_lastLine, text, TEXT_BUFFER);
	m_lastLinePosition = 0;
	bool result = m_resources->TranslateText(text, m_textBuffer, TEXT_BUFFER);

	if (result)
	{
		if (m_lastLine[0] == '\x81' && m_lastLine[1] == '\x79')
		{
			m_fixCharacters = true;
		}

		Orig_HandleText6(thisPointer, junk, m_textBuffer, p1, p2, p3, p4, p5, p6);
	}
	else
	{
		Orig_HandleText6(thisPointer, junk, text, p1, p2, p3, p4, p5, p6);
	}
}

void __fastcall CGameFunctions::HandleText5(void* thisPointer, void* junk, const char* text, void* p1, void* p2, void* p3, void* p4, void* p5)
{
	if (*text == '\x00' || (*text == '\x05' && *(text + 1) == '\x00'))
	{
		Orig_HandleText5(thisPointer, junk, text, p1, p2, p3, p4, p5);
		return;
	}

	m_fixCharacters = false;
	strncpy(m_lastLine, text, TEXT_BUFFER);
	m_lastLinePosition = 0;
	bool result = m_resources->TranslateText(text, m_textBuffer, TEXT_BUFFER);

	if (result)
	{
		if (m_lastLine[0] == '\x81' && m_lastLine[1] == '\x79')
		{
			m_fixCharacters = true;
		}

		Orig_HandleText5(thisPointer, junk, m_textBuffer, p1, p2, p3, p4, p5);
	}
	else
	{
		Orig_HandleText5(thisPointer, junk, text, p1, p2, p3, p4, p5);
	}
}

void __fastcall CGameFunctions::HandleText3(void* thisPointer, void* junk, const char* text, void* p1, void* p2, void* p3)
{
	if (*text == '\x00' || (*text == '\x05' && *(text + 1) == '\x00'))
	{
		Orig_HandleText3(thisPointer, junk, text, p1, p2, p3);
		return;
	}

	m_fixCharacters = false;
	strncpy(m_lastLine, text, TEXT_BUFFER);
	m_lastLinePosition = 0;
	bool result = m_resources->TranslateText(text, m_textBuffer, TEXT_BUFFER);

	if (result)
	{
		if (m_lastLine[0] == '\x81' && m_lastLine[1] == '\x79')
		{
			m_fixCharacters = true;
		}

		Orig_HandleText3(thisPointer, junk, m_textBuffer, p1, p2, p3);
	}
	else
	{
		Orig_HandleText3(thisPointer, junk, text, p1, p2, p3);
	}
}

void* __stdcall CGameFunctions::HandleCharacterColor(void* unknownPointer, const char* textLine)
{
	if (m_fixCharacters)
	{
		while (m_lastLine[m_lastLinePosition] != '\x00')
		{
			if (m_lastLine[m_lastLinePosition] == '\x81' && m_lastLine[m_lastLinePosition + 1] == '\x79')
			{
				m_lastLinePosition += 2;
				break;
			}
			else
			{
				m_lastLinePosition++;
			}
		}

		return Orig_HandleCharacterColor(unknownPointer, &m_lastLine[m_lastLinePosition]);
	}

	return Orig_HandleCharacterColor(unknownPointer, textLine);
}

void* __cdecl CGameFunctions::DecodeR6TiOpaque1(void* imageData, ImageInfo* info, INT32 unknown, INT32 unknown2, INT32 unknown3, ImageDimensions dimensions)
{
	// Crash
	if (unknown3 != 0)
	{
		return Orig_DecodeR6TiOpaque1(imageData, info, unknown, unknown2, unknown3, dimensions);
	}

	// Let's calculate SHA1 of this image to identify it
	int type = 1;
	SHA1Context context;
	SHA1Reset(&context);
	SHA1Input(&context, (unsigned char*) &type, 4);
	SHA1Input(&context, (unsigned char*) &dimensions, 4); // width/height
	SHA1Input(&context, (unsigned char*) info, 8); // depth
	SHA1Input(&context, (unsigned char*) &info->dataSize, 4);

	int bytesToRead = 1024;

	if (info->dataSize < 1024) bytesToRead = info->dataSize;

	SHA1Input(&context, (unsigned char*) imageData, bytesToRead);
	
	if (!SHA1Result(&context))
	{
		return Orig_DecodeR6TiOpaque1(imageData, info, unknown, unknown2, unknown3, dimensions);
	}

	// Save SHA1 hash in string form
	char* hash = new char[41];
	sprintf(hash, "%08X%08X%08X%08X%08X", context.Message_Digest[0], context.Message_Digest[1], context.Message_Digest[2], context.Message_Digest[3], context.Message_Digest[4]);
	
	// Let resource manager do his job
	void* result = m_resources->TranslateImage(hash, imageData, dimensions, info->depth2, 2);
	delete[] hash;

	// Replacement
	if (result != NULL)
	{
		return Orig_DecodeR6TiOpaque1(result, info, unknown, unknown2, unknown3, dimensions);
	}

	return Orig_DecodeR6TiOpaque1(imageData, info, unknown, unknown2, unknown3, dimensions);
}

void* __cdecl CGameFunctions::DecodeR6TiTransparent(void* imageData, ImageInfo* info, INT32 unknown, INT32 unknown2, INT32 unknown3, INT32 unknown4, INT32 unknown5, INT32 unknown6, INT32 unknown7)
{
	// TODO: Find more reliable way to retrieve that stuff
	ImageDimensions* dimensions = (ImageDimensions*) info;
	dimensions -= 3;

	// Let's calculate SHA1 of this image to identify it
	int type = 2;
	SHA1Context context;
	SHA1Reset(&context);
	SHA1Input(&context, (unsigned char*) &type, 4);
	SHA1Input(&context, (unsigned char*) dimensions, 4); // width/height
	SHA1Input(&context, (unsigned char*) info, 8); // depth
	SHA1Input(&context, (unsigned char*) &info->dataSize, 4);

	int bytesToRead = 256;

	if (info->dataSize < 256) bytesToRead = info->dataSize;

	SHA1Input(&context, (unsigned char*) imageData, bytesToRead);
	
	if (!SHA1Result(&context))
	{
		return Orig_DecodeR6TiTransparent(imageData, info, unknown, unknown2, unknown3, unknown4, unknown5, unknown6, unknown7);
	}

	// Save SHA1 hash in string form
	char* hash = new char[41];
	sprintf(hash, "%08X%08X%08X%08X%08X", context.Message_Digest[0], context.Message_Digest[1], context.Message_Digest[2], context.Message_Digest[3], context.Message_Digest[4]);
	
	// Let resource manager do his job
	void* result = m_resources->TranslateImage(hash, imageData, *dimensions, info->depth2, 3);
	delete[] hash;

	// Replacement
	if (result != NULL)
	{
		return Orig_DecodeR6TiTransparent(result, info, unknown, unknown2, unknown3, unknown4, unknown5, unknown6, unknown7);
	}

	return Orig_DecodeR6TiTransparent(imageData, info, unknown, unknown2, unknown3, unknown4, unknown5, unknown6, unknown7);
}