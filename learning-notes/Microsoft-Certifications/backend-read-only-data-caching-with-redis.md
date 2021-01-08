# [Backend Read-Only Data Caching with Redis](https://docs.microsoft.com/en-us/learn/modules/optimize-your-web-apps-with-redis/)

## Caching

- Caching is storing frequently accessed data in memory.
- Used to increase performance and reduce server load.

## Redis cache

- Redis (**RE**mote **DI**ctionary **S**erver) cache is an open-source, in-memory key value pair store.
- Fast.
- Can store and manipulate common data types such as strings, hashes, and sets.

## Azure Cache for Redis

- Redis PaaS.
- Gives access to secure, dedicated Redis cache, managed by MSFT.
- Accessible from any app within Azure.
- The cached data is located in-memory on an Azure server, opposed to being loaded from disk by a database.

## Types of Supported data

- Binary-safe strings (most common: strings, contents of an image file, etc.)
- Lists of strings.
- Unordered sets of strings.
- Hashes.
- Sorted sets of string.
- Maps of strings.

## Redis key

- Are binary safe strings.
- Used to look up the value from the cache.
- Best to avoid long keys. They take up more memory and longer loockup times.
- Maximum key size is 512 MB, but use **ALLAYS** less than that.
- Good practice/convention: "object:id", as in "sport:football".
  - Bad: "fb:8-2-2"
  - Good: "sport:football;date:2008-02-02"

## Redis caching architectures

- It's how we distribute our data in the cache.
- Single node:
  - The complete dataset is stored in a single node.
  - (An instance).
- Multiple node:
  - Node cache replication in two-node configuration, (primary/secondary, I.e.: master/slave).
- Clustered:
  - A cluster shares data across multiple nodes to increase memory availability.
  - It is a built-mode of operation for the Redis nodes themselves.
  - The nodes handle all the sharding, replication and routing.
