#pragma once
// #include <mutex>
#include <unordered_set>
#include "..\libs\crow\include\crow.h"

class HttpServer
{
    private:
        crow::SimpleApp app;
        std::mutex mtx;;
        std::unordered_set<crow::websocket::connection*> users;

        void setHTTPRoutes();
        void setP2PRoutes();

    public:
        HttpServer();
        HttpServer( bool _Run );
        ~HttpServer();

        void run();
};

