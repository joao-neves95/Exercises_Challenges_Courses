# Algorithmic complexity / Big-O / Asymptotic analysis

## Asymptotic Notations

- [Mathematical] languages that allows for the analysis of an algorithm's running time.
- Done by **\*identifying its behaviour** as the input size of the algorithm increases\* (the algorithm's growth rate).
- How much the algorithm slows down as the input grows bigger.
- **The analysis is done by calculating how many operations/steps the algorithms needs to perform**.
- It relates to **scalability** of a program - as input grows larger, does the algorithm scale?
- The growth analysis can be done on:
  - Time.
  - Memory [consumption].
- Why even do this?
  - It saves companies a lot of money, when their developers know how to write efficient code.
- What is good code:
  - Readable:
    - Clean.
    - Others can read it.
  - Scalable:
    - Code that can scale when input scales (memory/speed).
    - Efficient (Big O).

### Types

- Best case:
  - Not typically evaluated because the best case conditions are not really planned for.
- **Worst case**:
  - "Big-O", "O".
  - "Ordnung", order of approximation.
  - The most common way of algorithm analysis.
  - The standard way of measuring the time and space that an algorithm will consume as the input grows.
  - The ceiling (**upper bound**) growth for a given function (**asymptotic upper bound**).
- Average case
- (etc.)

## The Big O notation

`f(n)`

Where: - `f`: The algorithm. - `n`: The input size. - `f(n)`: The running time.

### Big O simplification

1. Worst case
2. Remove constants
3. Different terms for each input (e.g.: multiple collections - O(`a` + `n`))
4. Drop non dominants input (e.g.: multiple collections - O(`a` + `n`))

### Types of functions, limits

From slowest growing function to fastest growing function.

- Constant:
  - `O(1)`
  - No loops.
  - E.g.: Accessing an hashtable/hashmap.
- [Logarithmic](./glossary/logarithms.md):
  - `O(log n)`
  - The complexity goes up linearly while the input goes up exponentially.
  - E.g.: Usually searching algorithms of sorted items - binary search.
- Linear:
  - O(`an + b`) == **`O(n)`**
  - E.g.: For each iteration, there exists a loop though `n` items.
  - Iterating through half a collection is still **`O(n)`**.
- Quadratic:
  - O(`n * n`) == **`O(n^2)`**
  - O(`an^2 + bn + c`) == **`O(n^2)`**
  - E.g.: 2 nested loops.
  - Every element in a collection needs to be compared to other elements.
- Polynomial:
  - O(`an^z + ... + an^2 + a*n^1 + a*n^0`) == **`O(n^z)`**
    - Where `z` is a constant.
- Exponential:
  - **`O(2^n)`**
  - The complexity doubles with each addition to the input dataset.
  - E.g.: Looping over all possible combinations of an array; Recursive algorithms that solve a problem of size `n` - recursive Fibonacci.
- Factorial:
  - **`O(n!)`**
  - E.g.: Adding a loop for every item in the dataset.
  - There's something very wrong with code with factorial complexity.

## Space Complexity

- How does storage space increase (variables/memory) with the increase of the input.
- How much memory is being use.
- What increased space complexity:
  - Adding variables.
  - Adding new data structures.
  - Adding more function calls.
  - Memory allocations.
- A program stores data in 2 ways:
  - Heap:
    - Data dynamic allocated (E.g.: `malloc()`).
    - One heap per application (typically).
  - Stack:
    - Faster than storing on the heap.
    - Always LIFO.
    - Function calls are stored on the stack.
    - Each thread gets a stack.

## Trade-offs

- Usually there is a trade-off between speed (CPU) and memory/space (RAM).
  - > speed => > memory
  - < memory => < speed
- Trade offs between speed, memory and readability.
