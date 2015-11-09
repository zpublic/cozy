#include "ElfObject.h"
#include <cstring>
#include "CozyEnum.h"

using namespace CozyElf;

unsigned char COZY_API DefaultMagicNumber[] = { 0x7f, 0x45, 0x4c, 0x46 };

ElfObject::ElfObject()
    :m_pSegmentTbl(nullptr), m_pSectionTbl(nullptr), m_pszFilename(nullptr), m_pStringTable(nullptr), m_rawData(nullptr),
    m_hFile(INVALID_HANDLE_VALUE), m_hFileMapping(INVALID_HANDLE_VALUE)
{
    Clear();
}

bool ElfObject::Init(LPCTSTR pszFilename)
{
    Clear();

    m_hFile = ::CreateFile(pszFilename, GENERIC_READ | GENERIC_WRITE, 0, nullptr, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (m_hFile != INVALID_HANDLE_VALUE)
    {
        if (TryRead())
        {
            m_dwFileSize        = ::GetFileSize(m_hFile, nullptr);
            m_hFileMapping      = ::CreateFileMapping(m_hFile, nullptr, PAGE_READWRITE, 0, m_dwFileSize, nullptr);

            if (m_hFileMapping != nullptr)
            {
                // 映射文件到地址空间
                m_rawData = reinterpret_cast<char*>(::MapViewOfFile(m_hFileMapping, FILE_MAP_ALL_ACCESS, 0, 0, m_dwFileSize));
                if (m_rawData == nullptr)
                {
                    return false;
                }

                // 初始化Elf头结构
                ::CopyMemory(&m_stElfHdr, m_rawData, sizeof(Elf32_Ehdr));

                // 初始化SegmentTable
                m_dwSegmentNum      = static_cast<DWORD>(m_stElfHdr.e_phnum);
                m_pSegmentTbl       = new Elf32_Phdr[m_dwSegmentNum];
                ::CopyMemory(m_pSegmentTbl, m_rawData + m_stElfHdr.e_phoff, sizeof(Elf32_Phdr) * m_dwSegmentNum);

                // 初始化SectionTable
                m_dwSectionNum      = static_cast<DWORD>(m_stElfHdr.e_shnum);
                m_pSectionTbl       = new Elf32_Shdr[m_dwSectionNum];
                ::CopyMemory(m_pSectionTbl, m_rawData + m_stElfHdr.e_shoff, sizeof(Elf32_Shdr) * m_dwSectionNum);

                // 初始化字符串表
                InitStringTable();

                m_pszFilename = pszFilename;

                return true;
            }
        }
        
    }
    return false;
}

bool ElfObject::TryRead()
{
    if (m_hFile != INVALID_HANDLE_VALUE)
    {
        ::SetFilePointer(m_hFile, 0, 0, FILE_BEGIN);

        DWORD dwRead = 0;
        unsigned char magic_number_and_other[6];

        ::ReadFile(m_hFile, magic_number_and_other, 6, &dwRead, nullptr);

        if (dwRead != 6) return false;
        for (int i = 0; i < 4; ++i)
        {
            if (magic_number_and_other[i] != DefaultMagicNumber[i])
            {
                return false;
            }
        }

        if (static_cast<ElfClass>(magic_number_and_other[4]) != ElfClass::X86)
        {
            return false;
        }

        if (static_cast<ElfEndianess>(magic_number_and_other[5]) != ElfEndianess::LittleEndian)
        {
            return false;
        }

        return true;
    }
    return false;
}

void ElfObject::Clear()
{
    m_dwSegmentNum    = 0;
    m_dwSectionNum    = 0;
    m_dwFileSize      = 0;

    if (m_rawData != nullptr)
    {
        ::UnmapViewOfFile(m_rawData);
        m_rawData = nullptr;
    }

    SAFE_DELETE_ARRAY(m_pSegmentTbl);
    SAFE_DELETE_ARRAY(m_pSectionTbl);
    SAFE_DELETE_ARRAY(m_pStringTable);

    SAFE_CLOSE(m_hFileMapping);
    SAFE_CLOSE(m_hFile);

    ::ZeroMemory(&m_stElfHdr, sizeof(m_stElfHdr));
}

void ElfObject::Release()
{
    Clear();
    delete this;
}

Elf32_Ehdr* ElfObject::GetElfHeader()
{
    if (m_pszFilename == nullptr) return nullptr;

    return &m_stElfHdr;
}

Elf32_Phdr* ElfObject::GetSegmentTable(size_t* pNum)
{
    if (m_pszFilename == nullptr) return nullptr;
    if (pNum != nullptr)
    {
        *pNum = m_dwSegmentNum;
    }
    return m_pSegmentTbl;
}

Elf32_Shdr* ElfObject::GetSectionTable(size_t* pNum)
{
    if (m_pszFilename == nullptr) return nullptr;
    if (pNum != nullptr)
    {
        *pNum = m_dwSectionNum;
    }
    return m_pSectionTbl;
}

int32_t ElfObject::GetEntryPoint() const
{
    if (m_pszFilename == nullptr) return -1;
    return m_stElfHdr.e_entry;
}

const char* ElfObject::GetString(Elf32_Off offset) const
{
    if (m_pszFilename == nullptr || m_pStringTable == nullptr) return nullptr;
    return m_pStringTable + offset;
}

void ElfObject::InitStringTable()
{
    DWORD nStroff   = m_pSectionTbl[m_stElfHdr.e_shstrndx].sh_offset;
    DWORD nLength   = m_pSectionTbl[m_stElfHdr.e_shstrndx].sh_size;

    m_pStringTable  = new char[nLength];
    ::CopyMemory(m_pStringTable, m_rawData + nStroff, nLength);
}

void ElfObject::SaveElfHeader()
{
    if (m_hFile != INVALID_HANDLE_VALUE && m_hFileMapping != INVALID_HANDLE_VALUE && m_rawData != nullptr)
    {
        ::CopyMemory(m_rawData, &m_stElfHdr, sizeof(m_stElfHdr));
        ::FlushViewOfFile(m_rawData, sizeof(m_stElfHdr));
    }
}

void ElfObject::SaveSegmentTable()
{
    if (m_hFile != INVALID_HANDLE_VALUE && m_hFileMapping != INVALID_HANDLE_VALUE && m_pSegmentTbl != nullptr)
    {
        DWORD nOffset   = m_stElfHdr.e_phoff;
        DWORD dwLength  = sizeof(Elf32_Phdr) * m_dwSegmentNum;

        SaveToFile(m_rawData + nOffset, m_pSegmentTbl, dwLength);
    }
}

void ElfObject::SaveSectionTable()
{
    if (m_hFile != INVALID_HANDLE_VALUE && m_hFileMapping != INVALID_HANDLE_VALUE && m_pSectionTbl != nullptr)
    {
        DWORD nOffset   = m_stElfHdr.e_shoff;
        DWORD dwLength  = sizeof(Elf32_Shdr) * m_dwSectionNum;

        SaveToFile(m_rawData + nOffset, m_pSectionTbl, dwLength);
    }
}

void ElfObject::SaveStringTable()
{
    if (m_hFile != INVALID_HANDLE_VALUE && m_hFileMapping != INVALID_HANDLE_VALUE && m_pStringTable != nullptr)
    {
        DWORD dwStroff = m_pSectionTbl[m_stElfHdr.e_shstrndx].sh_offset;
        DWORD dwLength = m_pSectionTbl[m_stElfHdr.e_shstrndx].sh_size;

        SaveToFile(m_rawData + dwStroff, m_pStringTable, dwLength);
    }
}

LPCTSTR ElfObject::GetFileName() const
{
    return m_pszFilename;
}

DWORD ElfObject::GetFileSize() const
{
    return m_dwFileSize;
}

int32_t ElfObject::SectionToFile(DWORD dwIndex) const
{
    if (dwIndex >= m_dwSectionNum) return -1;

    return m_pSectionTbl[dwIndex].sh_offset;
}

int32_t ElfObject::FileToSection(DWORD dwOffset) const
{
    for (DWORD i = 0; i < m_dwSectionNum; ++i)
    {
        if (dwOffset >= m_pSectionTbl[i].sh_offset && dwOffset < m_pSectionTbl[i].sh_offset + m_pSectionTbl[i].sh_size)
        {
            return i;
        }
    }
    return -1;
}

void ElfObject::SaveToFile(LPVOID lpDest, LPCVOID lpSrc, DWORD dwLength)
{
    ::CopyMemory(lpDest, lpSrc, dwLength);
    ::FlushViewOfFile(lpDest, dwLength);
}