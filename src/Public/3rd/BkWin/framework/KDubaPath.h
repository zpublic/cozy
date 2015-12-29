#ifndef _KDUBAPATH_H_
#define _KDUBAPATH_H_
#include <atlstr.h>

class KDubaPath
{
public:
	static CString	GetModuleFullPath(HMODULE hModule);
	static CString	GetDubaInstallPathFromReg(); //在64位进程下获取失败，谨慎！！！
	static CString	GetPlatformLang(void);
	static CString	GetModuleFolder(HMODULE hModule);
	static CString  GetKavAppDataPath(HMODULE hModule);

	static CString	PathToFileName(LPCTSTR szFullPath);
	static CString	PathToFolderPath(LPCTSTR szFullPath);
	static CString	PathToFolderPath2(LPCTSTR szFullPath);
	static CString	FileNameRemoveSuffix(LPCTSTR szFileName);
	static CString	PathToSuffix(LPCTSTR szFullPath);
	static CString	PathToSuffix2(LPCTSTR szFullPath);
	static CString	PathRemoveSuffix(LPCTSTR szFullPath);

	static CString	GetParsentFolder(LPCTSTR szFullPath);
	static CString	GetRootPath(LPCTSTR szFullPath);
	static CString	LegalizePath(const CString &strPath);

	static void	PathAddBackslash(CString& strPath);
	static void PathRemoveBackslash(CString &strPath);
	static void	PathRemoveFileSpec(CString& strPath);

	static BOOL		CreateDeepDirectory(LPCTSTR szPath);
	static BOOL		DeleteDirectory(LPCTSTR szPath, BOOL bContinueWhenFail);
};

#endif