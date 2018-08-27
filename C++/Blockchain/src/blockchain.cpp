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
//
// The block generation process:
//  .generateNewBlock() -> .mine(); -> .validateNewBlock(); -> .addBlockToChain();
//

#include <mutex>
#include <iostream>
#include <string>

#include "blockchain.hpp"
#include "libs\json-3.2.0\single_include\nlohmann\json.hpp"
using namespace nlohmann;

#include "config.hpp"
#include "db.hpp"
#include "utilities\crypto.hpp"
#include "utilities\console.hpp"
#include "mining.hpp"
#include "models\block.hpp"

Blockchain::Blockchain()
{
}

Blockchain::~Blockchain()
{
}

Blockchain* Blockchain::instance = nullptr;
std::once_flag Blockchain::onceFlag;

void Blockchain::createInstance() 
{
    Blockchain::instance = new Blockchain();
    Blockchain::instance->chain = new DB( DATABASE_BLOCKS );

    // Temporary.
    Blockchain::instance->genesis();
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
    this->genesisComplete = true;

    Console::log( "\n\n" );
    Console::log( "Created the genesis block:\n" );
    Console::log( "\n" );
    Blockchain::printBlockInfo( genesisBlock );
    Console::log( "\n\n" );

    Block* minedBlock = Mining::mine( genesisBlock );

    if (minedBlock->hash != "")
        this->addBlockToChain( *minedBlock );

    delete minedBlock;
}

json Blockchain::getLatestBlock() 
{
    std::string latestBlockKey = this->chain->get( kLastBlockKey );
    return json::parse( this->chain->get(latestBlockKey) );
}

void Blockchain::addBlockToChain( const Block _Block ) 
{
    if (this->validateNewBlock( _Block )) {
        this->chain->del( kLastBlockKey );
        this->chain->put( _Block.hash, Blockchain::blockToJson( _Block ).dump() );
        this->chain->put( kLastBlockKey, _Block.hash );
        Console::log( "New block successfully added to the chain:\n" );
        Blockchain::printBlockInfo( _Block );
    }
}

void Blockchain::generateNewBlock(const std::string _BlockData)
{
    const json latestBlock = getLatestBlock();
    const Block newBlock = Block::Block( latestBlock["index"].get<unsigned long long>() + 1, latestBlock["hash"].get<std::string>(), _BlockData);

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
    if (this->chain->count(true) <= 0) return true;

    Console::log( "Validating new block..." );
    const json latestBlock = getLatestBlock();

    //std::string indexStr = latestBlock["index"].get<std::string>();
    //char *indexCStr = new char[indexStr.length() + 1];
    //strcpy( indexCStr, indexStr.c_str() );

    if (_BlockToValidate.index != latestBlock["index"].get<unsigned long long>() + 1)
    {
        Console::log( "Invalid index." );
        return false;
    }
    else if (_BlockToValidate.previousHash != latestBlock["hash"].get<std::string>())
    {
        Console::log( "Invalid previous hash." );
        return false;
    }
    else if (_BlockToValidate.hash != Blockchain::calculateBlockHash( _BlockToValidate ))
    {
        Console::log( "Invalid hash." );
        return false;
    }

    // delete[] indexCStr;

    Console::log( "The block is valid." );
    return true;
}

void Blockchain::replaceChain(const std::vector<Block> _NewChain) 
{
    if (_NewChain.size() > this->chain->count(true))
    {
        leveldb::DestroyDB( DATABASE_BLOCKS, leveldb::Options() );

        unsigned long long int i;
        for (i = 0; i < _NewChain.size(); ++i) {
            this->chain->put( _NewChain[i].hash, Blockchain::blockToJson( _NewChain[i] ).dump() );
        }

        // Update cached block count.
        this->chain->count();
    }
}

json Blockchain::blockToJson( const Block _Block ) 
{
    json jsonBlock = {
        {"index", _Block.index},
        {"timestamp", _Block.timestamp},
        {"hash", _Block.hash},
        {"previousHash", _Block.previousHash},
        {"targetBits", _Block.targetBits},
        {"targetBits", _Block.nounce},
        {"targetBits", _Block.data}
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
