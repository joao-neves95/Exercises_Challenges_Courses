// Build Boost in VS2017 (14.1) - https://studiofreya.com/2017/04/23/building-boost-1-64-with-visual-studio-2017/
// TODO: Migrate to Boost.Beast (already on libs) for a complete client/server.
// Boost.Beast repo: https://github.com/boostorg/beast.
//
// Using Crow
// https://github.com/ipkn/crow
#include "httpServer.hpp"
#include "..\libs\crow\include\crow.h"
#include "..\libs\json-3.2.0\single_include\nlohmann\json.hpp"
using namespace nlohmann;

#include "..\blockchain.hpp"
#include "..\utilities\console.hpp"
#include "..\models\block.hpp"
#include "..\config.hpp"

HttpServer::HttpServer()
{
    setHTTPRoutes();
    // setP2PRoutes();
}

HttpServer::HttpServer( bool _Run ) {
    setHTTPRoutes();
    //setP2PRoutes();

    if (_Run)
        this->run();
}

HttpServer::~HttpServer()
{
}

void HttpServer::setHTTPRoutes() {
    CROW_ROUTE(app, "/").methods( crow::HTTPMethod::Get )
    ([] {
        crow::json::wvalue json;
        json["message"] = "Welcome to the (I still don't have a name) API.";

        return crow::response( json );
    });

    CROW_ROUTE( app, "/blocks" ).methods( crow::HTTPMethod::Get )
    ([] {
        std::string jsonStrChain = Blockchain::chainToJson( Blockchain::getInstance()->chain );
        crow::json::wvalue jsonChain;
        jsonChain["chain"] = jsonStrChain;

        crow::response res;
        res.add_header( "Content-Type", "application/json" );
        res.write( jsonStrChain );
        // res.end();

        return res;
    });

    CROW_ROUTE(app, "/block/<int>").methods(crow::HTTPMethod::Get)
    ([](int reqIndex) {
        Blockchain* instance = Blockchain::getInstance();

        if (instance->chain.size() <= 0)
            return crow::response( 404 );

        if (instance->getLatestBlock().index < reqIndex)
            return crow::response( 404 );

        std::vector<json> jsonBlockVec;
        jsonBlockVec.push_back( Blockchain::blockToJson( instance->chain[reqIndex] ) );
        json j_vec( jsonBlockVec );
        crow::response res;
        res.add_header( "Content-Type", "application/json" );
        res.write( j_vec.dump() );
        // res.end();

        return res;
    });

    // Temporary route.
    CROW_ROUTE( app, "/mine/<int>" ).methods( crow::HTTPMethod::Get )
    ([]( int ammount = 1 ) {

        int i;
        for (i = 0; i < ammount; ++i) {
            Console::log( std::to_string( i + 1 ) );
            Blockchain::getInstance()->generateNewBlock( "Block number " + std::to_string( i + 1 ) );
        }

        return crow::response( 200 );
    });

    // Temporary route.
    CROW_ROUTE( app, "/server-name" ).methods( crow::HTTPMethod::Get )
    ([] {
        char name[256];
        return crow::response( gethostname(name, 256) );
    });
}

// This never gets called (commented out).
void HttpServer::setP2PRoutes() {
    CROW_ROUTE( app, "/ws" )
        .websocket()
        .onopen( [&]( crow::websocket::connection& conn ) 
        {
            CROW_LOG_INFO << "new websocket connection";
            std::lock_guard<std::mutex> _( mtx );
            users.insert( &conn );
        } )
        .onclose( [&]( crow::websocket::connection& conn, const std::string& reason ) 
        {
            CROW_LOG_INFO << "websocket connection closed: " << reason;
            std::lock_guard<std::mutex> _( mtx );
            users.erase( &conn );
        } )
        .onmessage( [&]( crow::websocket::connection& /*conn*/, const std::string& data, bool is_binary ) 
        {
            std::lock_guard<std::mutex> _( mtx );
            for (auto u : users) {
                if (is_binary)
                    u->send_binary( data );
                else
                    u->send_text( data );
            }
        } );
}

void HttpServer::run() 
{
    if (ENVIRONMENT == kDevelopment)
        app.loglevel( crow::LogLevel::Debug );
    
    app.port( SERVER_PORT )
        .multithreaded()
        .run();
}
