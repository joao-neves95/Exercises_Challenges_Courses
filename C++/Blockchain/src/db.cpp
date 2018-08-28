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

// TODO: Fix cached block count.

#include "db.hpp"
#include "utilities\console.hpp"
#include "config.hpp"

DB::DB( const std::string _Name, const bool _OpenDB, const bool _CacheBlockCount )
{
    //if (DB::db != NULL || DB::db != nullptr)
    //{
    //    if (ENVIRONMENT == kDevelopment)
    //        throw std::logic_error( "There is already an open connection. Close the connection before opening a new one." );
    //    else
    //        throw;
    //}

    // DB::db = NULL;
    this->name = _Name;
    this->setDefaultOptions();
    this->cacheCount = _CacheBlockCount;
    this->cachedCount = 0;

    if (_OpenDB)
        this->open();
}

DB::~DB()
{
    this->dispose();
}

const void DB::setDefaultOptions() 
{
    this->openOptions.create_if_missing = true;
    // this->openOptions.error_if_exists = true;
    this->readOptions.verify_checksums = true;
}

leveldb::DB * DB::getDB() 
{
    return this->db;
}

void DB::open()
{
    this->status = leveldb::DB::Open( this->openOptions, this->name, &this->db );
    
    if (!this->status.ok())
        return Console::log( "ERROR: There was an error opening the \"" + this->name + "\" database." );

    if (this->cacheCount)
        this->cachedCount = this->count();
}

void DB::dispose() 
{
    delete this->db;
    this->db = NULL;
}

unsigned long long int DB::count( const bool _GetCachedCount )
{
    if (_GetCachedCount)
        return this->cachedCount;

    this->cachedCount = 0;
    leveldb::ReadOptions rOptions = this->readOptions;
    rOptions.fill_cache = false;
    leveldb::Iterator* it = this->db->NewIterator( leveldb::ReadOptions( rOptions ) );

    for (it->SeekToFirst(); it->Valid(); it->Next()) {
        ++this->cachedCount;
    }

    return this->cachedCount;
}

std::string DB::getLastKey() 
{
    std::string key;
    return key;
}

std::string DB::getLastValue() 
{
    std::string value;
    return value;
}

std::vector<std::string> DB::getAllValues() 
{
    leveldb::ReadOptions rOptions = this->readOptions;
    rOptions.fill_cache = false;
    leveldb::Iterator* it = this->db->NewIterator( leveldb::ReadOptions( rOptions ) );
    
    std::vector<std::string> allValues;

    for (it->SeekToFirst(); it->Valid(); it->Next()) {
        allValues.push_back( it->value().ToString() );
    }

    return allValues;
}

std::string DB::get( std::string _Key ) 
{
    std::string value;
    leveldb::Status status = this->db->Get( leveldb::ReadOptions( this->readOptions ), _Key, &value );

    if (!status.ok())
        return NULL;

    return value;
}

bool DB::put( std::string _Key, std::string _Value ) 
{
    leveldb::Status status = this->db->Put( leveldb::WriteOptions(), _Key, _Value );

    if (!status.ok())
        return false;

    if (this->cacheCount)
        ++this->cachedCount;

    return true;
}

bool DB::del( std::string _Key ) 
{
    leveldb::Status status = this->db->Delete( leveldb::WriteOptions(), _Key );

    if (!status.ok())
        return NULL;

    if (this->cacheCount)
        --this->cachedCount;

    return true;
}
