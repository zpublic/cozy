#ifndef __COZY_THUNDER_TASK_IMPL__
#define __COZY_THUNDER_TASK_IMPL__

#include "ICozyThunderTask.h"
#include <vector>
#include "ThreadPool.h"
#include "Block.h"
#include <fstream>

namespace Cozy
{
    class CozyThunderTaskImpl : public ICozyThunderTask
    {
    public:
        static const int DefaultBlockSize = 1024 * 1024;

    public:
        CozyThunderTaskImpl();
        ~CozyThunderTaskImpl();

        // state = 0 新任务 1 正在下载 2 下载暂停（旧任务加载） 3 下载完成
        virtual int GetTaskState();
        virtual const wchar_t* GetCfgPath();
        virtual const wchar_t* GetRemotePath();
        virtual const wchar_t* GetLocalPath();
        virtual unsigned long GetFileSize();
        virtual unsigned int GetBlockCount();
        // state = 0 下载开始 1 下载完成 2下载失败
        virtual int GetBlockState(unsigned int blockId);

        virtual void SetCfgPath(const wchar_t* path);
        virtual void SetRemotePath(const wchar_t* path);
        virtual void SetLocalPath(const wchar_t* path);
        void SetTaskState(int value);
        void SetFileSize(unsigned long value);
        void SetBlockCount(unsigned int);
        void SetBlockInfo(const std::wstring& value);

        virtual void SetTaskCallback(ICozyThunderTaskCallback* pCallback);
        // state 0/2 -> 1
        virtual bool Start();
        // state 1 -> 2
        virtual bool Stop();

    private:
        std::size_t InitFile();
        void InitBlock(int n);
        void OnTaskBegin();
        void OnBlockStatus(int id, int status);
        void OnTaskEnd(int code);

    private:
        static std::string ws2s(const std::wstring& ptr);
        static std::wstring s2ws(const std::string& str);
        static std::vector<std::pair<std::size_t, std::size_t>> splitSize(std::size_t size);
        void __safeWrite(std::size_t offset, const void* data, std::size_t size);
        std::string __makeRange(int begin, int end);

    private:
        std::vector<Block>          m_vecBlock;
        ICozyThunderTaskCallback*   m_pCallback;
        ThreadPool                  m_threadPool;
        std::mutex                  m_fileMutex;
        std::wstring                m_lpszLocalPath;
        std::wstring                m_lpszRemtotePath;
        std::wstring                m_lpszCfgPath;
        std::ofstream               m_plocalFile;
        std::atomic<std::size_t>    m_finishblcokCount;
        int                         m_state;
        std::size_t                 m_remoteSize;
    };
}

#endif // __COZY_THUNDER_TASK_IMPL__