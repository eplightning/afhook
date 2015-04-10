#include "CHookEngine.h"

BOOL CHookEngine::HookGame()
{
	CGameFunctions::m_resources = this->m_res;
	CWindowsFunctions::m_resources = this->m_res;

	m_logger->WriteLine("Initializing hook");
	HMODULE hmoduleVm60 = GetModuleHandle(TEXT("Vm60"));
	HMODULE hmoduleRvmm = GetModuleHandle(TEXT("rvmm"));

	// Opaque1
	byte patternTexture60[] = { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x10, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x33, 0xC5, 0x89, 0x45, 0x00, 0x8B, 0x45 };
	UINT_PTR ptrTexture1 = this->FindMemoryPattern("xxxxxxx????xxxx?xx", patternTexture60, ((UINT_PTR) hmoduleRvmm + 0x65000), 0x30000);
	
	if (ptrTexture1 != 0)
	{
		m_logger->WriteLine("Pattern finder found location of DecodeR6TiOpaque1 function: ").WritePointer(ptrTexture1);
	}
	else
	{
		byte patternTexture1[] = { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x0C, 0x8B, 0x45, 0x08, 0x33, 0xC9, 0x33, 0xD2, 0x56, 0x66 };
		ptrTexture1 = this->FindMemoryPattern("xxxxxxxxxxxxxxx", patternTexture1, ((UINT_PTR) hmoduleRvmm + 0x60000), 0x20000);

		if (ptrTexture1 != 0)
		{
			m_logger->WriteLine("Pattern finder found location of DecodeR6TiOpaque1 function: ").WritePointer(ptrTexture1);
		}
		else
		{
			m_logger->WriteLine("Pattern finder was unable to find location of DecodeR6TiOpaque1 function");
		}
	}

	// Transparent
	byte patternTexture260[] = { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x10, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x33, 0xC5, 0x89, 0x45, 0xFC, 0x0F, 0xBF, 0x45 };
	UINT_PTR ptrTexture2 = this->FindMemoryPattern("xxxxxxx????xxxxxxxx", patternTexture260, ((UINT_PTR) hmoduleRvmm + 0x65000), 0x30000);

	if (ptrTexture2 != 0)
	{
		m_logger->WriteLine("Pattern finder found location of DecodeR6TiTransparent function: ").WritePointer(ptrTexture2);
	}
	else
	{
		byte patternTexture2[] = { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x10, 0x0F, 0xBF, 0x45, 0x18, 0x0F, 0xBF, 0x4D, 0x1C, 0x2B, 0xC8, 0x56, 0x81, 0xF9, 0x00, 0x10 };
		ptrTexture2 = this->FindMemoryPattern("xxxxxxxxxxxxxxxxxxxxx", patternTexture2, ((UINT_PTR) hmoduleRvmm + 0x60000), 0x20000);

		if (ptrTexture2 != 0)
		{
			m_logger->WriteLine("Pattern finder found location of DecodeR6TiTransparent function: ").WritePointer(ptrTexture2);
		}
		else
		{
			m_logger->WriteLine("Pattern finder was unable to find location of DecodeR6TiTransparent function");
		}
	}
	
	// HandleText
	HookInfo handleText = this->FindHandleText((UINT_PTR) hmoduleVm60);
	
	switch (handleText.version)
	{
		case 7:
			m_logger->WriteLine("Pattern finder found location of patched Chronicles 04 HandleText function: ").WritePointer(handleText.address);
			break;

		case 6:
			m_logger->WriteLine("Pattern finder found location of HandleText function: ").WritePointer(handleText.address);
			break;

		case 5:
			m_logger->WriteLine("Pattern finder found location of rUGP 5.8 HandleText function: ").WritePointer(handleText.address);
			break;

		case 4:
			m_logger->WriteLine("Pattern finder found location of legacy rUGP HandleText function: ").WritePointer(handleText.address);
			break;

		case 0:
			m_logger->WriteLine("Pattern finder was unable to find location of HandleText function");
	}

	// GetCharacterName
	byte patternCharName[] = { 0x55, 0x8B, 0xEC, 0x6A, 0xFF, 0x68, 0x00, 0x00, 0x00, 0x00, 0x64, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x50, 0x51, 0x53, 0x56, 0x57, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x8D, 0x4D };
	UINT_PTR ptrCharName = this->FindMemoryPattern("xxxxxx????xxxxxxxxxxxx????????????????xx", patternCharName, ((UINT_PTR) hmoduleVm60 + 0xF000), 0x11000);

	if (ptrCharName != 0)
	{
		m_logger->WriteLine("Pattern finder found location of GetCharacterName function: ").WritePointer(ptrCharName);
	}
	else
	{
		byte patternCharName2[] = { 0x6A, 0xFF, 0x68, 0x00, 0x00, 0x00, 0x00, 0x64, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x50, 0x64, 0x89, 0x25, 0x00, 0x00, 0x00, 0x00, 0x83, 0xEC, 0x08, 0x55, 0x56, 0x57, 0x8D, 0x4C, 0x24, 0x0C };
		ptrCharName = this->FindMemoryPattern("xxx????xxxxxxxxxxxxxxxxxxxxxxxx", patternCharName2, ((UINT_PTR) hmoduleVm60 + 0x13A40), 0x3000);

		if (ptrCharName != 0)
		{
			m_logger->WriteLine("Pattern finder found location of GetCharacterName function: ").WritePointer(ptrCharName);
		}
		else
		{
			byte patternCharName3[] = { 0x6A, 0xFF, 0x68, 0x00, 0x00, 0x00, 0x00, 0x64, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x50, 0x64, 0x89, 0x25, 0x00, 0x00, 0x00, 0x00, 0x83, 0xEC, 0x08, 0x53, 0x55, 0x56, 0x33, 0xED, 0x57, 0x8D };
			ptrCharName = this->FindMemoryPattern("xxx????xxxxxxxxxxxxxxxxxxxxxxxx", patternCharName3, ((UINT_PTR) hmoduleVm60 + 0x10700), 0x3000);

			if (ptrCharName != 0)
			{
				m_logger->WriteLine("Pattern finder found location of GetCharacterName function: ").WritePointer(ptrCharName);
			}
			else
			{
				m_logger->WriteLine("Pattern finder was unable to find location of GetCharacterName function");
			}
		}
	}

	// Magic
	m_logger->WriteLine("Hooking game and WinAPI functions");

	DetourTransactionBegin();
	DetourUpdateThread(GetCurrentThread());

	switch (handleText.version)
	{
		case 7:
			DetourAttach(&(PVOID&) handleText.address, CGameFunctions::HandleText7);
			break;

		case 6:
			DetourAttach(&(PVOID&) handleText.address, CGameFunctions::HandleText6);
			break;

		case 5:
			DetourAttach(&(PVOID&) handleText.address, CGameFunctions::HandleText5);
			break;

		case 3:
			DetourAttach(&(PVOID&) handleText.address, CGameFunctions::HandleText3);
	}

	// TODO: fix images for old rUGP
	if (handleText.version == 3)
	{
		ptrTexture1 = 0;
		ptrTexture2 = 0;
	}

	if (ptrCharName != 0) DetourAttach(&(PVOID&) ptrCharName, CGameFunctions::HandleCharacterColor);
	if (ptrTexture1 != 0) DetourAttach(&(PVOID&) ptrTexture1, CGameFunctions::DecodeR6TiOpaque1);
	if (ptrTexture2 != 0) DetourAttach(&(PVOID&) ptrTexture2, CGameFunctions::DecodeR6TiTransparent);

	DetourAttach(&(PVOID&) CWindowsFunctions::Orig_AppendMenuA, CWindowsFunctions::W32AppendMenuA);
	DetourAttach(&(PVOID&) CWindowsFunctions::Orig_MessageBoxA, CWindowsFunctions::W32MessageBoxA);
	DetourAttach(&(PVOID&) CWindowsFunctions::Orig_LoadMenuIndirectA, CWindowsFunctions::W32LoadMenuIndirectA);
	DetourAttach(&(PVOID&) CWindowsFunctions::Orig_CreateDialogIndirectParamA, CWindowsFunctions::W32CreateDialogIndirectParamA);

	if (DetourTransactionCommit() != ERROR_SUCCESS)
	{
		m_logger->WriteLine("DetourTransactionCommit failed ...");
		return FALSE;
	}

	// Save old functions
	CGameFunctions::Orig_HandleText7 = (void (__fastcall*) (void*, void*, const char*, void*, void*, void*, void*, void*, void*, void*)) handleText.address;
	CGameFunctions::Orig_HandleText6 = (void (__fastcall*) (void*, void*, const char*, void*, void*, void*, void*, void*, void*)) handleText.address;
	CGameFunctions::Orig_HandleText5 = (void (__fastcall*) (void*, void*, const char*, void*, void*, void*, void*, void*)) handleText.address;
	CGameFunctions::Orig_HandleText3 = (void (__fastcall*) (void*, void*, const char*, void*, void*, void*)) handleText.address;
	CGameFunctions::Orig_HandleCharacterColor = (void* (__stdcall*) (void*, const char*)) ptrCharName;
	CGameFunctions::Orig_DecodeR6TiOpaque1 = (void* (__cdecl*) (void*, ImageInfo*, INT32, INT32, INT32, ImageDimensions)) ptrTexture1;
	CGameFunctions::Orig_DecodeR6TiTransparent = (void* (__cdecl*) (void*, ImageInfo*, INT32, INT32, INT32, INT32, INT32, INT32, INT32)) ptrTexture2;
	
	m_logger->WriteLine("Hooking done");

	return TRUE;
}

