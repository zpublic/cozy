#ifndef __COZY_ELF_OBJECT__
#define __COZY_ELF_OBJECT__

#include "CozyDef.h"
#include "ElfStructs.h"
#include <cstdio>

namespace CozyElf
{
    class ElfObject
    {
    public:
        ElfObject();

    // 禁止在堆上构造 与Release配合
    private:
        ~ElfObject() = default;

    public:
        // 初始化
        bool Init(const zl_char* filename);

        // 释放
        void Release();

    public:
        Elf32_Ehdr* GetElfHeader();
        Elf32_Phdr* GetSegmentTable(zl_int32* num);
        Elf32_Shdr* GetSectionTable(zl_int32* num);

        Elf32_Addr GetEntryPoint() const;
        zl_uint32 GetFileSize() const;
        const char* GetString(Elf32_Off offset) const;
        const char* GetFileName() const;

    public:
        void SaveElfHeader();
        void SaveSegmentTable();
        void SaveSectionTable();
        void SaveStringTable();

    public:
        zl_int32 SectionToFile(zl_uint32 index) const;
        zl_int32 FileToSection(zl_uint32 offset) const;

    private:
        bool TryRead();
        void Clear();
        void InitStringTable();
        void SaveToFile(const void* src, zl_uint32 offset, zl_uint32 length);

    private:
        Elf32_Ehdr          m_elf_header;
        const zl_char*      m_filename;
        Elf32_Phdr*         m_segment_table;
        Elf32_Shdr*         m_section_table;
        std::FILE*          m_file;
        zl_char*            m_string_table;
        zl_uint32           m_file_size;
        zl_uint16           m_segment_num;
        zl_uint16           m_section_num;
    };
}

#endif // __COZY_ELF_OBJECT__