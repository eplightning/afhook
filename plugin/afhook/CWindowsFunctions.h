#pragma once

#include "targetver.h"
#include <windows.h>

#include "CResourceManager.h"

#define UI_BUFFER 8192

class CWindowsFunctions {
public:
	// Detours
	static BOOL __stdcall W32AppendMenuA(HMENU hMenu, UINT uFlags, UINT_PTR uIDNewItem, LPCSTR lpNewItem);
	static int __stdcall W32MessageBoxA(HWND hwnd, LPCSTR lpText, LPCSTR lpCaption, UINT type);
	static HWND __stdcall W32CreateDialogIndirectParamA(HINSTANCE hInstance, LPCDLGTEMPLATE lpTemplate, HWND hWndParent, DLGPROC lpDialogFunc, LPARAM dwInitParam);
	static HMENU __stdcall W32LoadMenuIndirectA(const MENUTEMPLATEA* lpMenuTemplate);

	// Win32 real functions
	static BOOL (__stdcall* Orig_AppendMenuA) (HMENU, UINT, UINT_PTR, LPCSTR);
	static int (__stdcall* Orig_MessageBoxA) (HWND, LPCSTR, LPCSTR, UINT);
	static HWND (__stdcall* Orig_CreateDialogIndirectParamA) (HINSTANCE, LPCDLGTEMPLATE, HWND, DLGPROC, LPARAM);
	static HMENU (__stdcall* Orig_LoadMenuIndirectA) (const MENUTEMPLATEA*);

	static CResourceManager* m_resources;

private:
	static void RecursiveTranslateMenu(HMENU menu);
	static BOOL CALLBACK CB_EnumChildProc(HWND hwnd, LPARAM lParam);

	static char m_uiBuffer[UI_BUFFER];
};