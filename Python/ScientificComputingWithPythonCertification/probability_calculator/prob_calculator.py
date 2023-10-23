import copy
import random
from typing import Any


class Hat:
  contents: list[str] = []

  def __init__(self, **kwargs) -> None:
    self.contents = occurrence_dict_to_list(kwargs)

  def draw(self, num_of_balls: int) -> list[str]:
    if num_of_balls > len(self.contents):
      return [self.contents.pop() for _ in range(len(self.contents))]

    response = []
    for _ in range(num_of_balls):
      ball = None
      index = -1
      # Edge case: protect from repeated numbers.
      while ball is None:
        index = random.randint(0, len(self.contents) - 1)
        ball = try_get(self.contents, index, None)
      response.append(self.contents.pop(index))

    return response


def experiment(hat: Hat, expected_balls: dict[str, int], num_balls_drawn: int,
               num_experiments: int):
  frequence = 0

  for _ in range(num_experiments):
    hat_copy = copy.deepcopy(hat)
    drawn_occurrence_dict = occurrence_list_to_dict(
        hat_copy.draw(num_balls_drawn))

    if (has_minimum_occurrence(expected_balls, drawn_occurrence_dict)):
      frequence += 1

  return frequence / num_experiments


def has_minimum_occurrence(minimum_occurrence_dict: dict[str, int],
                           occurrence_dict_b: dict[str, int]) -> bool:
  for category, min_count in minimum_occurrence_dict.items():
    if occurrence_dict_b.get(category, 0) < min_count:
      return False

  return True


def occurrence_dict_to_list(occurrence_dict: dict[str, int]) -> list[str]:
  occurrence_list = []
  for category, occurrence in occurrence_dict.items():
    occurrence_list.extend([category for _ in range(occurrence)])

  return occurrence_list


def occurrence_list_to_dict(occurrence_list: list[str]) -> dict[str, int]:
  occurrence_dict = {}
  for category in occurrence_list:
    occurrence_dict[category] = occurrence_dict.get(category, 0) + 1

  return occurrence_dict


def try_get(target: list[Any], index: int, default) -> Any:
  try:
    return target[index]
  except IndexError:
    return default
