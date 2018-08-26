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
//
// Using leveldb.
//

#pragma once
#include <string>
#include <vector>
#include "leveldb\db.h"

class DB
{
    private:
        leveldb::DB * db;
        std::string name;
        leveldb::Options openOptions;
        leveldb::ReadOptions readOptions;
        leveldb::Status status;
        bool cacheCount;

        unsigned long long int cachedCount;

        const void setDefaultOptions();

    public:
        /** 
            Use:
                // Open the database.
                DB* db = new DB(<std::string>[name] DATABASE_BLOCKS, <bool>[openDB] true);

                // Use the database.
                db->getDB()->[action]();

                // Close the database.
                delete db;

            Params:
                <std::string> name: The database name (DATABASE_BLOCKS), <bool> openDB: Whether to open the database or not. 
        */
        DB( const std::string _Name, const bool _OpenDB = true, const bool _CacheBlockCount = true );
        ~DB();

        /** Returns a pointer to the current DB instance. */
        leveldb::DB * getDB();

        void open();

        void dispose();

        /** 
            Get the total block count from the database.
            Note: "getCachedCount = false" is an expensive operation. It iterates throught all blocks on the database. It caches the count while doing this operation.
        */
        unsigned long long int count(const bool _GetCachedCount = false);
        
        /** It returns the last block key or "NULL" if unsuccessful status. */
        std::string getLastKey();
        
        /** It returns the last block value or "NULL" if unsuccessful status. */
        std::string getLastValue();

        std::vector<std::string> getAllValues();
        
        /** It returns the desired block value or "NULL" if unseccessful status. */
        std::string get( std::string _Key );
        
        /** It returns true or false depending on the success status. */
        bool put( std::string _Key, std::string _Value );

        /** It returns true or false depending on the success status. */
        bool DB::del( std::string _Key );
};
