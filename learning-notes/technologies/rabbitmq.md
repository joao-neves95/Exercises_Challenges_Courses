# RabbitMQ
- Open source message broker software.
- It's a message queue system (e.g.: Apache Kafka, MsMQ, MS Azure Service Bus, ActiveMQ, etc.).
- publish/subscribe (pub/sub).
- Event-driven architecture.
- For **asynchronous** communication between multiple servers.
- Transmits information from a Publisher into a queue (FIFO - First Out, First Out) to then Consumers receive and consume those messages when ready to do so.
- It also functions as a temporary storage location for messages while the destination application is busy or unavailable.

## Main components

- Producer:
    - Source of the message.
|
- Broker
    - Exchange:
        - They control the routing of the messages from the Producer to the queues.
        - Each exchange type defines a specific routing algorithm specifying how messages should be delivered/published to the queues.
        - The structure that decides which queue to send the messages according to the routing type.
        - Types:
            - Direct:
                - Uses a **message routing key** (in the message header) to transport messages to queues.
                - A message is delivered to the queue with the binding key that exactly matches the message’s routing key.
            - Topic:
                - Messages are sent to one or more queues based on **wildcard matches between the routing key and the queue binding’s routing pattern**.
                - Messages are routed to one or more queues based on a pattern that matches a message routing key.
                - Consumers indicate which topics are of interest to them.
                - The consumer establishes a queue and binds it to the exchange using a routing pattern.
            - Fanout:
                - It **duplicates and routes** a received message to any associated queues, **regardless of routing keys or pattern matching**.
                - Applied in broadcasting systems (notify all clients).
                - Provided keys will be entirely ignored.
            - Header:
                - The messages are routed to the correct queue described in the header.
    |
    - Bindings:
        - The link between the exchange and the queues.
    |
    - Queues:
        - Where all incoming messages are stored.
        - FIFO model.
        - It is the memory.
        - Data can be persisted or stored in-memory.
        - Properties:
            - Name.
            - Durable: determines the lifetime of the queue; if messages are persisted in disk or stored in-memory. If the queue will survive a broker restart.
            - Exclusive: whether the queue will be used with multiple connections. If only one connection, the queue will be deleted when that connection closes.
            - AutoDelete: If set, the queue is deleted when all consumers have finished using it.
    |
- Consumer:
    - The server (app) that will receive and process the messages in the queue.
