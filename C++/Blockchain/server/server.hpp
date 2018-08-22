#pragma once
#include "..\libs\crow\include\crow.h"

class Server
{
    private:
        crow::SimpleApp app;
        void setRoutes();

    public:
        Server();
        Server( bool _Run );
        ~Server();

        void run();
};

