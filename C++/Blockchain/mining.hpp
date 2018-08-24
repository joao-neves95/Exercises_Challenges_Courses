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
