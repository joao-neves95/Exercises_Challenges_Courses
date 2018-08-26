// --------------------------------------------------------------------------
//
// Copyright (c) 2018 shivayl (João Neves - https://github.com/joao-neves95)
//
// Licensed under the MIT License (https://opensource.org/licenses/MIT).
//
// The license of this source code can be found in the MIT-LICENSE file 
// located in the root of this project.
//
// --------------------------------------------------------------------------

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
