# SOLID

## Intro

- Promoted by Uncle Bob (Robert Cecil Martin).
- Ensures the software is flexible, reusable, maintainable, understandable, by reducing coupling and increasing cohesion.
- Goals:
  - Easier change over time.
  - Safer extension of behaviour.
  - Better testability.
  - Clear separation of responsibilities.

## Principles

- S - Single responsibility principle:
  - Each class should have **only one responsibility**.
  - "A module should be responsible to one, and only one, actor."
  - Example: Separate business logic, persistence, validation, API handling.
- O - Open/closed principle:
  - Software entities should be **open for extension, but closed for modification**.
  - Ability to add new behaviour to a module without modifying existing code.
  - Approach: use interfaces, polymorphism, strategy pattern.
  - Polymorphic open–closed principle:
    - Refers to the use of abstracted interfaces, where the implementations can be changed and multiple implementations could be created and polymorphically substituted for each other.
- L - Liskov substitution principle:
  - **Subtypes must behave correctly when used in place of base types**.
  - Design by contract (**DbC**), using well defined interfaces.
  - Based on the concept of "substitutability".
  - It states that an object (such as a class) may be replaced by a sub-object (such as a class that extends the first class) without breaking the program.
- I - Interface segregation principle:
  - **Prefer many small interfaces over one large interface**.
  - Clients should only depend on what they need to use.
- D - Dependency inversion principle (DIP):
  - "Modules should **depend upon abstractions, not implementations**.".
  - For loosely coupled software modules.
  - Approach: Via interfaces/abstractions, through dependency injection
