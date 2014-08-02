/**
 * rUGP uses this export either to determine if library is intended to be used as rUGP plugin
 */
extern "C" __declspec(dllexport) void PluginThisLibrary() { };

/**
 * Displays cool text in About dialog, not required to work
 */
extern "C" __declspec(dllexport) const char* GetPluginString()
{
	return "afhook.rpo\r\n\tPlugin for those who can't read moon runes yet\r\n\t(c)2012-2013 me2legion@gmail.com\r\n\r\n";
};