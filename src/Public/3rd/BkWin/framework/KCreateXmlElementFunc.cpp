#include "StdAfx.h"
#include "KCreateXmlElementFunc.h"

KCreateXmlElementFunc::KCreateXmlElementFunc( KTinyXml& tinyXml) : m_TinyXml(tinyXml)
{
}

KCreateXmlElementFunc::~KCreateXmlElementFunc(void)
{
}

BOOL KCreateXmlElementFunc::AddTinyChild(
	  LPCSTR szName, 
	  int nCtrlID /*= 0*/, 
	  LPCSTR szPos/*="0,0,0,0"*/,
	  LPCSTR szShow /*=NULL*/, 
	  LPCSTR szSkin/*= NULL*/, 
	  LPCSTR szClass/*= NULL*/, 
	  LPCSTR szOnlydrawchild/*= NULL*/)
{
	BOOL bReturn = FALSE;

	bReturn = m_TinyXml.AddChild(szName, TRUE);
	if (FALSE == bReturn) goto Exit0;

	bReturn = m_TinyXml.Write("id", nCtrlID);
	if (FALSE == bReturn) goto Exit0;

	if (szShow)
	{
		bReturn = m_TinyXml.Write("show", szShow);
		if (FALSE == bReturn) goto Exit0;
	}

	if (szPos)
	{
		bReturn = m_TinyXml.Write("pos", szPos);
		if (FALSE == bReturn) goto Exit0;
	}

	if (szSkin)
	{
		bReturn = m_TinyXml.Write("skin", szSkin);
		if (FALSE == bReturn) goto Exit0;
	}

	if (szClass)
	{
		bReturn = m_TinyXml.Write("class", szClass);
		if (FALSE == bReturn) goto Exit0;
	}

	if (szOnlydrawchild)
	{
		bReturn = m_TinyXml.Write("onlydrawchild", szOnlydrawchild);
		if (FALSE == bReturn) goto Exit0;
	}

Exit0:
	return bReturn;
}

BOOL KCreateXmlElementFunc::AddTinySibling(
	LPCSTR szName, 
	int nCtrlID /*= 0*/,
	LPCSTR szPos /*="0,0,0,0"*/,
	LPCSTR szShow /*= NULL*/, 
	LPCSTR szSkin/*= NULL*/, 
	LPCSTR szClass/*= NULL*/,
	LPCSTR szOnlydrawchild/*= NULL*/)
{
	BOOL bReturn = FALSE;

	if (FALSE == m_TinyXml.ParentElement())
	{
		goto Exit0;
	}

	bReturn = AddTinyChild(szName, nCtrlID, szPos, szShow, szSkin, szClass, szOnlydrawchild);

Exit0:
	return bReturn;
}