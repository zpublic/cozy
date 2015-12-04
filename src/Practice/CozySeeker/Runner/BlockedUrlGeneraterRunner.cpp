#include "BlockedUrlGeneraterRunner.h"

NS_BEGIN

BlockedUrlGeneraterRunner::BlockedUrlGeneraterRunner()
{

}
BlockedUrlGeneraterRunner::~BlockedUrlGeneraterRunner()
{

}

void BlockedUrlGeneraterRunner::OnNewUrl(StrPtr url)
{
    if (m_to != nullptr)
    {
        m_to->OnNewUrl(url);
    }
}

void BlockedUrlGeneraterRunner::From(IUrlGeneraterPtr i)
{
    m_gen = i;
}

void BlockedUrlGeneraterRunner::To(IUrlInPtr to)
{
    m_to = to;
}

void BlockedUrlGeneraterRunner::Start()
{
    if (m_gen != nullptr)
    {
        m_gen->To(shared_from_this());
        m_gen->Start();
    }
}

void BlockedUrlGeneraterRunner::Stop()
{
    if (m_gen != nullptr)
    {
        m_gen->Stop();
    }
}

NS_END