from enum import Enum
from math import floor


class AmPm(Enum):
  AM = "AM"
  PM = "PM"


def is_am(hours_format_24: float):
  '''
  e.g: is_am(10.90) = true
  e.g: is_am(12.30) = false
  '''
  return hours_format_24 < 12


def convert_12hour_to_24hour(hours: float, am_pm: str):
  '''
  e.g: convert_12hour_to_24hour(11.20, "PM") = 23.20
  '''
  if am_pm == AmPm.PM.value:
    return hours + 12
  elif am_pm == AmPm.AM.value and hours >= 12 and hours < 13:
    return hours - 12
  else:
    return hours


def convert_24hour_to_12_hour(hours: int) -> tuple[int, str]:
  '''
  e.g.: convert_24hour_to_12_hour(13) = (1, "PM") \n
  e.g.: convert_24hour_to_12_hour(5) = (5, "AM") \n
  e.g.: convert_24hour_to_12_hour(0) = (12, "AM")
  '''
  if hours >= 13:
    return (hours - 12, AmPm.PM.value)
  elif hours == 12:
    return (12, AmPm.PM.value)
  else:
    return (12 if hours == 0 else hours, AmPm.AM.value)


def convert_minutes_to_hours(minutes: float) -> float:
  '''
  e.g.: convert_minutes_to_hours(90) = 1.5
  e.g.: convert_minutes_to_hours(60) = 1
  e.g.: convert_minutes_to_hours(30) = 0.5
  '''
  return minutes / 60


def convert_hours_to_minutes(hours: float) -> float:
  '''
  e.g.: convert_hours_to_minutes(0.50) = 30.0
  e.g.: convert_hours_to_minutes(1.50) = 90.0
  '''
  return hours * 60


def convert_hours_to_days(hours: float) -> float:
  '''
  e.g.: convert_hours_to_days(36) = 1.5
  e.g.: convert_hours_to_days(24) = 1
  e.g.: convert_hours_to_days(12) = 0.5
  '''
  return hours / 24


def fractional_part(decimal: float) -> float:
  '''
  e.g: fractional_part(2.05) = 0.05
  '''
  return decimal % 1


days_of_week = [
    "monday", "tuesday", "wednesday", "thursday", "friday", "saturday",
    "sunday"
]


def add_to_day_of_week(current_week_day: str, num_days_to_add: int):
  days_of_week_num = days_of_week.index(current_week_day.lower()) + 1

  total_num_of_week_days = days_of_week_num + num_days_to_add

  target_week_day_num = total_num_of_week_days - (
      7 * floor(total_num_of_week_days / 7))

  return days_of_week[target_week_day_num - 1].title()
