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
    return 8;
}

unsigned int Mining::getTargetDifficulty()
{
    // Hard coded for now (testing).
    return 1000;
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
            return new Block( _Block.index, _Block.previousHash, _Block.data, currentHashAttempt, _Block.nounce );
        }

        ++_Block.nounce;
    }

    Console::log( "Unsuccessful mining." );
    return NULL;
}

/** NOT TESTED YET. */
Block * Mining::mineArgon2d( Block _Block )
{
    Console::log( "\nMining the new block containing \"" + _Block.data + "\".\n" );
    unsigned int difficulty = Mining::getTargetDifficulty();
    std::string targetHash = Crypto::toArgon2dHexStr( _Block.getData(), difficulty );

    string currentHashAttempt;

    // "_I64_MAX" is for preventing any possible overflows.
    while (_Block.nounce < _I64_MAX) {
        currentHashAttempt = _Block.calculateArgon2dHexHash( difficulty );

        if (currentHashAttempt == targetHash) {
            Console::log( "Successfully mined the new block.\n\n" );
            return new Block( _Block.index, _Block.previousHash, _Block.data, currentHashAttempt, _Block.nounce );
        }

        // The nounce here serves as the memory usage in kibibytes to hash the block data.
        ++_Block.nounce;
    }

    Console::log( "Unsuccessful mining." );
    return NULL;
}

/** NOT TESTED YET. */
Block * Mining::mineHybrid( Block _Block )
{
    Console::log( "\nMining the new block containing \"" + _Block.data + "\".\n" );
    unsigned int difficulty = Mining::getTargetDifficulty();

    string currentHashAttempt;

    // "_I64_MAX" is for preventing any possible overflows.
    while (_Block.nounce < _I64_MAX) {
        // SHA256( Argon2d( blockData ) + blockData.nounce ), where blockData.nounce, including the blockData inside Argon2d, increments until it matches the target bits.
        // See: .hashMatchesTargetBits(hash).
        currentHashAttempt = _Block.calculateHybridHash( difficulty );

        if (Mining::hashMatchesTargetBits( currentHashAttempt )) {
            Console::log( "Successfully mined the new block.\n\n" );
            return new Block( _Block.index, _Block.previousHash, _Block.data, currentHashAttempt, _Block.nounce );
        }

        ++_Block.nounce;
    }

    Console::log( "Unsuccessful mining." );
    return NULL;
}
