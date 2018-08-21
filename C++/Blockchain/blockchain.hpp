#pragma once
#include <list>
#include <vector>
using namespace std;

#include "block.hpp"

class Blockchain
{
    std::vector<Block> chain;
    bool genesisComplete = false;

    public:
        Blockchain();
        ~Blockchain();

        void genesis();

        Block getLatestBlock();

        void generateNextBlock(std::string _BlockData);

        bool validateNewBlock(Block _BlockToValidate);

        void replaceChain(std::vector<Block> newChain);
};
