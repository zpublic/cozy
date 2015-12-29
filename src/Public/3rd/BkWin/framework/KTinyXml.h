#ifndef __KTinyXml_h__
#define __KTinyXml_h__

#include <atlstr.h>
#include "..\tinyxml\tinyxml.h"
#include "KPathParser.h"
#include <string>

class KTinyXml
{
public:
	KTinyXml();

	BOOL LoadFile(IN LPCSTR szXmlFile);
	BOOL LoadFile(IN LPCWSTR szXmlFile);
	BOOL SaveFile(IN LPCSTR szXmlFile);
	BOOL SaveFile(IN LPCWSTR szXmlFile);
	BOOL Parse(IN LPCSTR szXml);
	BOOL Parse(IN LPCSTR szXml, TiXmlEncoding encoding);
	void Clear(void);

	BOOL Open(IN LPCSTR szPath, BOOL bCreate = FALSE);
	BOOL OpenSubPath(IN LPCSTR szSubPath, BOOL bCreate = FALSE);

	BOOL Write(IN LPCSTR szKey, IN int nValue);
	BOOL Write(IN LPCSTR szKey, IN LPCSTR szValue);
	BOOL Write(IN LPCSTR szKey, IN LPCWSTR szValue);
	BOOL Write(IN LPCSTR szKey, IN const std::string& strValue);
	BOOL Write(IN LPCSTR szKey, IN const std::wstring& strValue);
	BOOL WriteText(IN LPCSTR szText, BOOL bCDATA = FALSE);
	BOOL WriteText(IN LPCWSTR szText, BOOL bCDATA = FALSE);

	BOOL Read(IN LPCSTR szKey, OUT int& nValue);
	BOOL Read(IN LPCSTR szKey, OUT CStringA& strValue);
	BOOL Read(IN LPCSTR szKey, OUT CStringW& strValue);
	BOOL Read(IN LPCSTR szKey, OUT std::string& strValue);
	BOOL Read(IN LPCSTR szKey, OUT std::wstring& strValue);
	BOOL ReadText(OUT CStringA& strText);
	BOOL ReadText(OUT CStringW& strText);
	BOOL ReadText(OUT std::string& strText);
	BOOL ReadText(OUT std::wstring& strText);
	INT  ReadInt(void); 
	CStringA ToXml(void);

	void RootElement(void);
	BOOL ParentElement(void);
	BOOL FirstChild(LPCSTR szName = NULL);
	BOOL NextSibling(LPCSTR szName = NULL);
	
	BOOL AddSibling(LPCSTR szName, BOOL bOpen = FALSE);
	BOOL AddChild(LPCSTR szName, BOOL bOpen = FALSE);

	BOOL DeleteCurrentElement(void);
	BOOL DeleteCurrentElement(LPCSTR szNextSiblingName);
	TiXmlElement* GetElement(){return m_pOpenElement; }

protected:
	TiXmlElement* _Open(TiXmlElement* pParentElement, LPCSTR szPath, BOOL bCreate);
	TiXmlElement* _Create(TiXmlNode* pParentNode, KPathParserA& path, int nPathIndex);
	BOOL _LoadFile(LPCWSTR szPath, CStringA& strXml);

	friend class KTinyXmlRemember; 
	void SetElement(TiXmlElement* pOpenElement)
	{
		m_pOpenElement = pOpenElement; 
	}

private:
	TiXmlDocument m_xmlDoc;
	TiXmlElement* m_pOpenElement;
};


/*
@class  KTinyXmlRemember
@brief  用于在一个作用域内不改变当前打开的元素
@*/
class KTinyXmlRemember
{
public:
	KTinyXmlRemember(KTinyXml& ktinyXml) : m_ktinyXml(ktinyXml)
	{
		m_pOpenElement = m_ktinyXml.GetElement(); 
	}

	~KTinyXmlRemember(void)
	{
		m_ktinyXml.SetElement(m_pOpenElement); 
	}

private:
	KTinyXml&     m_ktinyXml; 
	TiXmlElement* m_pOpenElement; 
};

#define KTinyXmlRememberPos(ktinyXml) \
	KTinyXmlRemember __$$_##ktinyXml##__(ktinyXml)

#endif

// -------------------------------------------------------------------------
// $Log: $ dingping 2012-10-28
// 增加 KTinyXmlRemember类，用于在一个作用域内不改变当前打开的元素
