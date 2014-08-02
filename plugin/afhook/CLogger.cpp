#include "CLogger.h"

CFileLogger::~CFileLogger()
{
	m_stream << std::endl;

	WriteTime();
	m_stream << " AFHook terminated" << std::endl << "----------------------------------" << std::endl;

	m_stream.close();
}

void CFileLogger::Open(const std::wstring& name)
{
	m_stream.open(name, std::ios::out | std::ios::app);
	
	WriteTime();
	WriteText(" AFHook loaded");
}

void CFileLogger::WriteTime()
{
	int size = GetTimeFormatA(LOCALE_CUSTOM_DEFAULT, NULL, NULL, NULL, NULL, 0);
	char* time = new char[size];
	GetTimeFormatA(LOCALE_CUSTOM_DEFAULT, NULL, NULL, NULL, time, size);

	m_stream << time;

	delete[] time;
}

CFileLogger& CFileLogger::WriteLine(const std::string& stuff)
{
	m_stream << std::endl;

	WriteTime();

	m_stream << " " << stuff;

	return *this;
}

CFileLogger& CFileLogger::WritePointer(UINT_PTR pointer)
{
	m_stream << "0x";

	m_stream.setf(std::ios::hex, std::ios::basefield);
	m_stream.setf(std::ios::uppercase);

	m_stream << pointer;

	m_stream.setf(std::ios::dec, std::ios::basefield);
	m_stream.setf(0, std::ios::uppercase);

	return *this;
}

CFileLogger& CFileLogger::WriteText(const std::string& stuff)
{
	m_stream << stuff;

	return *this;
}

CEmptyLogger& CEmptyLogger::WriteLine(const std::string& stuff)
{
	return *this;
}

CEmptyLogger& CEmptyLogger::WritePointer(UINT_PTR pointer)
{
	return *this;
}

CEmptyLogger& CEmptyLogger::WriteText(const std::string& stuff)
{
	return *this;
}