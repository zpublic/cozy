#ifndef __KPathParser_h__
#define __KPathParser_h__

#include <vector>

template <typename T>
class KPathParser
{
public:
	~KPathParser();

	size_t Size(void);

	const T* Get(size_t n);

	BOOL Parse(const T* szPath);

protected:
	void Clear(void);

private:
	std::vector<T*>	m_vecFolders;
};

template <typename T>
KPathParser<T>::~KPathParser()
{
	this->Clear();	
}

template <typename T>
size_t KPathParser<T>::Size(void)
{
	return m_vecFolders.size();
}

template <typename T>
const T* KPathParser<T>::Get(size_t n)
{
	if (n < m_vecFolders.size())
	{
		return m_vecFolders[n];
	}
	return NULL;
}


template <typename T>
BOOL KPathParser<T>::Parse(const T* szPath)
{
	BOOL bReturn = FALSE;
	int i = 0, j = 0;
	T tSeperate = '\\', tSeperate1 = '/';
	T* pTempBuf = NULL;
	int nSize = 0;

	this->Clear();
	if (!szPath) return FALSE;

	for (i = 0; szPath[i] != 0; i++)
	{
		for (j = 0; szPath[i+j] != tSeperate && szPath[i+j] != tSeperate1 && szPath[i+j] != 0; ++j)
		{	
		}

		if (j == 0) goto Exit0;

		nSize = (j + 1) * sizeof(T);
		pTempBuf = new T[nSize];
		memcpy((void*)pTempBuf, (void*)&szPath[i], nSize);
		pTempBuf[j] = 0;
		m_vecFolders.push_back(pTempBuf);
		if (szPath[i+j] == 0) break;
		else i += j;
	}

	bReturn = TRUE;

Exit0:
	return bReturn;
}

template <typename T>
void KPathParser<T>::Clear(void)
{
	T* pTemp = NULL;
	std::vector<T*>::iterator iter;
	for (iter = m_vecFolders.begin(); iter != m_vecFolders.end(); ++iter)
	{
		pTemp = *iter;
		delete pTemp;
	}
	m_vecFolders.clear();
}

typedef KPathParser<char> KPathParserA;
typedef KPathParser<wchar_t> KPathParserW;

#endif