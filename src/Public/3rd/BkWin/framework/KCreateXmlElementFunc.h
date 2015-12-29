/* -----------------------------------------------------------------------
 *  File		:	KCreateXmlElementFunc.h
 *  Author	    :	hexujun
 *  Datet   	:	2013/8/6 11:48
 *  Brief		:	创建适应贝壳库的Xml Element元素
 *
 * ----------------------------------------------------------------------
 * ----------------------------------------------------------------------*/

#ifndef __KCreateXmlElementFunc__h__
#define __KCreateXmlElementFunc__h__
//
///////////////////////////////////////////////////////////////////////////

#include "framework\KTinyXml.h"

class KCreateXmlElementFunc
{
public:

	KCreateXmlElementFunc(KTinyXml& tinyXml);
	~KCreateXmlElementFunc(void);

	KTinyXml& operator()()
	{
		return m_TinyXml;
	}

	BOOL AddTinyChild(
		LPCSTR szName, 
		int  nCtrlID = 0, 
		LPCSTR szPos="0,0,0,0",
		LPCSTR szShow = NULL,
		LPCSTR szSkin = NULL, 
		LPCSTR szClass= NULL, 
		LPCSTR szOnlydrawchild = NULL);

	BOOL AddTinySibling(
		LPCSTR szName, 
		int	   nCtrlID = 0, 
		LPCSTR szPos="0,0,0,0",
		LPCSTR szShow = NULL,
		LPCSTR szSkin= NULL, 
		LPCSTR szClass= NULL, 
		LPCSTR szOnlydrawchild = NULL);

private:

	KTinyXml&	m_TinyXml;
};


///////////////////////////////////////////////////////////////////////////
//
#endif // __KCreateXmlElementFunc__h__

