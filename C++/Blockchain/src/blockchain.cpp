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
// TODO: BUG: Block index is allways 1 now.
#include "blockchain.hpp"

#include <mutex>
#include <iostream>
#include <string>

#include "json-3.2.0\single_include\nlohmann\json.hpp"
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
    std::call_once( Blockchain::onceFlag, Blockchain::createInstance );

    return Blockchain::instance;
}

void Blockchain::genesis() 
{
    if (this->genesisComplete)
        return;

    Block genesisBlock = Block::Block(0, "0", "The GENESIS block.");
    genesisBlock.hash = genesisBlock.calculateHybridHash( genesisBlock.targetBits );

    Console::log( "\n\n" );
    Console::log( "Created the genesis block:\n" );
    Console::log( "\n" );
    Blockchain::printBlockInfo( genesisBlock );
    Console::log( "\n\n" );

    // Block* minedBlock = Mining::mineSHA256( genesisBlock );
    Block* minedBlock = Mining::mineHybrid( genesisBlock );

    if (minedBlock == NULL)
        return;

    this->addBlockToChain( *minedBlock );
    delete minedBlock;
    this->genesisComplete = true;
}

json Blockchain::getLatestBlock() 
{
    std::string latestBlockKey = this->chain->get( kLastBlockKey );
    return json::parse( this->chain->get(latestBlockKey) );
}

void Blockchain::addBlockToChain( Block _Block ) 
{
    if (this->validateNewBlock( _Block )) {
        this->chain->del( kLastBlockKey );
        this->chain->put( _Block.hash, _Block.toJson().dump() );
        this->chain->put( kLastBlockKey, _Block.hash );
        Console::log( "New block successfully added to the chain:\n" );
        Blockchain::printBlockInfo( _Block );
    }
}

void Blockchain::generateNewBlock( const std::string _BlockData )
{
    const json latestBlock = getLatestBlock();
    const Block newBlock = Block::Block( latestBlock["index"].get<unsigned long long>() + 1, latestBlock["hash"].get<std::string>(), _BlockData);

    Console::log( "\nGenerated a new block:\n" );
    Blockchain::printBlockInfo( newBlock );
    Console::log( "\n" );

    // Block* minedBlock = Mining::mineSHA256( newBlock );
    Block* minedBlock = Mining::mineHybrid( newBlock );

    if (minedBlock == NULL)
        return;

    this->addBlockToChain( *minedBlock );
    delete minedBlock;
}

// TODO: Validate timestamps.
bool Blockchain::validateNewBlock( Block _BlockToValidate )
{
    if (this->chain->count(true) <= 0) return true;

    Console::log( "Validating new block..." );
    const json latestBlock = getLatestBlock();

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
    // else if (_BlockToValidate.hash != _BlockToValidate.calculateSHA256Hash())
    // TODO: Debug.
    // Problem here.
    else if (_BlockToValidate.hash != _BlockToValidate.calculateHybridHash(_BlockToValidate.targetBits))
    {
        Console::log( "Invalid hash." );
        return false;
    }

    Console::log( "The block is valid." );
    return true;
}

void Blockchain::replaceChain( std::vector<Block> _NewChain) 
{
    if (_NewChain.size() > this->chain->count(true))
    {
        leveldb::DestroyDB( DATABASE_BLOCKS, leveldb::Options() );

        unsigned long long int i;
        for (i = 0; i < _NewChain.size(); ++i) {
            this->chain->put( _NewChain[i].hash, _NewChain[i].toJson().dump() );
        }

        // Update cached block count.
        this->chain->count();
    }
}

std::string Blockchain::chainToJson( std::vector<Block> _Chain ) 
{
    std::vector<json> jsonChain;
    
    unsigned long long i;
    for (i = 0; i < _Chain.size(); ++i) {
        jsonChain.push_back( _Chain[i].toJson() );
    }

    json j_vec( jsonChain );

    return j_vec.dump();
}

void Blockchain::printBlockInfo( const Block _Block ) 
{
    std::cout << "Index: " << std::to_string( _Block.index )
              << "\nTimestamp: " << _Block.timestamp
              << "Hash: " << _Block.hash
              << "\nPrevious Hash: " << _Block.previousHash
              << "\nTarget Bits: " << _Block.targetBits
              << "\nNounce: " << _Block.nounce
              << "\nData: " << _Block.data
              << std::endl;
}
