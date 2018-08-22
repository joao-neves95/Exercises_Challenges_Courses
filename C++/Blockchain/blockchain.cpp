#include <mutex>
#include <iostream>
#include <string>

#include "blockchain.hpp"
#include "libs\crow\include\crow\json.h"
#include "libs\json-3.2.0\single_include\nlohmann\json.hpp"
using namespace nlohmann;
#include "crypto.hpp"
#include "models\block.hpp"
#include "utils\console.hpp"

Blockchain::Blockchain()
{
    // Temporary.
    this->genesis();
}

Blockchain::~Blockchain()
{
}

Blockchain* Blockchain::instance = nullptr;
std::once_flag Blockchain::onceFlag;

void Blockchain::createInstance() {
    Blockchain::instance = new Blockchain();
}

Blockchain* Blockchain::getInstance() {
    std::call_once( Blockchain::onceFlag, Blockchain::createInstance );

    return Blockchain::instance;
}

void Blockchain::genesis() 
{
    if (this->genesisComplete)
        return;

    Block genesisBlock = Block::Block(0, "0", "The GENESIS block.");
    genesisBlock.hash = calculateBlockHash( genesisBlock );
    chain.push_back( genesisBlock );
    this->genesisComplete = true;

    Console::log( "\n\n" );
    Console::log( "Created the genesis block:\n" );
    Console::log( "\n" );
    Blockchain::printBlockInfo( genesisBlock );
    Console::log( "\n\n" );
}

Block Blockchain::getLatestBlock() {
    return chain[chain.size() - 1];
}

void Blockchain::generateNextBlock(const std::string _BlockData)
{
    const Block latestBlock = getLatestBlock();
    Block newBlock = Block::Block(latestBlock.index + 1, latestBlock.hash, _BlockData);
    newBlock.hash = calculateBlockHash( newBlock );
    this->chain.push_back( newBlock );

    Console::log( "\n" );
    Console::log( "Created a new block:\n" );
    Blockchain::printBlockInfo( newBlock );
    Console::log( "\n" );
}

std::string Blockchain::calculateBlockHash(const Block _Block) {
    std::string data = std::to_string(_Block.index) + _Block.previousHash + _Block.timestamp + _Block.data;
    return Crypto::toSha256Str( data );
}

bool Blockchain::validateNewBlock(const Block _BlockToValidate)
{
    Console::log( "Validating new block..." );
    const Block latestBlock = getLatestBlock();

    if (_BlockToValidate.index != latestBlock.index + 1)
    {
        Console::log( "Invalid index." );
        return false;
    }
    else if (_BlockToValidate.previousHash != latestBlock.previousHash)
    {
        Console::log( "Invalid previous hash." );
        return false;
    }
    else if (_BlockToValidate.hash != calculateBlockHash( _BlockToValidate ))
    {
        Console::log( "Invalid hash." );
        return false;
    }

    return true;
}

void Blockchain::replaceChain(const std::vector<Block> newChain) {
    if (newChain.size() > this->chain.size())
    {
        this->chain = newChain;
    }
}

json Blockchain::blockToJson( const Block _Block ) {
    //json jsonBlock;
    //jsonBlock["index"] = _Block.index;
    //jsonBlock["timestamp"] = _Block.timestamp;
    //jsonBlock["previousHash"] = _Block.previousHash;
    //jsonBlock["hash"] = _Block.hash;

    json jsonBlock = {
        {"index", _Block.index},
        {"timestamp", _Block.timestamp},
        {"previousHash", _Block.previousHash},
        {"hash", _Block.hash}
    };

    return jsonBlock;
}

std::string Blockchain::chainToJson( const std::vector<Block> _Chain ) {
    std::vector<json> jsonChain;
    
    unsigned long long i;
    for (i = 0; i < _Chain.size(); ++i) {
        jsonChain.push_back( Blockchain::blockToJson( _Chain[i] ) );
    }

    json j_vec( jsonChain );

    return j_vec.dump();
}

void Blockchain::printBlockInfo( const Block _Block ) {
    std::cout << "Id: " << std::to_string( _Block.index )
              << "\nTimestamp: " << _Block.timestamp
              << "Hash: " << _Block.hash
              << "\nPrevious Hash: " << _Block.previousHash
              << "\nData: " << _Block.data
              << std::endl;
}
