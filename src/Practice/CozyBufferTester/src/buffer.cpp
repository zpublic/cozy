#include "buffer.h"

void Buffer::SeekIterator(SeekType type, int offset)
{
    if (type == SeekType::Begin)
    {
        m_dataIterator = offset;
    }
    else if(type == SeekType::End)
    {
        m_dataIterator = m_data.size() - offset;
    }
    else if (type == SeekType::Curr)
    {
        m_dataIterator += offset;
    }
}

void Buffer::Clear()
{
    m_data.clear();
    m_dataIterator = 0;
}