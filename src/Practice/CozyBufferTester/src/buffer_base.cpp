#include "buffer_base.h"

AutoCallObject::AutoCallObject(const std::function<void(void)>& func)
{
    func();
}