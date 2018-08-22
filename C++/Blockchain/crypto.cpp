#include <string>

#include "crypto.hpp"
#include "libs\cryptopp700\dll.h"
#include "libs\cryptopp700\sha.h"
#include "libs\cryptopp700\hex.h"

Crypto::Crypto()
{
}


Crypto::~Crypto()
{
}

// TODO: Fix criptopp lib.
std::string Crypto::toSha256Str(std::string data) {
    CryptoPP::byte digest[CryptoPP::SHA256::DIGESTSIZE];
    CryptoPP::SHA256().CalculateDigest( digest, (CryptoPP::byte*)data.c_str(), data.length() );

    CryptoPP::HexEncoder encoder;

    std::string outputHashStr;
    encoder.Attach( new CryptoPP::StringSink( outputHashStr ) );
    encoder.Put( digest, sizeof( digest ) );
    encoder.MessageEnd();

    return outputHashStr;
}
