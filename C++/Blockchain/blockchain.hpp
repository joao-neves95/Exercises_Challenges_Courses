#pragma once
#include <list>
#include <vector>
using namespace std;

#include "libs\json-3.2.0\single_include\nlohmann\json.hpp"
using namespace nlohmann;
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

        void generateNextBlock(const std::string _BlockData);

        std::string calculateBlockHash(const Block _Block);

        bool validateNewBlock(const Block _BlockToValidate );

        void replaceChain(const std::vector<Block> newChain);

        static json blockToJson(const Block _Block);

        static std::string chainToJson(const std::vector<Block> _Chain);

        static void printBlockInfo(const Block _Block);
};
