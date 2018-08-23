#pragma once
#include <string>

class Block {
    public:
        unsigned long long index;
        /** UTC asctime timestamp. */
        std::string timestamp;
        std::string hash;
        std::string previousHash;
        /** The difficulty at wich this block was mined. */
        unsigned int targetBits;
        long long nounce;
        std::string data;

        Block();
        /** Generates a block without hash (for mining). */
        Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data );
        Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data, std::string _Hash, long long _Nounce );

        ~Block();
};
