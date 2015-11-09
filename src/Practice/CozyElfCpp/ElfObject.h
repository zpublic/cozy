#ifndef __COZY_ELF_OBJECT__
#define __COZY_ELF_OBJECT__

#include "CozyDef.h"
#include "ElfStructs.h"
#include "windows.h"

namespace CozyElf
{
    class COZY_API ElfObject
    {
    public:
        ElfObject();

    // 禁止在堆上构造 与Release配合
    private:
        ~ElfObject() = default;

    public:
        // 初始化
        bool Init(LPCTSTR pszFilename);

        // 释放
        void Release();

    public:
        Elf32_Ehdr* GetElfHeader();
        Elf32_Phdr* GetSegmentTable(size_t* pNum);
        Elf32_Shdr* GetSectionTable(size_t* pNum);

        int32_t GetEntryPoint() const;
        DWORD GetFileSize() const;
        const char* GetString(Elf32_Off offset) const;
        LPCTSTR GetFileName() const;

    public:
        void SaveElfHeader();
        void SaveSegmentTable();
        void SaveSectionTable();
        void SaveStringTable();

    public:
        int32_t SectionToFile(DWORD dwIndex) const;
        int32_t FileToSection(DWORD dwOffset) const;

    private:
        bool TryRead();
        void Clear();
        void InitStringTable();
        void SaveToFile(LPVOID lpDest, LPCVOID lpSrc, DWORD dwLength);

    private:
        Elf32_Ehdr      m_stElfHdr;
        LPCTSTR         m_pszFilename;
        Elf32_Phdr*     m_pSegmentTbl;
        Elf32_Shdr*     m_pSectionTbl;
        char*           m_pStringTable;
        char*           m_rawData;
        DWORD           m_dwFileSize;
        DWORD           m_dwSegmentNum;
        DWORD           m_dwSectionNum;
        HANDLE          m_hFile;
        HANDLE          m_hFileMapping;
    };
}

#endif // __COZY_ELF_OBJECT__