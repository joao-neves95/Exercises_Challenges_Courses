# Idempotency

## Intro

- Same request executed multiple times produces the same result.
- Prevents duplicates during retries.

## Why

- Network retries.
- Timouts.
- Client resubmits.
- Message duplication.

## Advantages

- Avoids duplication during retries.
- Retries increase reliability; idenpotency ensures correctness.

## Example

- Payment API with idempotency key (a unique identifier sent with a request so that repeating the same request produces the same result only once, even if it’s received multiple times).

## Typical flow

1. Client generates unique key (UUID).
2. Server checks storage to see if key exists.
