# Design Requirements

---

## Availability

- Uptime / Uptime + downtime
- E.g.: 23h / 24h = 96%
- Commonly used to define SLOs
  - Service Level Objective

---

## Reliability

- The probability that a system won't fail.
- E.g. If there only exists 1 server instance, there is a higher probability of failing a request, then if it exists multiple instances.

## Fault Tolerance

- If a portion of a system as a fault, the system still continues to operate successfully.
- E.g. One server goes down, but there's another instance running.
- Fault tolerance improves reliability.

## Redundancy

- Having multiple copies of a service, even if not in use, so that if one fails, the other is operational, making so the system will continue to function.
- Having only one server creates a single point of failure - if it goes down, the entire system goes down.
- Redundancy makes fault tolerance possible.

---

## Throughput

- In general means the amount of operations/data that a system can handle over a period of time.
- E.g.:
  - client -> server communication: requests/second (RPS).
  - server -> DB communication: queries/second (QPS).
  - data: bytes/second.

## Latency

- The amount of time an operation takes to complete.
- The network influences server latency.
- Having multiple instances in different parts of the world reduces latency, because the server closest to the user can handle the request with less latency.

---
