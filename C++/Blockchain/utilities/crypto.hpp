#pragma once
#include <string>
#include <vector>

#include "..\libs\PicoSHA2\picosha2.h"

class Crypto
{
    private:
        Crypto() {};
        ~Crypto() {};

    public:
        static std::string toSha256Str(std::string _Data) {
            std::vector<unsigned char> hash( picosha2::k_digest_size );
            picosha2::hash256( _Data.begin(), _Data.end(), hash.begin(), hash.end() );

            const std::string outputHashHexStr = picosha2::bytes_to_hex_string( hash.begin(), hash.end() );

            return outputHashHexStr;
        }
};
