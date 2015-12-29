#include "stdafx.h"
#include "KDubaPath.h"
#include <shlobj.h>

CString	KDubaPath::GetModuleFullPath(HMODULE hModule)
{
	DWORD dwRet = 0;
	CString strTemp;
	TCHAR szFileName[MAX_PATH + 1] = {0};

	dwRet = ::GetModuleFileName(hModule, szFileName, MAX_PATH);
	if (!dwRet) goto Exit0;

	strTemp = szFileName;

Exit0:
	return strTemp;
}

CString	KDubaPath::PathToFileName(LPCTSTR szFullPath)
{
	CString strPath(szFullPath);

	strPath.Replace(_T('/'), _T('\\'));
	int nPos = strPath.ReverseFind(_T('\\'));
	if (nPos == -1)
		return strPath;
	else
		return strPath.Right(strPath.GetLength() - nPos - 1);
}

CString	KDubaPath::PathToFolderPath(LPCTSTR szFullPath)
{
	CString strTemp;

	strTemp = szFullPath;
	int nPos = -1;
	nPos = strTemp.ReverseFind(_T('\\'));
	if (-1 == nPos)
	{
		nPos = strTemp.ReverseFind(_T('/'));
	}

	if (-1 == nPos) return _T("");

	return strTemp.Left(nPos + 1);
}

CString	KDubaPath::PathToFolderPath2(LPCTSTR szFullPath)
{
	CString strTemp;

	strTemp = szFullPath;
	int nPos = -1;
	nPos = strTemp.ReverseFind(_T('\\'));
	if (-1 == nPos)
	{
		nPos = strTemp.ReverseFind(_T('/'));
	}

	if (-1 == nPos)
	{
		return strTemp;
	}

	return strTemp.Left(nPos + 1);
}

CString		KDubaPath::GetModuleFolder(HMODULE hModule)
{
	return PathToFolderPath(GetModuleFullPath(hModule));
}

CString	KDubaPath::FileNameRemoveSuffix(LPCTSTR szFileName)
{
	CString strTemp;

	strTemp = szFileName;
	int nPos = -1;
	nPos = strTemp.ReverseFind(_T('.'));

	if (-1 == nPos) 
		return strTemp;

	return strTemp.Left(nPos);
}

CString	KDubaPath::PathToSuffix(LPCTSTR szFullPath)
{
	CString strTemp = PathToFileName(szFullPath);

	int nPos = -1;
	nPos = strTemp.ReverseFind(_T('.'));

	if (-1 == nPos) 
		return strTemp;

	return strTemp.Mid(nPos + 1);
}

CString	KDubaPath::PathToSuffix2(LPCTSTR szFullPath)
{
	CString strTemp = PathToFileName(szFullPath);

	int nPos = -1;
	nPos = strTemp.ReverseFind(_T('.'));

	if (-1 == nPos) 
		return L"";

	return strTemp.Mid(nPos + 1);
}

CString	KDubaPath::PathRemoveSuffix(LPCTSTR szFullPath)
{
	CString strTemp = PathToFileName(szFullPath);

	int nPos = -1;
	nPos = strTemp.ReverseFind(_T('.'));

	if (-1 == nPos) 
		return strTemp;

	return strTemp.Mid(0, nPos);
}

CString		KDubaPath::GetParsentFolder(LPCTSTR szFullPath)
{
	CString strParsentPath(szFullPath);

	PathRemoveBackslash(strParsentPath);
	PathRemoveFileSpec(strParsentPath);
	if (strParsentPath.IsEmpty())
		strParsentPath = szFullPath;

	return strParsentPath;
}

CString	KDubaPath::GetRootPath(LPCTSTR szFullPath)
{
	CString strRoot(szFullPath);
	CString strTempPath(szFullPath);

	int nPos = strTempPath.Find(_T('\\'));
	if (nPos != -1)
		strRoot = strTempPath.Left(nPos);

	return strRoot;
}

CString			KDubaPath::LegalizePath(const CString &strPath)
{
	CString strOriginPath = strPath;
	CString strData(strOriginPath);
	CString strFolderPath;
	CString strDstPath;

	while (1)
	{
		strFolderPath = GetRootPath(strData);
		if (strFolderPath == _T(".."))
			strDstPath = GetParsentFolder(strDstPath);
		else
			strDstPath.Append(strFolderPath);

		PathAddBackslash(strDstPath);
		strData = strData.Mid(strFolderPath.GetLength());
		strData.TrimLeft(L"\\");

		if (strData.IsEmpty())
			break;
	}

	return strDstPath;
}

void			KDubaPath::PathAddBackslash(CString& strPath)
{
	CString strTemp;
	strTemp = strPath.Right(1);

	if (strTemp != _T("\\") && strTemp != _T("/"))
		strPath.Append(_T("\\"));
}

void			KDubaPath::PathRemoveBackslash(CString &strPath)
{
	if (strPath.Right(1) == _T("\\"))
		strPath = strPath.Left(strPath.GetLength() - 1);
}

