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

        /** Log hex. */
        static void logx( uint8_t _IntMessage ) {
            std::cout << std::hex << _IntMessage;
        }

        /** Log hex. */
        static void logxLine( uint8_t _IntMessage ) {
            std::cout << std::hex << _IntMessage << std::endl;
        }
};
