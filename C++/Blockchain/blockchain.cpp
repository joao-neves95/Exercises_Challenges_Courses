#include <string>

#include "blockchain.hpp"
#include "block.hpp"
#include "utils\console.hpp"

Blockchain::Blockchain()
{
}

Blockchain::~Blockchain()
{
}

void Blockchain::genesis() 
{
    if (genesisComplete)
        return;

    const Block genesisBlock = Block::Block(0, "0", "The GENESIS block.");
    chain.push_back( genesisBlock );
}

Block Blockchain::getLatestBlock() {
    return chain[chain.size() - 1];
}

void Blockchain::generateNextBlock(std::string _BlockData) 
{
    const Block latestBlock = getLatestBlock();
    const Block newBlock = Block::Block(latestBlock.index + 1, latestBlock.hash, _BlockData);
    chain.push_back( newBlock );
}

bool Blockchain::validateNewBlock(Block _BlockToValidate) 
{
    Console::log("Validating new block...");
    const Block latestBlock = getLatestBlock();

    if (_BlockToValidate.index != latestBlock.index + 1)
    {
        Console::log("Invalid index.");
        return false;
    }
    else if (_BlockToValidate.previousHash != latestBlock.previousHash)
    {
        Console::log("Invalid previous hash.");
        return false;
    }
    else if (_BlockToValidate.hash != _BlockToValidate.calculateHash())
    {
        Console::log("Invalid hash.");
        return false;
    }

    return true;
}

void Blockchain::replaceChain(std::vector<Block> newChain) {
    if (newChain.size() > chain.size())
    {
        chain = newChain;
    }
}
