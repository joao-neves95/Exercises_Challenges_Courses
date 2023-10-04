
def arithmetic_arranger(problems, show_result = False):
  if len(problems) > 5:
    return "Error: Too many problems."

  if show_result:
    arranged_problems = [[], [], [], []]
  else:
    arranged_problems = [[], [], []]

  for problem in problems:
    operation = problem.split()
    result = validate_operation(operation)

    if len(result) == 0:
      result = calculate_result(operation)
    else:
      return result

    longest_num_idx, longest_num_len = get_longest_num_operator(operation)
    if longest_num_idx == 0:
        operator_spaces_num = (longest_num_len + 1) - len(operation[2])
    else:
        operator_spaces_num = 1

    arranged_problems[0].append(f"{str_repeat(' ', longest_num_len + 2 - len(operation[0]))}{operation[0]}")
    arranged_problems[1].append(f"{operation[1]}{str_repeat(' ', operator_spaces_num)}{operation[2]}")
    arranged_problems[2].append(str_repeat("-", longest_num_len + 2))

    if show_result:
      arranged_problems[3].append(f"{str_repeat(' ', longest_num_len + 2 - len(result))}{result}")

  arranged_problems = "\n".join(map(lambda row : "    ".join(row), arranged_problems))
  return arranged_problems

def validate_operation(operation: list[str]) -> str:
  if len(operation) > 3:
      return "Error: Too many problems."

  if operation[1] not in "+-":
    return "Error: Operator must be '+' or '-'."

  if not is_digit(operation[0]) or not is_digit(operation[2]):
    return "Error: Numbers must only contain digits."

  if len(operation[0]) > 4 or len(operation[2]) > 4:
    return "Error: Numbers cannot be more than four digits."

  return ""

def get_longest_num_operator(operation: list[str]) -> tuple[int, int]:
  left, right = len(operation[0]), len(operation[2])

  if left > right:
    return (0, left)
  else:
    return (2, right)

def calculate_result(operation: list[str]) -> str:
  left, right = parse_operands(operation)

  match operation[1]:
    case "+":
      return str(left + right)
    case "-":
      return str(left - right)
    case _:
      return "Error: Operator must be '+' or '-'."

def parse_operands(operation: list[str]) -> tuple[int, int]:
  return (int(operation[0]), int(operation[2]))

def is_digit(s: str):
  return (s[1:] if s[0] in ('-', '+') else s).isdigit()

def str_repeat(s: str, times: int):
  # Source: https://waymoot.org/home/python_string
  return ''.join([s for i in range(times)])
