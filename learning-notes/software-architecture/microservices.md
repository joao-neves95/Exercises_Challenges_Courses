# Microservices

## What
- Small loosely-coupled business services that work together.
- Deployed autonomously/independently.
- They persist their own data or state.
- Since they are independent, they are technology agnostic.
- A team can update one service, without having to deploy the entire backend application again, only the required pieces.
- Each can live on a separate codebase.
- It should never be used for small applications.

## Monolithic Architecture Pros-Cons
- Easy to develop (for small projects), debug, end-to-end test, and deploy (single build system).
- Easy transaction management.
- A single big codebase. Tightly coupled.
- Complexity.
- Hard to maintain and make changes. Any change affects the entire system.
- Inability to apply new technologies.
- Hard and expensive to scale. You cannot scale components independently.

## Microservice Architecture Pros-Cons
- Independent services. Loosely coupled.
- Better scalability. Easy to multiplex.
- Teams can choose the appropriate technologies for each service.
- Easier to understand, maintain and add new features.
- Small, focused teams.
- Transaction management is very difficult.
- Challenging to manage and trace/metrics. It's much more complex.

## Communication types between microservices
- Request/Response:
    - HTTP APIs, gRPC.
    - Direct synchronous communication.
    - Impacts performance (each call adds latency) and reliability (an unresponsive service can impact an entire operation).
    - Can create deep chaining or deeply nested HTTP calls, which is simple to implement, but an **anti-pattern**.
        - This can be solved by using the Gateway Aggregation pattern.
- Event driven:
    - uServices communicate through a message queue system (event queues).
