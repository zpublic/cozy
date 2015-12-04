#include "MultiUrlGeneraterRunner.h"

NS_BEGIN

MultiUrlGeneraterRunner::MultiUrlGeneraterRunner(int threadCount = 1)
    :m_AsyncInvoker(threadCount)
{
    m_AsyncInvoker.SetCallback(std::bind(&MultiUrlGeneraterRunner::OnInvoke, this, std::placeholders::_1));
}

MultiUrlGeneraterRunner::~MultiUrlGeneraterRunner()
{
    if (m_AsyncInvoker.IsRunning())
    {
        m_AsyncInvoker.Stop();
    }
}

void MultiUrlGeneraterRunner::Start()
{
    m_AsyncInvoker.Start();
    for (auto i : m_Gens)
    {
        if (i != nullptr)
        {
            i->To(this->shared_from_this());
            i->Start();
        }
    }
}

void MultiUrlGeneraterRunner::Stop()
{
    for (auto i : m_Gens)
    {
        if (i != nullptr)
        {
            i->Stop();
        }
    }
    m_AsyncInvoker.Stop();
}

void MultiUrlGeneraterRunner::To(IUrlInPtr to)
{
    m_Tos.push_back(to);
}

void MultiUrlGeneraterRunner::From(IUrlGeneraterPtr ptr)
{
    m_Gens.push_back(ptr);
}

void MultiUrlGeneraterRunner::OnNewUrl(StrPtr url)
{
    m_AsyncInvoker.Add(url);
}

void MultiUrlGeneraterRunner::OnInvoke(StrPtr ptr)
{
    for (auto to : m_Tos)
    {
        if (to != nullptr)
        {
            to->OnNewUrl(ptr);
        }
    }
}

NS_END