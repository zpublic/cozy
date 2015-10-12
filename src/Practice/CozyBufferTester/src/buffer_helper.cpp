#include "buffer_helper.h"
#include "buffer.h"

std::unordered_map<std::string, std::vector<std::function<void(Buffer&, void*)>>> SerializeMapping;
std::unordered_map<std::string, std::vector<std::function<void(Buffer&, void*)>>> DeserializeMapping;

bool MappingTest(const std::string& name)
{
    return SerializeMapping.find(name) == SerializeMapping.end()
        && DeserializeMapping.find(name) == DeserializeMapping.end();
}