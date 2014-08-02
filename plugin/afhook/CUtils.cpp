#include "CUtils.h"

wchar_t CUtils::m_gameDirectory[MAX_PATH] = {L'\x00'};

std::wstring CUtils::GetGameDirectory(const wchar_t* append)
{
	if (m_gameDirectory[0] == L'\x00')
	{
		GetModuleFileName(GetModuleHandle(NULL), m_gameDirectory, MAX_PATH);

		for (int i = wcsnlen(m_gameDirectory, MAX_PATH); i >= 0; i--)
		{
			if (m_gameDirectory[i] == L'\\')
			{
				m_gameDirectory[i] = L'\x00';
				break;
			}
		}
	}

	std::wstring path(m_gameDirectory);
	path.append(append);

	return path;
}