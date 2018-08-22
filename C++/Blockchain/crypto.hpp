#pragma once
#include <string>

class Crypto
{
    private:
        Crypto();
        ~Crypto();

    public:
        static std::string toSha256Str(std::string data);
};
