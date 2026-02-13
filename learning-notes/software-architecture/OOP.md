# Object Oriented Programming

- **A programming paradigm that organises software around "objects", that bundle data/state (properties) and behaviour (methods) together, modelling domain (real-world) concepts.**
- Why:
  - Reduce complexity
  - Separate concerns
  - Manage change
- E.g. of languages that support OOP: C++, C#, Fortran 2003, Java, JavaScript, Kotlin, MATLAB, Objective-C, Perl, PHP, Python, R, Ruby, Scala, Swift, etc.

## Core Principles

- Encapsulation:
  - To hide internal state.
  - Prevent invalid usage.
  - Reduce coupling.
  - Controlling access to certain data/attributes.
  - E.g.:
    - keeping a car's engine private.
    - Private fields + public methods.
- Abstraction:
  - Expose only what users need to know.
  - Hide complex implementation details behind interfaces and APIs.
  - E.g.: to use a car's steering wheel without knowing the engine mechanics.
- Inheritance:
  - Create new types based on existing ones.
  - To reuse code/logic from parent objects.
  - Modern practice warning: Prefer composition over inheritance when possible.
  - E.g.: `ElectricCar` inherits from `Car`.
- Polymorphism:
  - Different objects respond to the same interface differently.
  - The ability for objects to take on many forms.
  - Benefits:
    - extensibility
    - cleaner conditional logic.
  - E.g. a `.drive()` method that works differently for a ``Car` and a `Boat`.

## Related

- [SOLID](./SOLID.md)
