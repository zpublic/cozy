#ifndef __COZY_BLOCK__
#define __COZY_BLOCK__

#include "CozyThunderDef.h"

namespace Cozy
{
    class Block
    {
    public:
        Block()
            : m_BlockStatus(BlockStatusStart)
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

#endif // __COZY_BLOCK__