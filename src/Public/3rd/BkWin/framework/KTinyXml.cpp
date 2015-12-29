#include "stdafx.h"
#include "KTinyXml.h"
#include "kutfconv.h"

KTinyXml::KTinyXml()
{
	m_pOpenElement = NULL;
}

BOOL KTinyXml::LoadFile(IN LPCSTR szXmlFile)
{
	m_xmlDoc.Clear();
	m_xmlDoc.LoadFile(szXmlFile);
	if (m_xmlDoc.Error()) return FALSE;

	return TRUE;
}

BOOL KTinyXml::LoadFile(IN LPCWSTR szXmlFile)
{
	BOOL bReturn = FALSE;
	BOOL bRetCode = FALSE;
	CStringA strXml;

	m_xmlDoc.Clear();

	bRetCode = _LoadFile(szXmlFile, strXml);
	if (!bRetCode || strXml.IsEmpty())
		goto Exit0;

	m_xmlDoc.Parse(strXml);
	if (m_xmlDoc.Error()) 
		goto Exit0;

	bReturn = TRUE;
Exit0:
	return bReturn;
}

BOOL KTinyXml::SaveFile(IN LPCSTR szXmlFile)
{
	return m_xmlDoc.SaveFile(szXmlFile);
}

BOOL KTinyXml::SaveFile(IN LPCWSTR szXmlFile)
{
	return m_xmlDoc.SaveFile(CW2A(szXmlFile));
}

BOOL KTinyXml::Parse(IN LPCSTR szXml)
{
	return Parse(szXml, TIXML_DEFAULT_ENCODING);
}

BOOL KTinyXml::Parse(IN LPCSTR szXml, TiXmlEncoding encoding)
{
	m_xmlDoc.Clear();
	m_xmlDoc.Parse(szXml, NULL, encoding);
	if (m_xmlDoc.Error()) return FALSE;

	return TRUE;
}

void KTinyXml::Clear(void)
{
	m_xmlDoc.Clear();
}

BOOL KTinyXml::Open(IN LPCSTR szPath, BOOL bCreate /* = FALSE */)
{
	if (!szPath || !szPath[0])
	{
		m_pOpenElement = m_xmlDoc.RootElement();
	}
	else
	{
		m_pOpenElement = _Open(NULL, szPath, bCreate);
	}

	return m_pOpenElement != NULL;
}

BOOL KTinyXml::OpenSubPath(IN LPCSTR szSubPath, BOOL bCreate /* = FALSE */)
{
	if (!szSubPath || !szSubPath[0])
		return FALSE;

	m_pOpenElement = _Open(m_pOpenElement, szSubPath, bCreate);

	return m_pOpenElement != NULL;
}

TiXmlElement* KTinyXml::_Open(TiXmlElement* pParentElement, LPCSTR szPath, BOOL bCreate)
{
	KPathParserA path;
	BOOL bRetCode = FALSE;
	int nPathIndex = 0;
	TiXmlElement* pRetElement = NULL;
	TiXmlDocument* pDocument = NULL;
	TiXmlElement* pTempElement = NULL;

	path.Parse(szPath);
	if (!path.Size()) goto Exit0;

	if (!pParentElement)
	{
		pDocument = m_xmlDoc.GetDocument();
		TiXmlElement* pRootElement = m_xmlDoc.RootElement();
		if (!pRootElement)
		{
			if (!bCreate) goto Exit0;

			pDocument = m_xmlDoc.GetDocument();
			TiXmlDeclaration* pDeclar = new TiXmlDeclaration("1.0", "UTF-8", "no");
			pDocument->LinkEndChild(pDeclar);
			pRetElement = _Create(pDocument, path, 0);
			goto Exit0;
		}
		else
		{
			if (strcmp(pRootElement->Value(), path.Get(0)) == 0)
			{
				nPathIndex = 1;
				pParentElement = pRootElement;
			}
			else
				goto Exit0;
		}
	}
	
	do 
	{
		if (nPathIndex == path.Size())
			break;

		pTempElement = pParentElement->FirstChildElement(path.Get(nPathIndex));
		if (!pTempElement)
		{
			if (!bCreate) goto Exit0;

			pRetElement = _Create(pParentElement, path, nPathIndex);
			goto Exit0;
		}
		
		nPathIndex++;
		pParentElement = pTempElement;

	} while (true);

	pRetElement = pParentElement;

Exit0:
	return pRetElement;
}

