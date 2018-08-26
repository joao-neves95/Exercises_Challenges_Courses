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

#pragma once
#include <list>
#include <vector>
using namespace std;

#include "libs\json-3.2.0\single_include\nlohmann\json.hpp"
using namespace nlohmann;

#include "db.hpp"
#include "models\block.hpp"

class Blockchain
{
    private:
        bool genesisComplete = false;

        static Blockchain* Blockchain::instance;
        static std::once_flag Blockchain::onceFlag;

        static void createInstance();

    public:
        // std::vector<Block> chain;
        DB* chain;

        Blockchain();
        ~Blockchain();

        static Blockchain* getInstance();

        void genesis();

        json getLatestBlock();

        void addBlockToChain( const Block _Block );

        void generateNewBlock(const std::string _BlockData);

        static std::string getBlockData( const Block _Block );

        static std::string calculateBlockHash(const Block _Block);

        bool validateNewBlock(const Block _BlockToValidate );

        void replaceChain(const std::vector<Block> _NewChain);

        static json blockToJson(const Block _Block);

        static std::string chainToJson(const std::vector<Block> _Chain);

        static void printBlockInfo(const Block _Block);
};
