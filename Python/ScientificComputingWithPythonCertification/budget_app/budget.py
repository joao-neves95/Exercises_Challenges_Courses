from math import floor


class Category:
  name: str
  ledger: list

  def __init__(self, name: str):
    self.name = name
    self.ledger = []

  def __create_ledger_item(self,
                           amount: float,
                           description: str | None = None):
    return {
        "amount": amount,
        "description": "" if description is None else description
    }

  def deposit(self, amount: float, description: str | None = None):
    self.ledger.append(self.__create_ledger_item(amount, description))

  def withdraw(self, amount: float, description: str | None = None):
    if not self.check_funds(amount):
      return False

    self.ledger.append(self.__create_ledger_item(-amount, description))
    return True

  def get_balance(self) -> float:
    total_balance = 0

    for ledgerItem in self.ledger:
      total_balance += ledgerItem["amount"]

    return total_balance

  def transfer(self, amount: float, target_budget_category) -> bool:
    if not self.check_funds(amount):
      return False

    self.withdraw(amount, f"Transfer to {target_budget_category.name}")
    target_budget_category.deposit(amount, f"Transfer from {self.name}")
    return True

  def check_funds(self, amount: float):
    return amount <= self.get_balance()

  def __str__(self) -> str:
    return self.to_str()

  def to_str(self) -> str:
    max_desc_item_idx, max_item_desc_len = self.__find_max_ledger_description_len(
    )
    max_amount_len = len(
        Category.__format_amount(self.ledger[max_desc_item_idx]['amount']))

    table = ""
    for ledger_item in self.ledger:
      amount = Category.__format_amount(ledger_item['amount'])
      desc = Category.__format_description(
          ledger_item['description']).ljust(max_item_desc_len -
                                            (len(amount) - max_amount_len))
      table += f"{desc} {amount}\n"

    table = f"{self.name.center(max_item_desc_len + len(' ') + max_amount_len, '*')}\n{table}Total: {self.get_balance()}"
    return table

  def __find_max_ledger_description_len(self) -> tuple[int, int]:
    '''
    Returns: (index, length)
    '''
    max_desc_len = (0,
                    len(
                        Category.__format_description(
                            self.ledger[0]["description"])))

    for i in range(len(self.ledger))[1:]:
      this_ledger_item_desc_len = len(
          Category.__format_description(self.ledger[i]["description"]))
      if this_ledger_item_desc_len > max_desc_len[1]:
        max_desc_len = (i, this_ledger_item_desc_len)

    return max_desc_len

  @staticmethod
  def __format_description(description: float):
    return description[0:23]

  @staticmethod
  def __format_amount(amount: float):
    return f"{amount:.2f}"[0:7]


def create_spend_chart(categories: [Category]):
  spend_by_category = __get_percentage_spent_by_category(categories)

  chart = []
  for i in reversed(range(0, 110, 10)):
    row = [f"{str(i).rjust(3)}|"]
    i_col = 0
    for _, spend in spend_by_category.items():
      if spend >= i:
        row.append(" o " if i_col < len(spend_by_category) - 1 else " o  ")
      else:
        row.append("   " if i_col < len(spend_by_category) - 1 else "    ")
      i_col += 1
    chart.append(row)

  chart = table_to_string(chart, "")
  chart = f"Percentage spent by category\n{chart}\n{' ' * 4}{'---' * len(categories)}-"

  max_category_name = None
  all_category_names = list(spend_by_category.keys())
  for i in range(len(all_category_names)):
    if max_category_name is None or len(
        all_category_names[i]) > max_category_name[1]:
      max_category_name = (i, len(all_category_names[i]))

  x_axis_names_table = []
  for iRow in range(max_category_name[1]):
    row = []
    for iCol in range(len(all_category_names)):
      if iCol == 0:
        row.append(" " * 5)
      row.append(try_get(all_category_names[iCol], iRow, " ").ljust(3))
    x_axis_names_table.append(row)

  chart = f"{chart}\n{table_to_string(x_axis_names_table, '')}"
  return chart


def __get_percentage_spent_by_category(categories: [Category]):
  total_spend = 0
  spend_by_category = {}

  for category in categories:
    category_spent = 0
    for budget_item in category.ledger:
      if budget_item["amount"] < 0:
        category_spent += budget_item["amount"] * -1
    spend_by_category[category.name] = category_spent
    total_spend += category_spent

  for category, spend in spend_by_category.items():
    spend_by_category[category] = int(
        convert_to_percentage_of_total(spend, total_spend))

  return spend_by_category


def convert_to_percentage_of_total(value: float, total: float):
  return (value * 100) / total


def try_get(target_str: str, index: int, default: str):
  try:
    return target_str[index]
  except IndexError:
    return default


def table_to_string(table: [[]], column_separator: str):
  return "\n".join((column_separator.join(row) for row in table))
