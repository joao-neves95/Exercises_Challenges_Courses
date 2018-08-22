#pragma once
// #include <mutex>
#include <unordered_set>
#include "..\libs\crow\include\crow.h"

class Server
{
    private:
        crow::SimpleApp app;
        std::mutex mtx;;
        std::unordered_set<crow::websocket::connection*> users;

        void setHTTPRoutes();
        void setP2PRoutes();

    public:
        Server();
        Server( bool _Run );
        ~Server();

        void run();
};