TiXmlElement* KTinyXml::_Create(TiXmlNode* pParentNode, KPathParserA& path, int nPathIndex)
{
	TiXmlElement* pRetElement = NULL;

	for (size_t i = nPathIndex; i < path.Size(); i++)
	{
		pRetElement = new TiXmlElement(path.Get(i));
		pParentNode->LinkEndChild(pRetElement);
		pParentNode = pRetElement;
	}


	return pRetElement;
}

BOOL KTinyXml::Write(IN LPCSTR szKey, IN int nValue)
{
	if (!m_pOpenElement) return FALSE;

	m_pOpenElement->SetAttribute(szKey, nValue);

	return TRUE;
}

BOOL KTinyXml::Write(IN LPCSTR szKey, IN LPCSTR szValue)
{
	if (!m_pOpenElement || !szValue) return FALSE;

	m_pOpenElement->SetAttribute(szKey, szValue);

	return TRUE;
}

BOOL KTinyXml::Write(IN LPCSTR szKey, IN LPCWSTR szValue)
{
	if (!m_pOpenElement || !szValue) return FALSE;

	m_pOpenElement->SetAttribute(szKey, TCharToUTF8(szValue));

	return TRUE;
}

BOOL KTinyXml::Write(IN LPCSTR szKey, IN const std::string& strValue)
{
	if (!m_pOpenElement) return FALSE;

	m_pOpenElement->SetAttribute(szKey, strValue.c_str());

	return TRUE;
}

BOOL KTinyXml::Write(IN LPCSTR szKey, IN const std::wstring& strValue)
{
	if (!m_pOpenElement) return FALSE;

	m_pOpenElement->SetAttribute(szKey, TCharToUTF8(strValue.c_str()));

	return TRUE;
}

BOOL KTinyXml::WriteText(IN LPCSTR szText, BOOL bCDATA /* = FALSE */)
{
	if (!m_pOpenElement || !szText) return FALSE;

	TiXmlText *pXmlText = new TiXmlText(szText);
	pXmlText->SetCDATA(!!bCDATA);
	TiXmlNode* child = m_pOpenElement->FirstChild();
	if ( child ) {
		m_pOpenElement->ReplaceChild(child, *pXmlText);
		delete pXmlText;

	}
	else
		m_pOpenElement->LinkEndChild(pXmlText); 

	return TRUE;
}

BOOL KTinyXml::WriteText(IN LPCWSTR szText, BOOL bCDATA /* = FALSE */)
{
	if (!m_pOpenElement || !szText) return FALSE;

	CStringA strText = TCharToUTF8(szText);
	TiXmlText *pXmlText = new TiXmlText((LPCSTR)strText);
	pXmlText->SetCDATA(!!bCDATA);
	TiXmlNode* child = m_pOpenElement->FirstChild();
	if ( child ) {
		m_pOpenElement->ReplaceChild(child, *pXmlText);
		delete pXmlText;
	}
	else
		m_pOpenElement->LinkEndChild(pXmlText); 
	

	return TRUE;
}

BOOL KTinyXml::Read(IN LPCSTR szKey, OUT int& nValue)
{
	if (!m_pOpenElement) return FALSE;

	int nRetCode = m_pOpenElement->QueryIntAttribute(szKey, &nValue);
	if (nRetCode != TIXML_SUCCESS) return FALSE;

	return TRUE;
}

BOOL KTinyXml::Read(IN LPCSTR szKey, OUT CStringA& strValue)
{
	strValue.Empty();

	if (m_pOpenElement)
	{
		LPCSTR lpszTemp = m_pOpenElement->Attribute(szKey);
		if (lpszTemp)
		{
			strValue = lpszTemp;
			return TRUE;
		}
	}

	return FALSE;
}

