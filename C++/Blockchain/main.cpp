#include <iostream>

#include "server\HttpServer.hpp"
#include "blockchain.hpp"
#include "utils\console.hpp"

/* run this program using the console pauser or add your own getch, system("pause") or input loop */

int main(int argc, char** argv)
{
    Blockchain::Blockchain();

    new HttpServer( true );

	return 0;
}
