#include <string>
#include <ctime>
using namespace std;

#include "block.hpp"
#include "..\crypto.hpp"

Block::Block() {}

Block::Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data ) {
    index = _Index;

    time_t now = time( 0 );
    tm *utc = gmtime( &now );
    timestamp = asctime( utc );

    previousHash = _PreviousHash;
    data = _Data;
}

Block::Block( unsigned long long _Index, std::string _PreviousHash, std::string _Data, std::string _Hash )
{
        index = _Index;

        time_t now = time(0);
        tm *utc = gmtime(&now);
        timestamp = asctime(utc);

        previousHash = _PreviousHash;
        data = _Data;

        hash = _Hash;
};

Block::~Block() {};
