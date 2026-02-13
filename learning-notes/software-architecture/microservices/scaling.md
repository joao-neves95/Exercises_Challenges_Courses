# Scaling Microservices handling high traffic

There are multiple strategies to scale and API handling high traffic:

- Caching:
  - On frequently read resources.
  - Reduce database/API load.
  - Culminates in faster responses.
  - E.g. memory cache; Redis.
- Message queues:
  - For heavy async work.
  - **Decouples** components.
  - Process work asynchronously.
  - Smoother traffic spikes.
  - Also improves **resilience**, as consumer failures don't delete messages.
  - Read more: [Message Queues](message-queues.md)
- Horizontal scaling:
  - Multiple app/API instances, managed through a load balancer.
- Idempotency:
  - Same request can be repeated safely.
  - E.g. Retry won't duplicate transaction.
    - This enables safe retries, which is critical in distributed systems.
- Retries:
  - Automatically retry transient failures (timeouts, network).
  - Improves reliability without user intervention.
  - Use exponential backoff.
- Database indexing
- Pagination
- Reduce network calls
- Profile before optimizing; Implement observability (logs + metrics).
