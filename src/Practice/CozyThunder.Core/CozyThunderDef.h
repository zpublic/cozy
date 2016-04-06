#ifndef __COZY_THUNDER_DEF__
#define __COZY_THUNDER_DEF__

#ifdef COZY_API_EXPORT 
#define COZY_API __declspec(dllexport)
#else
#define COZY_API __declspec(dllimport)
#endif // COZY_API_EXPORT

namespace Cozy
{
    static const int BlockStatusInvalid = -1;
    static const int BlockStatusStart   = 0;
    static const int BlockStatusFinish  = 1;
    static const int BlockStatusFailed  = 2;
    
    struct Block
    {
    public:
        Block()
            : m_BlockStatus(BlockStatusInvalid)
        {

        }

        int GetBlcokStatus() const
        {
            return m_BlockStatus;
        }

        void SetBlockStatus(int value)
        {
            m_BlockStatus = value;
        }

    private:
        int m_BlockStatus;
    };
}

#endif // __COZY_THUNDER_DEF__