BOOL CHookEngine::UnhookGame()
{
	DetourTransactionBegin();
	DetourUpdateThread(GetCurrentThread());
	if (CGameFunctions::Orig_HandleText7 != 0) DetourDetach(&(PVOID&) CGameFunctions::Orig_HandleText7, CGameFunctions::HandleText7);
	if (CGameFunctions::Orig_HandleText6 != 0) DetourDetach(&(PVOID&) CGameFunctions::Orig_HandleText6, CGameFunctions::HandleText6);
	if (CGameFunctions::Orig_HandleText5 != 0) DetourDetach(&(PVOID&) CGameFunctions::Orig_HandleText5, CGameFunctions::HandleText5);
	if (CGameFunctions::Orig_HandleText3 != 0) DetourDetach(&(PVOID&) CGameFunctions::Orig_HandleText3, CGameFunctions::HandleText3);
	if (CGameFunctions::Orig_HandleCharacterColor != 0) DetourDetach(&(PVOID&) CGameFunctions::Orig_HandleCharacterColor, CGameFunctions::HandleCharacterColor);
	if (CGameFunctions::Orig_DecodeR6TiOpaque1 != 0) DetourDetach(&(PVOID&) CGameFunctions::Orig_DecodeR6TiOpaque1, CGameFunctions::DecodeR6TiOpaque1);
	if (CGameFunctions::Orig_DecodeR6TiTransparent != 0) DetourDetach(&(PVOID&) CGameFunctions::Orig_DecodeR6TiTransparent, CGameFunctions::DecodeR6TiTransparent);
	DetourDetach(&(PVOID&) CWindowsFunctions::Orig_AppendMenuA, CWindowsFunctions::W32AppendMenuA);
	DetourDetach(&(PVOID&) CWindowsFunctions::Orig_MessageBoxA, CWindowsFunctions::W32MessageBoxA);
	DetourDetach(&(PVOID&) CWindowsFunctions::Orig_LoadMenuIndirectA, CWindowsFunctions::W32LoadMenuIndirectA);
	DetourDetach(&(PVOID&) CWindowsFunctions::Orig_CreateDialogIndirectParamA, CWindowsFunctions::W32CreateDialogIndirectParamA);

	return (DetourTransactionCommit() == ERROR_SUCCESS);
}

