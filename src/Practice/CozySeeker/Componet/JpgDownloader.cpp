#include "Componet/JpgDownloader.h"

NS_BEGIN

JpgDownloader::JpgDownloader()
    : m_httpClient(true), m_count(0)
{

}

void JpgDownloader::OnNewUrl(Cozy::StrPtr url)
{
    char c[255];
    sprintf_s(c, "%d.jpg", m_count.fetch_add(1));
    m_httpClient.DownloadFile(*url, std::string(c));
}

NS_END