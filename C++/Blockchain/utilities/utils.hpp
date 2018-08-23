#pragma once
#include <ctime>
#include <string>

#include "console.hpp"

class Utils 
{
    public:
        static const tm* getUTCTimestamp() {
            const time_t now = time( 0 );
            const tm* utc = gmtime( &now );

            return utc;
        }

        static std::string getUTCTimestampStr() {
            const tm* utc = Utils::getUTCTimestamp();

            return asctime( utc );
        }

        static std::string strRepeat( std::string _String, unsigned int _Times ) {
            std::string res = _String;

            unsigned int i;
            for (i = 0; i < _Times - 1; ++i) {
                res.append( _String );
            }

            return res;
        }

        static bool strStartsWith( std::string _String, std::string _RequiredStart ) {
            unsigned int i;
            for (i = 0; i < _RequiredStart.length(); ++i) {
                if (_String[i] == _RequiredStart[i])
                    continue;
                else
                    return false;
            }

            return true;
        }

        static std::string hexStrToBinary( std::string _String ) {
            string res;

            int i;
            for (i = 0; i < _String.length(); ++i) {
                res.append( Utils::hexCharToBinary( _String[i] ) );
            }

            return res;
        }

        static std::string hexCharToBinary( char _Char ) 
        {
            _Char = ::toupper( _Char );

            switch (_Char)
            {
                case '0': return "0000";
                case '1': return "0001";
                case '2': return "0010";
                case '3': return "0011";
                case '4': return "0100";
                case '5': return "0101";
                case '6': return "0110";
                case '7': return "0111";
                case '8': return "1000";
                case '9': return "1001";
                case 'A': return "1010";
                case 'B': return "1011";
                case 'C': return "1100";
                case 'D': return "1101";
                case 'E': return "1110";
                case 'F': return "1111";
            }
        }
};
