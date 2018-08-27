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

Block::Block() {};

Block::Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data ) {
    this->index = _Index;
    this->timestamp = Utils::getUTCTimestampStr();
    this->previousHash = _PreviousHash;
    this->data = _Data;
    this->hash = "";
    // this->targetBits = Mining::getTargetBits();
    this->targetBits = Mining::getTargetDifficulty();
    this->nounce = 0;
}

Block::Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data, std::string _Hash, long long _Nounce )
{
    this->index = _Index;
    this->timestamp = Utils::getUTCTimestampStr();
    this->previousHash = _PreviousHash;
    this->data = _Data;
    this->hash = _Hash;
    // this->targetBits = Mining::getTargetBits();
    this->targetBits = Mining::getTargetDifficulty();
    this->nounce = _Nounce;
};

Block::~Block() {};

std::string Block::getDataNoNounce() {
    return std::to_string( this->index ) + this->timestamp + this->previousHash + this->data + std::to_string( this->targetBits );
}

std::string Block::getData() {
    const string blockData = this->getDataNoNounce() + std::to_string( this->nounce );

    return blockData;
}

json Block::toJson() {
    json jsonBlock = {
        {"index", this->index},
        {"timestamp", this->timestamp},
        {"hash", this->hash},
        {"previousHash", this->previousHash},
        {"targetBits", this->targetBits},
        {"nounce", this->nounce},
        {"data", this->data}
    };

    return jsonBlock;
}

std::string Block::calculateSHA256Hash() {
    return Crypto::toSha256Str( this->getData() );
}

std::string Block::calculateArgon2dHexHash( uint32_t m_cost ) {
    return Crypto::toArgon2dHexStr( this->getData(), m_cost );
}

std::string Block::calculateArgon2dEncodedHash( uint32_t m_cost ) {
    return Crypto::toArgon2dEncodedStr( this->getData(), m_cost );
}

/**  
    SHA256( Argon2d( blockData ) + blockData.nounce )
*/
std::string Block::calculateHybridHash( uint32_t m_cost ) {
    std::string argon2dHexHash = Crypto::toArgon2dHexStr( this->getData(), m_cost );

    return Crypto::toSha256Str( argon2dHexHash + std::to_string( this->nounce ) );
}


