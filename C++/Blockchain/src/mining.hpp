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
#include <string>

#include "models\block.hpp"
#include "config.hpp"

class Mining
{
    private:
        Mining();
        ~Mining();

    public:
        /** Consensus. The mining target difficulty (for SHA256). */
        static unsigned int getTargetBits();

        /** Consensus. The mining target difficulty (for Argon2d). */
        static unsigned int getTargetDifficulty();

        static bool hashMatchesTargetBits( std::string _Hash );

        /** The mine function. The proof of work. */
        static Block * mineArgon2d( const Block _Block );

        static Block * mineSHA256( Block _Block );

        static Block * mineHybrid( Block _Block );
};
