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

// https://jeiwan.cc/posts/building-blockchain-in-go-part-2/
//
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

unsigned int Mining::getTargetBits()
{
    // Hard coded for now (testing).
    return 5;
}

unsigned int Mining::getTargetDifficulty()
{
    // Hard coded for now (testing).
    return 500;
}

bool Mining::hashMatchesTargetBits( std::string _Hash )
{
    const string hashBinary = Utils::hexStrToBinary( _Hash );
    const string requiredPrefixZeros = Utils::strRepeat( "0", Mining::getTargetBits() );

    // The mining hash attempt must have the required 0's (target bits) at the start of its binary form.
    return Utils::strStartsWith( hashBinary, requiredPrefixZeros );
}

Block * Mining::mineSHA256( Block _Block )
{
    Console::log( "\nMining the new block containing \"" + _Block.data + "\".\n" );

    string currentHashAttempt;

    // "_I64_MAX" is for preventing any possible overflows.
    while (_Block.nounce < _I64_MAX) {
        // SHA256( data ), where data.nounce increments until it matches the target bits.
        // See: .hashMatchesTargetBits(hash).
        currentHashAttempt = _Block.calculateSHA256Hash();

        if (Mining::hashMatchesTargetBits( currentHashAttempt )) {
            Console::log( "Successfully mined the new block.\n\n" );
            return new Block( _Block.index, _Block.timestamp, _Block.previousHash, _Block.data, currentHashAttempt, _Block.nounce );
        }

        ++_Block.nounce;
    }

    Console::log( "Unsuccessful mining." );
    return NULL;
}

/** 
    The hybrid mining algorithm. It's a combination of Argon2d hashing and SHA256 PoW.
    SHA256( Argon2d( blockData ) + blockData.nounce ), where blockData.nounce, including the nounce inside Argon2d's blockData, increments until it matches the target bits.
*/
Block * Mining::mineHybrid( Block _Block )
{
    Console::log( "\nMining the new block containing \"" + _Block.data + "\".\n" );

    string currentHashAttempt;

    // "_I64_MAX" is for preventing any possible overflows.
    while (_Block.nounce < _I64_MAX) {
        // SHA256( Argon2d( blockData ) + blockData.nounce ), where blockData.nounce, including the nounce inside Argon2d's blockData, increments until it matches the target bits.
        // See: .hashMatchesTargetBits(hash).
        _Block.argonHash = _Block.calculateArgon2dEncodedHash( _Block.difficulty );
        currentHashAttempt = _Block.calculateHybridHash();

        if (Mining::hashMatchesTargetBits( currentHashAttempt )) {
            Console::log( "Successfully mined the new block.\n\n" );
            return new Block( _Block.index, _Block.timestamp, _Block.previousHash, _Block.data, currentHashAttempt, _Block.argonHash, _Block.nounce );
        }

        ++_Block.nounce;
    }

    Console::log( "Unsuccessful mining." );
    return NULL;
}