BOOL KTinyXml::Read(IN LPCSTR szKey, OUT CStringW& strValue)
{
	strValue.Empty();

	if (m_pOpenElement)
	{
		LPCSTR lpszTemp = m_pOpenElement->Attribute(szKey);
		if (lpszTemp)
		{
			strValue = UTF8ToTChar(lpszTemp);
			return TRUE;
		}
	}

	return FALSE;
}

BOOL KTinyXml::Read(IN LPCSTR szKey, OUT std::string& strValue)
{
	strValue.clear();

	if (m_pOpenElement)
	{
		LPCSTR lpszTemp = m_pOpenElement->Attribute(szKey);
		if (lpszTemp)
		{
			strValue = lpszTemp;
			return TRUE;
		}
	}

	return FALSE;
}

BOOL KTinyXml::Read(IN LPCSTR szKey, OUT std::wstring& strValue)
{
	strValue.clear();

	if (m_pOpenElement)
	{
		LPCSTR lpszTemp = m_pOpenElement->Attribute(szKey);
		if (lpszTemp)
		{
			strValue = UTF8ToTChar(lpszTemp);
			return TRUE;
		}
	}

	return FALSE;
}

BOOL KTinyXml::ReadText(OUT CStringA& strText)
{
	TiXmlNode* pFirstChild = NULL;

	strText.Empty();
	if (m_pOpenElement)
	{
		LPCSTR lpszTemp = m_pOpenElement->GetText();
		if (lpszTemp)
		{
			strText = lpszTemp;
			return TRUE;
		}
	}

	return FALSE;
}

BOOL KTinyXml::ReadText(OUT CStringW& strText)
{
	TiXmlNode* pFirstChild = NULL;

	strText.Empty();
	if (m_pOpenElement)
	{
		LPCSTR lpszTemp = m_pOpenElement->GetText();
		if (lpszTemp)
		{
			strText = UTF8ToTChar(lpszTemp);
			return TRUE;
		}
	}

	return FALSE;
}

BOOL KTinyXml::ReadText(OUT std::string& strText)
{
	TiXmlNode* pFirstChild = NULL;

	strText.clear();
	if (m_pOpenElement)
	{
		LPCSTR lpszTemp = m_pOpenElement->GetText();
		if (lpszTemp)
		{
			strText = lpszTemp;
			return TRUE;
		}
	}

	return FALSE;
}

BOOL KTinyXml::ReadText(OUT std::wstring& strText)
{
	TiXmlNode* pFirstChild = NULL;

	strText.clear();
	if (m_pOpenElement)
	{
		LPCSTR lpszTemp = m_pOpenElement->GetText();
		if (lpszTemp)
		{
			strText = UTF8ToTChar(lpszTemp);
			return TRUE;
		}
	}

	return FALSE;
}

INT KTinyXml::ReadInt( void )
{
	CString strText; 
	ReadText(strText); 

	return _wtoi(strText); 
}

CStringA KTinyXml::ToXml(void)
{
	TiXmlPrinter  xmlPrinter;
	m_xmlDoc.Accept(&xmlPrinter);
	return xmlPrinter.CStr();
}

void KTinyXml::RootElement(void)
{
	m_pOpenElement = m_xmlDoc.RootElement();
}

BOOL KTinyXml::ParentElement(void)
{
	TiXmlElement* pParentElement = (TiXmlElement*)m_pOpenElement->Parent();
	if (pParentElement)
	{
		m_pOpenElement = pParentElement;
		return TRUE;
	}

	return FALSE;
}

BOOL KTinyXml::FirstChild(LPCSTR szName /* = NULL */)
{
	TiXmlElement* pChildElement = NULL;

	if (m_pOpenElement == NULL)
		return FALSE;

	if (!szName || !szName[0])
	{
		pChildElement = m_pOpenElement->FirstChildElement();
	}
	else
	{
		pChildElement = m_pOpenElement->FirstChildElement(szName);
	}

	if (pChildElement)
	{
		m_pOpenElement = pChildElement;
		return TRUE;
	}
	return FALSE;
}

