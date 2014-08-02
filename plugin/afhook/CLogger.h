#pragma once

#include "targetver.h"
#include <windows.h>

#include <string>
#include <fstream>
#include <iostream>

class CLogger {
public:
	virtual CLogger& WriteLine(const std::string& stuff) = 0;
	virtual CLogger& WritePointer(UINT_PTR pointer) = 0;
	virtual CLogger& WriteText(const std::string& stuff) = 0;

	virtual void Open(const std::wstring& name) = 0;
};

class CFileLogger : public CLogger {
public:
	~CFileLogger();

	virtual CFileLogger& WriteLine(const std::string& stuff);
	virtual CFileLogger& WritePointer(UINT_PTR pointer);
	virtual CFileLogger& WriteText(const std::string& stuff);

	virtual void Open(const std::wstring& name);

private:
	void WriteTime();

	std::ofstream m_stream;
};

class CEmptyLogger : public CLogger {
	virtual CEmptyLogger& WriteLine(const std::string& stuff);
	virtual CEmptyLogger& WritePointer(UINT_PTR pointer);
	virtual CEmptyLogger& WriteText(const std::string& stuff);
};