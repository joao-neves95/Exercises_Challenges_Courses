// https://jeiwan.cc/posts/building-blockchain-in-go-part-2/
#include "mining.hpp"
#include <limits>
using namespace std;

#include "libs\boost_1_68_0\boost\convert.hpp"
#include "blockchain.hpp"
#include "utilities\crypto.hpp"
#include "utilities\utils.hpp"
#include "utilities\console.hpp"
#include "config.hpp"

Mining::Mining()
{
}


Mining::~Mining()
{
}

bool Mining::hashMatchesTargetBits( std::string _Hash ) 
{
    const string hashBinary = Utils::hexStrToBinary( _Hash );
    const string requiredPrefixZeros = Utils::strRepeat( "0", TARGET_BITS );

    return Utils::strStartsWith( hashBinary, requiredPrefixZeros );
}

Block * Mining::mine( Block _Block ) 
{
    Console::log( "\nMining the new block containing \"" + _Block.data + "\".\n" );

    // Counter.
    long long nounce = 0;
    string currentHashAttempt;

    // "_I64_MAX" is for preventing any possible overflows.
    while (nounce < _I64_MAX) {
        currentHashAttempt = Blockchain::calculateBlockHash( _Block );

        if (Mining::hashMatchesTargetBits( currentHashAttempt )) {
            Console::log( "Successfully mined the new block.\n\n" );
            return new Block( _Block.index, _Block.previousHash, _Block.data, currentHashAttempt, _Block.nounce );
        }

        // ++nounce;
        ++_Block.nounce;
    }

    Console::log( "Unsuccessful mining." );
    return NULL;
}
