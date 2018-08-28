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

#include <string>
#include <ctime>
using namespace std;

#include "json-3.2.0\single_include\nlohmann\json.hpp"
using namespace nlohmann;

#include "block.hpp"
#include "..\mining.hpp"
#include "..\utilities\crypto.hpp"
#include "..\utilities\utils.hpp"
#include "..\config.hpp"

// TODO: Have timestampCreation and timestampMined properties.

Block::Block() {};

Block::Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data )
{
    this->index = _Index;
    this->timestamp = Utils::getUTCTimestampStr();
    this->previousHash = _PreviousHash;
    this->data = _Data;
    this->hash = "";
    this->argonHash = "";
    this->targetBits = Mining::getTargetBits();
    this->difficulty = Mining::getTargetDifficulty();
    this->nounce = 0;
}

Block::Block( unsigned long long _Index, std::string _TimestampCreation, std::string _PreviousHash, std::string _Data, std::string _Hash, long long _Nounce )
{
    this->index = _Index;
    this->timestamp = _TimestampCreation;
    this->previousHash = _PreviousHash;
    this->data = _Data;
    this->hash = _Hash;
    this->argonHash = "";
    this->targetBits = Mining::getTargetBits();
    this->difficulty = Mining::getTargetDifficulty();
    this->nounce = _Nounce;
};

Block::Block( unsigned long long _Index, std::string _TimestampCreation, std::string _PreviousHash, std::string _Data, std::string _Hash, std::string _ArgonHash, long long _Nounce )
{
    this->index = _Index;
    this->timestamp = _TimestampCreation;
    this->previousHash = _PreviousHash;
    this->data = _Data;
    this->hash = _Hash;
    this->argonHash = _ArgonHash;
    this->targetBits = Mining::getTargetBits();
    this->difficulty = Mining::getTargetDifficulty();
    this->nounce = _Nounce;
};

Block::~Block() {};

std::string Block::getDataNoNounce() 
{
    return std::to_string( this->index ) + this->timestamp + this->previousHash + this->data + std::to_string( this->targetBits ) + std::to_string( this->difficulty );
}

std::string Block::getData() 
{
    const string blockData = this->getDataNoNounce() + std::to_string( this->nounce );

    return blockData;
}

json Block::toJson()
{
    json jsonBlock = {
        {"index", this->index},
        {"timestamp", this->timestamp},
        {"hash", this->hash},
        {"argonHash", this->argonHash},
        {"previousHash", this->previousHash},
        {"targetBits", this->targetBits},
        {"difficulty", this->difficulty},
        {"nounce", this->nounce},
        {"data", this->data}
    };

    return jsonBlock;
}

std::string Block::calculateSHA256Hash()
{
    return Crypto::toSha256Str( this->getData() );
}

std::string Block::calculateArgon2dHexHash( uint32_t m_cost )
{
    return Crypto::toArgon2dHexStr( this->getData(), m_cost );
}

std::string Block::calculateArgon2dEncodedHash( uint32_t m_cost )
{
    return Crypto::toArgon2dEncodedStr( this->getData(), m_cost );
}

/**  
    SHA256( Argon2d( blockData ) + blockData.nounce )
*/
std::string Block::calculateHybridHash()
{
    return Crypto::toSha256Str( this->argonHash + std::to_string( this->nounce ) );
}
