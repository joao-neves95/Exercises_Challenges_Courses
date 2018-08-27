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
#include <cstdint>
#include <atlstr.h>
#include <vector>

#include "picosha2.h"
extern "C" {
    #include "argon2.h"
}

#include "utils.hpp"
#include "console.hpp"

#define HASHLEN 32 // digest length.
#define SALTLEN 16
#define ENCODEDLEN 98

// Argon2d.
static const uint32_t t_cost = 1; // 1-pass computation. Number of iterations.
static const uint32_t parallelism = 2; // Number of threads and compute lanes.

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

        // TODO: Avoid code repetition in the Argon2d methods.

        /** 
            m_cost = 97656.3 == 100.0000512 mb. Memory usage in kibibytes. 
        */
        inline static std::string toArgon2dHexStr( std::string _Data, uint32_t m_cost = 97656.3 ) {
            char *dataCStr = new char[_Data.length() + 1];
            strcpy( dataCStr, _Data.c_str() );
            uint32_t dataLen = strlen( (char *)dataCStr );

            char *salt = new char[SALTLEN + 1];
            strcpy( salt, Utils::randomAlphanumStr( SALTLEN ).c_str() );

            uint8_t hash[HASHLEN];

            argon2d_hash_raw( t_cost, m_cost, parallelism, dataCStr, dataLen, salt, SALTLEN, hash, HASHLEN );

            delete[] dataCStr;
            delete[] salt;

            std::vector<uint8_t> hashVec;
            hashVec.insert( hashVec.begin(), hash, hash + HASHLEN );

            const std::string outputHashHexStr = picosha2::bytes_to_hex_string( hashVec.begin(), hashVec.end() );

            return outputHashHexStr;
        }

        inline static std::string toArgon2dEncodedStr( std::string _Data, uint32_t m_cost = 97656.3 ) {
            char *dataCStr = new char[_Data.length() + 1];
            strcpy( dataCStr, _Data.c_str() );
            uint32_t dataLen = strlen( (char *)dataCStr );

            char *salt = new char[SALTLEN + 1];
            strcpy( salt, Utils::randomAlphanumStr( SALTLEN ).c_str() );

            char encoded[ENCODEDLEN];

            argon2d_hash_encoded( t_cost, m_cost, parallelism, dataCStr, dataLen, salt, SALTLEN, HASHLEN, encoded, ENCODEDLEN );

            delete[] dataCStr;
            delete[] salt;

            CString encodedCStr( encoded );
            std::string encodedStr( (LPCTSTR)encodedCStr );
            return encodedStr;
        }
};
