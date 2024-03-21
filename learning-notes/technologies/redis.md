# Redis
- Open-source
- NoSQL
- "Remote Dictionary Server"
- Key-value pairs, with high level data structures
- In-memory
- Fast
- Can save data both in RAM or disk
- Disadvantages
    - Since it works synchronously, it does not reach the high level of performance that asynchronous alternatives reach *on a single instance*.
    - Needs RAM equivalent to size of data (since it's in-memory).
    - Does not support complex queries like relational DB.
    - **If a transaction fails (error), there is no return.**

[More info (MSFT Certification)](../Microsoft-Certifications/backend-read-only-data-caching-with-redis.md)
