#ifndef __COZY_BUFFER_HELPER__
#define __COZY_BUFFER_HELPER__

#include <unordered_map>
#include <vector>
#include <functional>
#include "buffer.h"
#include "buffer_base.h"

extern std::unordered_map<std::string, std::vector<std::function<void(Buffer&, void*)>>> SerializeMapping;
extern std::unordered_map<std::string, std::vector<std::function<void(Buffer&, void*)>>> DeserializeMapping;

#define GetName(n) #n

#define RegisterSerialize(Class, Member)                                                \
do{                                                                                     \
SerializeMapping[GetName(Class)].push_back([](Buffer& b, void* p) {                     \
    auto ptr = reinterpret_cast<##Class##*>(p);                                         \
    ##Class##& ref = *ptr;                                                              \
    b.Write(ref.##Member##);                                                            \
});                                                                                     \
                                                                                        \
DeserializeMapping[GetName(Class)].push_back([](Buffer& b, void* p) {                   \
    auto ptr = reinterpret_cast<##Class##*>(p);                                         \
    ##Class##& ref = *ptr;                                                              \
    b.Read(ref.##Member##);                                                             \
});                                                                                     \
                                                                                        \
}while(0)                                                                               \

#define SerializeObject(Class, Buff, Object)                                            \
    Assert(!MappingTest(GetName(Class)));                                               \
do{                                                                                     \
for (auto &func : SerializeMapping[GetName(Class)])                                     \
{                                                                                       \
    func(Buff, &Object);                                                                \
}                                                                                       \
}while(0)                                                                               \

#define DeserializeObject(Class, Buff, Object)                                          \
    Assert(!MappingTest(GetName(Class)));                                               \
do{                                                                                     \
for (auto &func : DeserializeMapping[GetName(Class)])                                   \
{                                                                                       \
    func(Buff, &Object);                                                                \
}                                                                                       \
}while(0)                                                                               \

bool MappingTest(const std::string& name);

#endif // __COZY_BUFFER_HELPER__
