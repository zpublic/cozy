#include "Tester/TesterZhihu.h"
#include "Componet/ZhihuEnvInit.h"

NS_BEGIN

void TesterZhihu::Test()
{
    auto init = std::make_shared<ZhihuEnvInit>();
    auto b = init->Init();
}

NS_END