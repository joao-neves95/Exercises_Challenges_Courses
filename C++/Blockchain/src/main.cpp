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

#include <iostream>
#include<vector>

#include "blockchain.hpp"
#include "db.hpp"
#include "utilities\console.hpp"

/* run this program using the console pauser or add your own getch, system("pause") or input loop */

int main(int argc, char** argv)
{
    Blockchain::Blockchain();

    // Mining simulation.
    int i;
    for (i = 0; i < 10; ++i) {
        Blockchain::getInstance()->generateNewBlock( "Block number " + std::to_string( i + 1 ) );
    }

    system( "pause" );
	return 0;
}
