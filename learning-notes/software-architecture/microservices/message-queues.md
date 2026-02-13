# Message Queues

- For asynchronous communication between multiple servers.
- Use messaging queues when:
  - Async processing is needed.
  - Components should be loosely coupled.
  - You want resilience against spikes/failures.
- Benefits:
  - Queue buffers load.
  - Consumer failures don't lose messages.
  - Event-driven workflows.
- Example:
  - API -> publish event -> background worker process.

Read more: [RabbitMQ](../../technologies/rabbitmq.md)
