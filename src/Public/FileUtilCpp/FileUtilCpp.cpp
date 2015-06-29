// FileUtilCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "FileUtilCpp.h"


// This is an example of an exported variable
FILEUTILCPP_API int nFileUtilCpp=0;

// This is an example of an exported function.
FILEUTILCPP_API int fnFileUtilCpp(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see FileUtilCpp.h for the class definition
CFileUtilCpp::CFileUtilCpp()
{
	return;
}