BOOL KTinyXml::NextSibling(LPCSTR szName /* = NULL */)
{
	TiXmlElement* pSiblingElement = NULL;

	if (!szName || !szName[0])
	{
		pSiblingElement = m_pOpenElement->NextSiblingElement();
	}
	else
	{
		pSiblingElement = m_pOpenElement->NextSiblingElement(szName);
	}
	
	if (pSiblingElement)
	{
		m_pOpenElement = pSiblingElement;
		return TRUE;
	}
	return FALSE;
}

BOOL KTinyXml::AddSibling(LPCSTR szName, BOOL bOpen /* = FALSE; */)
{
	TiXmlNode* pParentNode = m_pOpenElement->Parent();
	if (!pParentNode) return FALSE;
	
	TiXmlElement* pNewElement = NULL; 
	if (!szName || !szName[0])
		pNewElement = new TiXmlElement(m_pOpenElement->Value());
	else
		pNewElement = new TiXmlElement(szName);
	
	pParentNode->LinkEndChild(pNewElement);
	if (bOpen)
		m_pOpenElement = pNewElement;
	
	return TRUE;
}

BOOL KTinyXml::AddChild(LPCSTR szName, BOOL bOpen /* = FALSE */)
{
	if (!szName || !szName[0])
		return FALSE;
	
	TiXmlElement* pElement = new TiXmlElement(szName);
	m_pOpenElement->LinkEndChild(pElement);
	if (bOpen)
		m_pOpenElement = pElement;
	
	return TRUE;
}

BOOL KTinyXml::DeleteCurrentElement(void)
{
	TiXmlElement* pParentElement = (TiXmlElement*)m_pOpenElement->Parent();
	if (pParentElement)
	{
		pParentElement->RemoveChild(m_pOpenElement);
		m_pOpenElement = pParentElement;
		return TRUE;
	}
	return FALSE;
}

BOOL KTinyXml::DeleteCurrentElement(LPCSTR szNextSiblingName)
{
	TiXmlElement* pParentElement = (TiXmlElement*)m_pOpenElement->Parent();
	if (pParentElement)
	{
		TiXmlElement* pSiblingElement = NULL;

		if (!szNextSiblingName || !szNextSiblingName[0])
			pSiblingElement = m_pOpenElement->NextSiblingElement();
		else
			pSiblingElement = m_pOpenElement->NextSiblingElement(szNextSiblingName);

		pParentElement->RemoveChild(m_pOpenElement);

		if (pSiblingElement)
		{
			m_pOpenElement = pSiblingElement;
			return TRUE;
		}

		return FALSE;
	}
	return FALSE;
}

BOOL KTinyXml::_LoadFile(LPCWSTR szPath, CStringA& strXml)
{
	BOOL bReturn = FALSE;
	BOOL bRetCode = FALSE;
	HANDLE hFile = INVALID_HANDLE_VALUE;

	strXml.Empty();

	hFile = ::CreateFile(szPath, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile == INVALID_HANDLE_VALUE) goto Exit0;

	DWORD dwFileSize = ::GetFileSize(hFile, NULL);
	if (dwFileSize > 2 * 1024 * 1024)
		goto Exit0;

	DWORD dwNumReaded = 0;
	DWORD dwReadSize = dwFileSize;
	PSTR pBuffer = strXml.GetBuffer(dwFileSize + 1);
	PSTR pOffset = pBuffer;
	do 
	{
		bRetCode = ::ReadFile(hFile, pOffset, dwReadSize, &dwNumReaded, NULL);
		if (!bRetCode || !dwNumReaded) goto Exit0;

		dwReadSize -= dwNumReaded;
		pOffset += dwNumReaded;

	} while (dwReadSize);

	pBuffer[dwFileSize] = 0;
	strXml.ReleaseBuffer();

	bReturn = TRUE;
Exit0:
	if (hFile)
		CloseHandle(hFile);

	return bReturn;
}