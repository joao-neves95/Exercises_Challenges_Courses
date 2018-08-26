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

