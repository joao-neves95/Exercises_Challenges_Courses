#include <iostream>

#include "server\server.hpp"
#include "blockchain.hpp"
#include "utils\console.hpp"

/* run this program using the console pauser or add your own getch, system("pause") or input loop */

int main(int argc, char** argv)
{
    Blockchain::Blockchain();

    new Server( true );

	return 0;
}
