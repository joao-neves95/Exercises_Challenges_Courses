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
        /** Consensus. The mining difficulty. */
        static unsigned int getTargetBits();

        static bool hashMatchesTargetBits( std::string _Hash );

        /** The mine function. The proof of work. */
        static Block * mine( const Block _Block );
};
