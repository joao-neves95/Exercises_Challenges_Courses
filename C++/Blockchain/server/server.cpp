// Using Crow
// https://github.com/ipkn/crow
// Build Boost in VS2017 (14.1) - https://studiofreya.com/2017/04/23/building-boost-1-64-with-visual-studio-2017/
#include "server.hpp"
#include "..\libs\crow\include\crow.h"
#include "..\blockchain.hpp"
#include "..\models\block.hpp"
#include "..\config.hpp"

Server::Server()
{
    setRoutes();
}

Server::Server( bool _Run ) {
    setRoutes();

    if (_Run)
        this->run();
}

Server::~Server()
{
}

void Server::setRoutes() {
    CROW_ROUTE(app, "/").methods( crow::HTTPMethod::Get )
    ([] {
        crow::json::wvalue json;
        json["message"] = "Welcome to the (I still don't have a name) API.";

        return crow::response( json );
    });

    CROW_ROUTE(app, "/block/<int>").methods(crow::HTTPMethod::Get)
    ([](int reqIndex) {
        Blockchain* instance = Blockchain::getInstance();

        if (instance->chain.size() <= 0)
            return crow::response( 404 );

        if (instance->getLatestBlock().index < reqIndex)
            return crow::response( 404 );

        crow::json::wvalue jsonBlock = Blockchain::blockToJson( instance->chain[reqIndex] );

        return crow::response( jsonBlock );
    });
}

void Server::run() 
{
    if (ENVIRONMENT == kDevelopment)
        app.loglevel( crow::LogLevel::Debug );
    
    app.port( HTTP_PORT )
        .multithreaded()
        .run();
}
