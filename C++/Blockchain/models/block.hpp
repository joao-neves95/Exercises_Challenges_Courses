#pragma once
#include <string>

class Block {
    public:
        unsigned long long index;
        /**
            UTC asctime timestamp.
        */
        std::string timestamp;
        std::string hash;
        std::string previousHash;
        std::string data;

        Block();
        /**
            Using this constructor, the hash needs to be added after the block instatiation.
        */
        Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data );
        Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data, std::string _Hash );

        ~Block();
};
