#include <mutex>
#include <iostream>
#include <string>

#include "blockchain.hpp"
#include "libs\crow\include\crow\json.h"
#include "libs\json-3.2.0\single_include\nlohmann\json.hpp"
using namespace nlohmann;

#include "utilities\crypto.hpp"
#include "utilities\console.hpp"
#include "mining.hpp"
#include "models\block.hpp"

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

void Blockchain::createInstance() 
{
    Blockchain::instance = new Blockchain();
}

Blockchain* Blockchain::getInstance() 
{
    // TODO: FIX BUG: This is beeing called twice.
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

Block Blockchain::getLatestBlock() 
{
    return chain[chain.size() - 1];
}

void Blockchain::addBlockToChain( const Block _Block ) 
{
    if (this->validateNewBlock( _Block )) {
        this->chain.push_back( _Block );
        Console::log( "New block successfully added to the chain:\n" );
        Blockchain::printBlockInfo( _Block );
    }
}

void Blockchain::generateNewBlock(const std::string _BlockData)
{
    const Block latestBlock = getLatestBlock();
    const Block newBlock = Block::Block(latestBlock.index + 1, latestBlock.hash, _BlockData);

    Console::log( "\nGenerated a new block:\n" );
    Blockchain::printBlockInfo( newBlock );
    Console::log( "\n" );

    Block* minedBlock = Mining::mine( newBlock );

    if (minedBlock->hash != "")
        this->addBlockToChain( *minedBlock );

    delete minedBlock;
}

std::string Blockchain::getBlockData( const Block _Block )
{
    const string blockData = std::to_string( _Block.index ) + _Block.timestamp + _Block.previousHash + _Block.data + std::to_string( _Block.targetBits ) + std::to_string( _Block.nounce );

    return blockData;
}

std::string Blockchain::calculateBlockHash(const Block _Block) 
{
    const std::string data = Blockchain::getBlockData( _Block );
    return Crypto::toSha256Str( data );
}

// TODO: Validate timestamps.
bool Blockchain::validateNewBlock(const Block _BlockToValidate)
{
    Console::log( "Validating new block..." );
    const Block latestBlock = getLatestBlock();

    if (_BlockToValidate.index != latestBlock.index + 1)
    {
        Console::log( "Invalid index." );
        return false;
    }
    else if (_BlockToValidate.previousHash != latestBlock.hash)
    {
        Console::log( "Invalid previous hash." );
        return false;
    }
    else if (_BlockToValidate.hash != Blockchain::calculateBlockHash( _BlockToValidate ))
    {
        Console::log( "Invalid hash." );
        return false;
    }

    Console::log( "The block is valid." );
    return true;
}

void Blockchain::replaceChain(const std::vector<Block> newChain) 
{
    if (newChain.size() > this->chain.size())
    {
        this->chain = newChain;
    }
}

json Blockchain::blockToJson( const Block _Block ) 
{
    json jsonBlock = {
        {"index", _Block.index},
        {"timestamp", _Block.timestamp},
        {"previousHash", _Block.previousHash},
        {"targetBits", _Block.targetBits},
        {"hash", _Block.hash}
    };

    return jsonBlock;
}

std::string Blockchain::chainToJson( const std::vector<Block> _Chain ) 
{
    std::vector<json> jsonChain;
    
    unsigned long long i;
    for (i = 0; i < _Chain.size(); ++i) {
        jsonChain.push_back( Blockchain::blockToJson( _Chain[i] ) );
    }

    json j_vec( jsonChain );

    return j_vec.dump();
}

void Blockchain::printBlockInfo( const Block _Block ) 
{
    std::cout << "Id: " << std::to_string( _Block.index )
              << "\nTimestamp: " << _Block.timestamp
              << "Hash: " << _Block.hash
              << "\nPrevious Hash: " << _Block.previousHash
              << "\nTarget Bits: " << _Block.targetBits
              << "\nData: " << _Block.data
              << std::endl;
}
