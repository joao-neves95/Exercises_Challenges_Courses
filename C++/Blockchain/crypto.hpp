#pragma once
#include <string>

static class Crypto
{
    public:
        Crypto();
        ~Crypto();

        static std::string toSha256Str(std::string data);
};
