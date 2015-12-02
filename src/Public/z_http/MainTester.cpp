#include "z_curl_warpper.h"
#include "z_file_writer.h"

int main()
{
    zl::http::ZLCurlWarpper curls;
    curls.SetWriteCallback(new zl::http::ZLFileWriter("d:\\1.html"));
    auto t = curls.Perform("http://www.baidu.com");
    auto s = curls.GetStatusCode();
    return 0;
}