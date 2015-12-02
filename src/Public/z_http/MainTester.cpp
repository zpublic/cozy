#include "z_curl_warpper.h"

int main()
{
    zl::http::ZLCurlWarpper curls;
    auto t = curls.Perform("http://www.baidu.com");
    auto s = curls.GetStatusCode();
    return 0;
}