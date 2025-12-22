# Redis

- "Remote Dictionary Server"
- Open-source
- NoSQL
- Key-value pairs, with high level data structures
- In-memory, which makes it highly performant (extremely fast), since there's no disk I/O.
- Can save data both in RAM or disk. Meaning it has optional persistence.
- Disadvantages:
  - Since it works synchronously, it does not reach the high level of performance that asynchronous alternatives reach *on a single instance*.
  - Needs RAM equivalent to size of data (since it's in-memory).
  - Does not support complex queries like relational DB.
  - **If a transaction fails (error), there is no return.**

[More info](../../../learning-notes/dev/microsoft-certs/backend-read-only-data-caching-with-redis.md)
