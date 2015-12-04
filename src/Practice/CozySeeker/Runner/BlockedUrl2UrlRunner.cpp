#include "BlockedUrl2UrlRunner.h"

NS_BEGIN

BlockedUrl2UrlRunner::BlockedUrl2UrlRunner()
{

}

BlockedUrl2UrlRunner::~BlockedUrl2UrlRunner()
{

}

void BlockedUrl2UrlRunner::SetProcessor(IUrl2UrlPtr p)
{
    m_Process = p;
    if (m_urlIn != nullptr)
    {
        p->To(m_urlIn);
    }
}

void BlockedUrl2UrlRunner::OnNewUrl(StrPtr url)
{
    if (m_Process != nullptr)
    {
        m_Process->OnNewUrl(url);
    }
}

void BlockedUrl2UrlRunner::Start()
{
    if (m_Process != nullptr)
    {
        m_Process->Start();
    }
}

void BlockedUrl2UrlRunner::Stop()
{
    if (m_Process != nullptr)
    {
        m_Process->Stop();
    }
}

void BlockedUrl2UrlRunner::To(IUrlInPtr to)
{
    m_urlIn = to;
    if (m_Process != nullptr)
    {
        m_Process->To(m_urlIn);
    }
}

NS_END