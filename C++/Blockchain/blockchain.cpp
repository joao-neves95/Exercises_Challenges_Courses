#include <mutex>
#include <iostream>
#include <string>

#include "blockchain.hpp"
#include "libs\crow\include\crow\json.h"
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

void Blockchain::generateNextBlock(std::string _BlockData) 
{
    const Block latestBlock = getLatestBlock();
    Block newBlock = Block::Block(latestBlock.index + 1, latestBlock.hash, _BlockData);
    newBlock.hash = calculateBlockHash( newBlock );
    chain.push_back( newBlock );
}

std::string Blockchain::calculateBlockHash(Block _Block) {
    std::string data = std::to_string(_Block.index) + _Block.previousHash + _Block.timestamp + _Block.data;
    return Crypto::toSha256Str( data );
}

bool Blockchain::validateNewBlock(Block _BlockToValidate) 
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

void Blockchain::replaceChain(std::vector<Block> newChain) {
    if (newChain.size() > this->chain.size())
    {
        chain = newChain;
    }
}

crow::json::wvalue Blockchain::blockToJson( Block _Block ) {
    crow::json::wvalue jsonBlock;
    jsonBlock["index"] = _Block.index;
    jsonBlock["timestamp"] = _Block.timestamp;
    jsonBlock["previousHash"] = _Block.previousHash;
    jsonBlock["hash"] = _Block.hash;

    return jsonBlock;
}

void Blockchain::printBlockInfo( Block _Block ) {
    std::cout << "Id: " << std::to_string( _Block.index )
              << "\nTimestamp: " << _Block.timestamp
              << "Hash: " << _Block.hash
              << "\nPrevious Hash: " << _Block.previousHash
              << "\nData: " << _Block.data
              << std::endl;
}
