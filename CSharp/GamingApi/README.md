# YLD Gaming API  

Yld Home Code Challenge (Backend C#)

This is the skeleton project that you must use for the Home Code Challenge

The target framework is .NET 6, uses Swagger (https://swagger.io/), and has Nullables and NETAnalyzers enabled

Feel free to structure the rest as you think it's better

Don't forget to read carefully the code challenge instructions that were sent to you!

## Architecture
- WebApi
    - The application/consumer layer.
- WebApi.ClientSdk
    - A C# SDK client for API data consumption.
    - Generated with NSwag.
    - Could be used directly by other microservices.
- Contracts
    - All contracts (interfaces, models, etc.) that connect all consumers to the Core layer.
- Core
    - The Core/domain.
    - Here lies all domain logic. At the moment it is consumed by the an API, but it could even be consumed directly by a desktop application, for example, without any changes in behavior.
- Infrastructure
    - This is all the infrastructure dependent code. Code dependent on hardware, networking, frameworks/libraries, etc.
    - Infrastructure must only be directly used by Core. The consumer only configures which infrastructure implementations the Core is going to use.
- Infrastructure.Contracts
    - Infrastructure dependent entities.
- Tests
    - Unit tests.

## Links
- Implementation details: https://public.3.basecamp.com/p/PCAoo4bL5xCxQbV7E9hPaQJp
- JSON feed: https://yld-recruitment-resources.s3.eu-west-2.amazonaws.com/steam_games_feed.json

## Libraries
- https://www.newtonsoft.com/json/help/html/Introduction.htm
    - https://www.newtonsoft.com/json/help/html/Performance.htm
- https://github.com/App-vNext/Polly
- https://github.com/RicoSuter/NSwag
- Testing
    - https://github.com/fluentassertions/fluentassertions
    - https://github.com/AutoFixture/AutoFixture
    - https://github.com/moq/moq4

---
