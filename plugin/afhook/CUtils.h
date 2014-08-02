#pragma once

#include "targetver.h"
#include <windows.h>

#include <string>

class CUtils {
public:
	static std::wstring GetGameDirectory(const wchar_t* append);

private:
	static wchar_t m_gameDirectory[MAX_PATH];
};