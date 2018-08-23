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
        static bool hashMatchesTargetBits( std::string _Hash );

        /** The mine function. The proof of work. */
        static Block * mine( const Block _Block );
};
