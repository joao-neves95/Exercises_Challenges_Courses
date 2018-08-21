#include <iostream>
#include <string>
#include <ctime>
using namespace std;

#include "block.hpp"
#include "crypto.hpp"

Block::Block(unsigned long long _Index, std::string _PreviousHash, std::string _Data) 
{
        index = _Index;

        time_t now = time(0);
        tm *utc = gmtime(&now);
        timestamp = asctime(utc);

        previousHash = _PreviousHash;
        data = _Data;

        hash = calculateHash();
    };

Block::~Block() {};

void Block::printBlockInfo()
{
    std::cout << "Id: " << std::to_string(index)
              << "\nTimestamp: " << timestamp
              << "\nHash: " << hash
              << "\nPrevious Hash: " << previousHash
              << "Data: " << data
              << std::endl;
}

std::string Block::calculateHash()
{
    std::string data = std::to_string(index) + previousHash + timestamp + data;
    return Crypto::toSha256Str(data);
}
