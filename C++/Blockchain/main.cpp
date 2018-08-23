#include <iostream>

#include "server\HttpServer.hpp"
#include "blockchain.hpp"
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

    // new HttpServer( true );

    system( "pause" );
	return 0;
}
