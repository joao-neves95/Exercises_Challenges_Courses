# This entrypoint file to be used in development. Start by reading README.md
from unittest import main

import budget
from budget import create_spend_chart

food = budget.Category("Food")
food.deposit(1000, "initial deposit")
food.withdraw(10.15, "groceries")
food.withdraw(15.89, "restaurant and more food for dessert")
clothing = budget.Category("Clothing")
food.transfer(50, clothing)
clothing.withdraw(25.55)
clothing.withdraw(100)
auto = budget.Category("Auto")
auto.deposit(1000, "initial deposit")
auto.withdraw(15)

print(food)
print()
print(clothing)
print()
print(create_spend_chart([food, clothing, auto]))
print()

# Run unit tests automatically
main(module='test_module', exit=False)
