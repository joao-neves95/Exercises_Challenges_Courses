# This entrypoint file to be used in development. Start by reading README.md
import sys
assert sys.version_info >= (3, 10)
from pytest import main
from arithmetic_arranger import arithmetic_arranger

print(
    arithmetic_arranger([
        # "32 / 698", "3801 - 2", "45 + 43", "123 + 49", "123 + 49", "123 + 49"
        '3801 - 2',
        '123 + 49'
    ]))

# Run unit tests automatically
main(['-vv'])
