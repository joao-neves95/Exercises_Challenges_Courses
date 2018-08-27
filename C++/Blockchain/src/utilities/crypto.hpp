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
// #include <cstring>
#include <atlstr.h>
#include <vector>
#include <cstdint>

#include "picosha2.h"
extern "C" {
    #include "argon2.h"
}

#include "utils.hpp"
#include "console.hpp"

#define HASHLEN 64 // digest length.
#define SALTLEN 16

class Crypto
{
    private:
        Crypto() {};
        ~Crypto() {};

    public:
        /** Returns a SHA256 HEX string hashed version of the data. */
        static std::string toSha256Str(std::string _Data) {
            std::vector<unsigned char> hash( picosha2::k_digest_size );
            picosha2::hash256( _Data.begin(), _Data.end(), hash.begin(), hash.end() );

            const std::string outputHashHexStr = picosha2::bytes_to_hex_string( hash.begin(), hash.end() );

            return outputHashHexStr;
        }

        inline static std::string toArgon2dByteArr( std::string _Data ) {
            uint8_t hash[HASHLEN];
            // char hash[HASHLEN];
            // uint8_t salt[SALTLEN];

            uint32_t t_cost = 1; // 1-pass computation. Number of iterations.
            // uint32_t m_cost = (1 << 16); // 2^16 (== 67.1089 mb).
            uint32_t m_cost = 97656.3; // == 100.0000512 mb. Memory usage in kibibytes.
            uint32_t parallelism = 2; // Number of threads and compute lanes.

            char *dataCStr = new char[_Data.length() + 1];
            strcpy( dataCStr, _Data.c_str() );
            uint32_t dataLen = strlen( (char *)dataCStr );

            char *salt = new char[SALTLEN + 1];
            strcpy( salt, Utils::randomAlphanumStr( SALTLEN ).c_str() );

            char encoded[200];

            // Using both functions for testing purposes only.

            argon2d_hash_encoded(t_cost, m_cost, parallelism, dataCStr, dataLen, salt, SALTLEN, HASHLEN, encoded, 200);

            argon2d_hash_raw( t_cost, m_cost, parallelism, dataCStr, dataLen, salt, SALTLEN, hash, HASHLEN );

            delete[] dataCStr;
            delete[] salt;

            // Hash in hex (testing):
            unsigned int i;
            for (i = 0; i < HASHLEN; ++i) {
                printf( "%02x", hash[i] );
            }

            CString encodedCStr( encoded );
            std::string encodedStr( (LPCTSTR)encodedCStr );
            return encodedStr;
        }
};
