# C\#

## async vs thread blocking

- Using `async`/`await`
  - Enables non-blocking I/O.
  - Thread returns to thread pool while waiting.
  - Scales better under high concurrency (improves throughput).
- Blocking (.Result / Wait() )
  - Thread sits idle.
  - Can cause thread pool starvation and deadlocks.
- Rule: Always use `async` for I/O bound work.

## ASP.NET Middleware pipeline

- Ordered chain of request handlers.
- Each middleware can:
  - inspect requests
  - modify responses
  - short-circuit pipeline
    - e.g.: returning a cached response in middleware, bypassing subsequent stages.
- Common uses:
  - Auth
  - Logging
  - Exception handling

## DI lifetimes (Transient / Scoped / Singleton)

- Transient
  - New instance every injection.
  - For lightweight, stateless services.
- Scoped
  - One per request.
  - Typical for DbContext.
- Singleton
  - One instance for app lifetime.
  - Must be thread-safe.

## Entity Framework performance considerations

- Avoid N+1 queries
- Use projections (select), don't get what you don't need.
- .AsNoTracking for read-only queries.
- Proper indexing.
- Pagination.

## HttpClient guidelines and patterns of use

- Use IHttpClientFactory, which handles pooling and DNS updates, as to avoid socket exhaustion.
- The default IHttpClientFactory is configured as a singleton.
- Factory-created clients are intended to be short-lived

## Memory/performance considerations

- Avoid unnecessary allocations
- Async for I/O.
- Caching frequent reads.
- Pooling when needed (caches expensive-to-create resources—such as database connections, threads, or objects—for reuse rather than repeatedly creating and destroying them)
