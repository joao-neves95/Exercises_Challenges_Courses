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

#include "json-3.2.0\single_include\nlohmann\json.hpp"

class Block {
    public:
        unsigned long long index;
        /** UTC asctime timestamp. */
        std::string timestamp;
        std::string hash;
        std::string argonHash;
        std::string previousHash;
        /** The difficulty at wich this block was mined. */
        unsigned int targetBits;
        unsigned int difficulty;
        long long nounce;
        // Data is temporary. To be substituted by a Transaction vector.
        std::string data;

        Block();
        /** Generates a block without hash (for mining). */
        Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data );
        Block( unsigned long long _Index, std::string _TimestampCreation, std::string _PreviousHash, std::string _Data, std::string _Hash, long long _Nounce );
        Block( unsigned long long _Index, std::string _TimestampCreation, std::string _PreviousHash, std::string _Data, std::string _Hash, std::string _ArgonHash, long long _Nounce );

        ~Block();

        std::string getData();

        std::string getDataNoNounce();

        nlohmann::json toJson();

        std::string calculateSHA256Hash();
  
        std::string calculateArgon2dHexHash( uint32_t m_cost = 68359.4 );
        
        std::string calculateArgon2dEncodedHash( uint32_t m_cost = 68359.4 );

        std::string calculateHybridHash();

};
