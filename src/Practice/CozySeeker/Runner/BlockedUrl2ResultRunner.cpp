#include "BlockedUrl2ResultRunner.h"

NS_BEGIN

BlockedUrl2ResultRunner::BlockedUrl2ResultRunner()
{

}

BlockedUrl2ResultRunner::~BlockedUrl2ResultRunner()
{

}

void BlockedUrl2ResultRunner::OnNewUrl(StrPtr url)
{
    if (m_trans != nullptr)
    {
        m_trans->OnNewUrl(url);
    }
}

void BlockedUrl2ResultRunner::To(IUrl2ResultPtr to)
{
    m_trans = to;
}

void BlockedUrl2ResultRunner::Start()
{

}

void BlockedUrl2ResultRunner::Stop()
{

}

NS_END