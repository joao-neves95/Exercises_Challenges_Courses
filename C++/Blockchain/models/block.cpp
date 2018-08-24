#include <string>
#include <ctime>
using namespace std;

#include "block.hpp"
#include "..\mining.hpp"
#include "..\utilities\crypto.hpp"
#include "..\utilities\utils.hpp"
#include "..\config.hpp"

Block::Block() {}

Block::Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data ) {
    this->index = _Index;
    this->timestamp = Utils::getUTCTimestampStr();
    this->previousHash = _PreviousHash;
    this->data = _Data;
    this->hash = "";
    this->targetBits = Mining::getTargetBits();
    this->nounce = 0;
}

Block::Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data, std::string _Hash, long long _Nounce )
{
    this->index = _Index;
    this->timestamp = Utils::getUTCTimestampStr();
    this->previousHash = _PreviousHash;
    this->data = _Data;
    this->hash = _Hash;
    this->targetBits = Mining::getTargetBits();
    this->nounce = _Nounce;
};

Block::~Block() {};
