#include "CWindowsFunctions.h"

char CWindowsFunctions::m_uiBuffer[UI_BUFFER];
CResourceManager* CWindowsFunctions::m_resources = NULL;

BOOL (__stdcall* CWindowsFunctions::Orig_AppendMenuA) (HMENU, UINT, UINT_PTR, LPCSTR) = AppendMenuA;
int (__stdcall* CWindowsFunctions::Orig_MessageBoxA) (HWND, LPCSTR, LPCSTR, UINT) = MessageBoxA;
HWND (__stdcall* CWindowsFunctions::Orig_CreateDialogIndirectParamA) (HINSTANCE, LPCDLGTEMPLATE, HWND, DLGPROC, LPARAM) = CreateDialogIndirectParamA;
HMENU (__stdcall* CWindowsFunctions::Orig_LoadMenuIndirectA) (const MENUTEMPLATEA*) = LoadMenuIndirectA;

BOOL __stdcall CWindowsFunctions::W32AppendMenuA(HMENU hMenu, UINT uFlags, UINT_PTR uIDNewItem, LPCSTR lpNewItem)
{
	if (uFlags & ~MF_STRING)
	{
		return CWindowsFunctions::Orig_AppendMenuA(hMenu, uFlags, uIDNewItem, lpNewItem);
	}

	bool result = m_resources->TranslateUserInterface(lpNewItem, m_uiBuffer, UI_BUFFER);

	if (result)
	{
		return CWindowsFunctions::Orig_AppendMenuA(hMenu, uFlags, uIDNewItem, m_uiBuffer);
	}

	return CWindowsFunctions::Orig_AppendMenuA(hMenu, uFlags, uIDNewItem, lpNewItem);
}

int __stdcall CWindowsFunctions::W32MessageBoxA(HWND hwnd, LPCSTR lpText, LPCSTR lpCaption, UINT type)
{
	bool result = m_resources->TranslateUserInterface(lpText, m_uiBuffer, UI_BUFFER);

	if (result)
	{
		return CWindowsFunctions::Orig_MessageBoxA(hwnd, m_uiBuffer, lpCaption, type);
	}

	return CWindowsFunctions::Orig_MessageBoxA(hwnd, lpText, lpCaption, type);
}

HWND __stdcall CWindowsFunctions::W32CreateDialogIndirectParamA(HINSTANCE hInstance, LPCDLGTEMPLATE lpTemplate, HWND hWndParent, DLGPROC lpDialogFunc, LPARAM dwInitParam)
{
	HWND dialog = CWindowsFunctions::Orig_CreateDialogIndirectParamA(hInstance, lpTemplate, hWndParent, lpDialogFunc, dwInitParam);
	if (dialog == NULL) return NULL;

	CWindowsFunctions::CB_EnumChildProc(dialog, NULL);
	EnumChildWindows(dialog, CWindowsFunctions::CB_EnumChildProc, NULL);

	return dialog;
}

BOOL CALLBACK CWindowsFunctions::CB_EnumChildProc(HWND hwnd, LPARAM lParam)
{
	int length = GetWindowTextLengthA(hwnd);

	if (length > 0)
	{
		length++; // NULL character

		char* jpn = new char[length];
	
		length = GetWindowTextA(hwnd, jpn, length);

		if (length > 0)
		{
			char className[6];
			GetClassNameA(hwnd, className, sizeof(className));

			if (strcmp(className, "Edit") != 0)
			{
				bool result = m_resources->TranslateUserInterface(jpn, m_uiBuffer, UI_BUFFER);

				if (result)
				{
					SetWindowTextA(hwnd, m_uiBuffer);
				}
			}
		}

		delete[] jpn;
	}

	return TRUE;
}

HMENU __stdcall CWindowsFunctions::W32LoadMenuIndirectA(const MENUTEMPLATEA* lpMenuTemplate)
{
	HMENU menu = CWindowsFunctions::Orig_LoadMenuIndirectA(lpMenuTemplate);
	if (menu == NULL) return NULL;

	RecursiveTranslateMenu(menu);

	return menu;
}

void CWindowsFunctions::RecursiveTranslateMenu(HMENU menu)
{
	int items = GetMenuItemCount(menu);

	if (items > 0)
	{
		for (int i = 0; i < items; i++)
		{
			// Get menu type and text length
			MENUITEMINFOA info;
			info.cbSize = sizeof(MENUITEMINFOA);
			info.dwTypeData = NULL;
			info.fMask = MIIM_FTYPE | MIIM_STRING;
			GetMenuItemInfoA(menu, i, true, &info);

			// Ignore seperators and other junk we don't need to translate
			if (info.fType & ~MFT_STRING || info.cch == 1) continue;

			// Get submenu and text
			info.cch += 1;
			info.dwTypeData = new char[info.cch];
			info.fMask = MIIM_SUBMENU | MIIM_STRING;
			GetMenuItemInfoA(menu, i, true, &info);

			// Recursion
			if (info.hSubMenu != NULL)
			{
				RecursiveTranslateMenu(info.hSubMenu);
			}

			// Translation
			bool result = m_resources->TranslateUserInterface(info.dwTypeData, m_uiBuffer, UI_BUFFER);
			delete[] info.dwTypeData;

			if (result)
			{
				// info.cch = strnlen(m_uiBuffer, UI_BUFFER - 1) + 1;
				info.dwTypeData = m_uiBuffer;
				info.fMask = MIIM_STRING;
				SetMenuItemInfoA(menu, i, true, &info);
			}
		}
	}
}