HookInfo CHookEngine::FindHandleText(UINT_PTR module)
{
	HookInfo out;
	out.version = 0;

	// rUGP 6.2 November Update
	out.version = 7;
	byte patternText[] = { 0x55, 0x8B, 0xEC, 0x6A, 0xFF, 0x68, 0x00, 0x00, 0x00, 0x00, 0x64, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x50, 0x83, 0xEC, 0x38, 0x53, 0x56, 0x57, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x33, 0xC5, 0x50, 0x8D, 0x45, 0xF4, 0x64, 0xA3, 0x00, 0x00, 0x00, 0x00, 0x8B, 0xF1, 0x33, 0xFF };
	out.address = this->FindMemoryPattern("xxxxxx????xxxxxxxxxxxxxx????xxxxxxxx????xxxx", patternText, (module + 0x10000), 0x30000);

	if (out.address) return out;

	// <= rUGP 6.1+6.2
	out.version = 6;

	byte patternText1[] = { 0x55, 0x8B, 0xEC, 0x6A, 0xFF, 0x68, 0x00, 0x00, 0x00, 0x00, 0x64, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x50, 0x83, 0xEC, 0x24, 0x53, 0x56, 0x57, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x33, 0xC5, 0x50, 0x8D, 0x45, 0xF4, 0x64, 0xA3, 0x00, 0x00, 0x00, 0x00, 0x8B, 0xF1, 0x33, 0xFF };
	out.address = this->FindMemoryPattern("xxxxxx????xxxxxxxxxxxxxx????xxxxxxxx????xxxx", patternText1, (module + 0x10000), 0x30000);

	if (out.address) return out;

	byte patternText2[] = { 0x55, 0x8B, 0xEC, 0x6A, 0xFF, 0x68, 0x00, 0x00, 0x00, 0x00, 0x64, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x50, 0x83, 0xEC, 0x0C, 0x53, 0x56, 0x57, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x33, 0xC5, 0x50, 0x8D, 0x45, 0xF4, 0x64, 0xA3, 0x00, 0x00, 0x00, 0x00, 0x8B, 0xF9, 0x33, 0xF6 };
	out.address = this->FindMemoryPattern("xxxxxx????xxxxxxxxxxxxxx????xxxxxxxx????xxxx", patternText2, (module + 0x10000), 0x30000);

	if (out.address) return out;

	byte patternText3[] = { 0x6A, 0xFF, 0x68, 0x00, 0x00, 0x00, 0x00, 0x64, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x50, 0x64, 0x89, 0x25, 0x00, 0x00, 0x00, 0x00, 0x83, 0xEC, 0x08, 0x53, 0x55, 0x56, 0x57, 0x8B, 0xF1, 0x33, 0xC0, 0xC7, 0x44, 0x24, 0x20, 0x01, 0x00, 0x00, 0x00, 0x8A, 0x86, 0xFE };
	out.address = this->FindMemoryPattern("xxx????xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", patternText3, (module + 0x10000), 0x30000);

	if (out.address) return out;

	// rUGP 5.8
	out.version = 5;
	byte patternText4[] = { 0x6A, 0xFF, 0x68, 0x00, 0x00, 0x00, 0x00, 0x64, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x50, 0x64, 0x89, 0x25, 0x00, 0x00, 0x00, 0x00, 0x83, 0xEC, 0x08, 0x53, 0x55, 0x56, 0x57, 0x8B, 0xF1, 0x33, 0xC0, 0xC7 };
	out.address = this->FindMemoryPattern("xxx????xxxxxxxxxxxxxxxxxxxxxxxxxx", patternText4, (module + 0x10000), 0x30000);

	if (out.address) return out;

	// Legacy rUGP
	out.version = 3;
	byte patternText5[] = { 0x64, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x6A, 0xFF, 0x68, 0x00, 0x00, 0x00, 0x00, 0x50, 0x64, 0x89, 0x25, 0x00, 0x00, 0x00, 0x00, 0x83, 0xEC, 0x08, 0x53, 0x55, 0x56, 0x57, 0x8B, 0x7C, 0x24, 0x28, 0x8B, 0xF1, 0x57, 0x8D, 0x4E };
	out.address = this->FindMemoryPattern("xxxxxxxxx????xxxxxxxxxxxxxxxxxxxxxxxx", patternText5, (module + 0x10000), 0x30000);

	if (!out.address) out.version = 0;

	return out;
}

// Based on www.gamedeception.net/threads/20592-Incredible-optimized-FindPattern
bool CHookEngine::MemoryCompare(const byte* data, const byte* datamask, const char* mask)
{
	for(; *mask; ++data, ++datamask, ++mask)
	{
		if (!strcmp(mask, "xxxx"))
		{
			if (*(UINT32*) data != *(UINT32*) datamask)
			{
				return FALSE;
			}

			data += 3, datamask += 3, mask += 3;
			continue;
		}

		if (!strcmp(mask, "xx"))
		{
			if (*(UINT16*) data != *(UINT16*) datamask)
			{
				return FALSE;
			}

			data++, datamask++, mask++;
			continue;
		}

		if (*mask == 'x' && *data != *datamask)
		{
			return false;
		}
	}

	return (*mask) == 0;
}

UINT_PTR CHookEngine::FindMemoryPattern(const char* mask, byte* datamask, UINT_PTR start, UINT_PTR length)
{
	UINT_PTR end = start + length;

	for (UINT_PTR i = start; i < end; i++)
	{
		if (this->MemoryCompare((byte*) i, datamask, mask))
		{
			return i;
		}
	}

	return 0;
}