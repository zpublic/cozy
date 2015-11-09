// CozyELFCppTester.cpp : 定义控制台应用程序的入口点。
//

#include <iostream>
#include "../CozyElfCpp/ElfObject.h"

int main()
{
    const zl_char* filename = "D:\\1.so";

    auto obj = new CozyElf::ElfObject();
    std::cout << "Open " << filename;

    if (obj->Init(filename))
    {
        std::cout << " Success !" << std::endl;

        std::cout << "FileSize : " << obj->GetFileSize() << std::endl;
        std::cout << "Entry Point : " << obj->GetEntryPoint() << std::endl;

        zl_int32 num = 0;

        auto ptbl = obj->GetSegmentTable(&num);
        std::cout << "ProgramTable : " << std::endl << "Count : " << num << std::endl;

        for (zl_int32 i = 0; i < num; ++i)
        {
            std::cout << "Virtual Address : " << ptbl[i].p_vaddr << std::endl;
        }

        std::cout << std::endl;

        auto stbl = obj->GetSectionTable(&num);
        std::cout << "SectionTable : " << std::endl << "Count : " << num << std::endl;

        for (zl_int32 i = 0; i < num; ++i)
        {
            std::cout << "Section Name " << obj->GetString(stbl[i].sh_name) << " Address : " << stbl[i].sh_addr << std::endl;
        }

        obj->SaveSectionTable();

        obj->Release();
    }
    else
    {
        std::cout << " Failed !" << std::endl;
    }
    
    system("pause");
    return 0;
}

