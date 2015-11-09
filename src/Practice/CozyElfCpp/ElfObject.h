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
        bool Init(const char* filename);

        // 释放
        void Release();

    public:
        Elf32_Ehdr* GetElfHeader();
        Elf32_Phdr* GetSegmentTable(size_t* num);
        Elf32_Shdr* GetSectionTable(size_t* num);

        int32_t GetEntryPoint() const;
        uint32_t GetFileSize() const;
        const char* GetString(Elf32_Off offset) const;
        const char* GetFileName() const;

    public:
        void SaveElfHeader();
        void SaveSegmentTable();
        void SaveSectionTable();
        void SaveStringTable();

    public:
        int32_t SectionToFile(uint32_t index) const;
        int32_t FileToSection(uint32_t offset) const;

    private:
        bool TryRead();
        void Clear();
        void InitStringTable();
        void SaveToFile(const void* src, uint32_t offset, uint32_t length);

    private:
        Elf32_Ehdr          m_elf_header;
        const char*         m_filename;
        Elf32_Phdr*         m_segment_table;
        Elf32_Shdr*         m_section_table;
        std::FILE*          m_file;
        char*               m_string_table;
        uint32_t            m_file_size;
        uint32_t            m_segment_num;
        uint32_t            m_section_num;
    };
}

#endif // __COZY_ELF_OBJECT__