void			KDubaPath::PathRemoveFileSpec(CString& strPath)
{
	int nPos = strPath.ReverseFind(_T('\\'));
	if (nPos == -1)
	{
		strPath.Empty();
	}
	else
	{
		strPath = strPath.Left(nPos);
	}
}

BOOL		KDubaPath::CreateDeepDirectory(LPCTSTR szPath)
{
	BOOL bRetCode = FALSE;
	CString strPath(szPath);

	if (GetFileAttributes(szPath) != INVALID_FILE_ATTRIBUTES)
		return TRUE;

	bRetCode = ::CreateDirectory(szPath, NULL);
	if (!bRetCode && ::GetLastError() != ERROR_ALREADY_EXISTS)
	{
		PathRemoveFileSpec(strPath);
		if (strPath.IsEmpty()) return FALSE;

		bRetCode = CreateDeepDirectory(strPath);
		if (!bRetCode) return FALSE;

		bRetCode = ::CreateDirectory(szPath, NULL);
		if (!bRetCode && ::GetLastError() != ERROR_ALREADY_EXISTS)
			return FALSE;
	}

	return TRUE;
}

CString	KDubaPath::GetDubaInstallPathFromReg()
{
	CRegKey regKey;

	if (ERROR_SUCCESS != regKey.Open(HKEY_LOCAL_MACHINE, _T("software\\kingsoft\\antivirus"), KEY_READ | KEY_WOW64_32KEY))
	{
		return L"";
	}

	WCHAR szBuffer[MAX_PATH + 1] = {0};
	ULONG uChars = MAX_PATH;

	if (regKey.QueryStringValue(_T("ProgramPath"), szBuffer, &uChars) != ERROR_SUCCESS)
	{
		return L"";
	}

	CString strPath;

	strPath = szBuffer;
	PathAddBackslash(strPath);

	return strPath;
}

CString KDubaPath::GetPlatformLang(void)
{
	CRegKey reg;

	LPCTSTR szKeyLang = _T("Lang");
	LPCTSTR szRegKisCommon = _T("SOFTWARE\\Kingsoft\\KISCommon");

	if (ERROR_SUCCESS != reg.Open(HKEY_LOCAL_MACHINE, szRegKisCommon), KEY_READ | KEY_WOW64_32KEY)
	{
		return L"chs";
	}

	WCHAR szBuffer[MAX_PATH + 1] = {0};
	ULONG uChars = MAX_PATH;

	if (ERROR_SUCCESS != reg.QueryStringValue(szKeyLang, szBuffer, &uChars))
	{
		return L"chs";
	}

	CString strLang = szBuffer;
	strLang.MakeLower();

	if (strLang.IsEmpty()) 
		strLang = _T("chs");

	return strLang;
}

BOOL		KDubaPath::DeleteDirectory(LPCTSTR szDir, BOOL bContinueWhenFail)
{
	BOOL bReturn = FALSE;
	CString sDir;
	CString sFindPath;
	WIN32_FIND_DATA fData;
	HANDLE hFind = INVALID_HANDLE_VALUE;

	sDir = szDir;
	PathAddBackslash(sDir);

	sFindPath.Format(_T("%s*.*"), sDir);

	hFind = ::FindFirstFile(sFindPath, &fData);
	if (hFind == INVALID_HANDLE_VALUE)
		goto Exit0;

	do 
	{
		if (0 == _tcscmp(fData.cFileName, _T(".")) ||
			0 == _tcscmp(fData.cFileName, _T("..")))
			continue;

		if (fData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
		{
			CString sSubfolder;
			sSubfolder.Format(_T("%s%s\\"), sDir, fData.cFileName);
			if ((FALSE == DeleteDirectory(sSubfolder, bContinueWhenFail)) && (!bContinueWhenFail))
				goto Exit0;
		}
		else 
		{
			CString sFileName = fData.cFileName;
			sFileName.MakeLower();
			if ((FALSE == DeleteFile(sDir + sFileName)) && (!bContinueWhenFail))
				goto Exit0;
		}

	} while (::FindNextFile(hFind, &fData) != 0);

	bReturn = TRUE;

Exit0:
	if (hFind != INVALID_HANDLE_VALUE)
		::FindClose(hFind);

	RemoveDirectory(sDir);
	PathRemoveBackslash(sDir);
	RemoveDirectory(sDir);

	return bReturn;
}

CString  KDubaPath::GetKavAppDataPath(HMODULE hModule)
{
	UNREFERENCED_PARAMETER(hModule);

	CString strPath;
	TCHAR szPath[MAX_PATH] = {0};

	if (SHGetSpecialFolderPath(NULL, szPath, CSIDL_COMMON_APPDATA, FALSE))
	{
		strPath = szPath;
		KDubaPath::PathAddBackslash(strPath);
	}
	else
	{
		strPath = KDubaPath::GetModuleFolder((HMODULE)&__ImageBase);
		strPath.Append(_T("appdata\\"));
	}

	if (!strPath.IsEmpty())
	{
		strPath.Append(_T("kingsoft\\kis\\"));

		KDubaPath::CreateDeepDirectory(strPath);
	}

	return strPath;
}