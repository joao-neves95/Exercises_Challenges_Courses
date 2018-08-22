#pragma once
#include <list>
#include <vector>
using namespace std;

#include "libs\crow\include\crow\json.h"
#include "models\block.hpp"

class Blockchain
{
    private:
        bool genesisComplete = false;

        static Blockchain* Blockchain::instance;
        static std::once_flag Blockchain::onceFlag;

        static void createInstance();

    public:
        std::vector<Block> chain;

        Blockchain();
        ~Blockchain();

        static Blockchain* getInstance();

        void genesis();

        Block getLatestBlock();

        void generateNextBlock(std::string _BlockData);

        std::string calculateBlockHash(Block _Block);

        bool validateNewBlock(Block _BlockToValidate);

        void replaceChain(std::vector<Block> newChain);

        static crow::json::wvalue blockToJson( Block _Block );

        static void printBlockInfo(Block _Block);
};
