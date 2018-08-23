#pragma once
#include <iostream>
#include <string>
#include "console.hpp"

class Console
{
    public:
        static void log( std::string _Message ) {
            std::cout << _Message << std::endl;
        }

        static void logi( int _IntMessage ) {
            std::cout << _IntMessage << std::endl;
        }
};
