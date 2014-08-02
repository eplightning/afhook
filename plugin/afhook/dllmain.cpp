#include "targetver.h"
#include <windows.h>

#include "afhook.h"
#include "CHookEngine.h"
#include "CLogger.h"
#include "CResourceManager.h"
#include "CUtils.h"

CHookEngine g_hookEngine;
CFileLogger g_logger;
CResourceManager g_resources;

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	if (ul_reason_for_call == DLL_PROCESS_ATTACH)
	{
		g_logger.Open(CUtils::GetGameDirectory(L"\\afhook.log"));

		g_resources.SetLogger(&g_logger);
		g_resources.LoadPackage(CUtils::GetGameDirectory(L"\\afhook.pkg"));

		g_hookEngine.SetLogger(&g_logger);
		g_hookEngine.SetResourceManager(&g_resources);

		return g_hookEngine.HookGame();
	}
	else if (ul_reason_for_call == DLL_PROCESS_DETACH)
	{
		g_resources.SavePackage(CUtils::GetGameDirectory(L"\\afhook.pkg"));

		return g_hookEngine.UnhookGame();
	}
	else if (ul_reason_for_call == DLL_THREAD_DETACH)
	{
		if (g_resources.IsModified())
		{
			g_resources.SavePackage(CUtils::GetGameDirectory(L"\\afhook.pkg"));
		}
	}
	
	return TRUE;
}