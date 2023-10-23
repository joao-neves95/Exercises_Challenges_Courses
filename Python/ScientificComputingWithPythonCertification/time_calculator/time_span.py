from math import floor

from calculations import (
    add_to_day_of_week,
    convert_12hour_to_24hour,
    convert_24hour_to_12_hour,
    convert_hours_to_days,
    convert_hours_to_minutes,
    convert_minutes_to_hours,
    fractional_part,
)


# WHAT THE FUCK IS TIME ANYWAY??
class TimeSpan:
  start_time_24h: float
  total_time_24h: float
  end_time_24h: float

  num_of_days_between: int

  start_day_of_week: str | None = None
  end_day_of_week: str | None = None

  def __init__(self, time_str: str, current_day_of_week: str | None = None):
    self.num_of_days_between = 0
    self.start_day_of_week = current_day_of_week

    split_time = time_str.split()
    start_time = split_time[0].split(":")

    # 23:55 PM ~= 23.916667 H
    self.start_time_24h = convert_12hour_to_24hour(
        hours=int(
            start_time[0]), am_pm=split_time[1]) + convert_minutes_to_hours(
                minutes=int(start_time[1]))

    self.total_time_24h = self.start_time_24h

  def add_hours(self, duration_str: str):
    duration_time = duration_str.split(":")

    duration_hours = int(duration_time[0]) + convert_minutes_to_hours(
        minutes=int(duration_time[1]))

    self.total_time_24h = self.total_time_24h + duration_hours
    self.num_of_days_between = floor(convert_hours_to_days(
        self.total_time_24h))

    self.end_time_24h = self.total_time_24h - (24 * self.num_of_days_between)

    if self.start_day_of_week is not None:
      self.end_day_of_week = add_to_day_of_week(self.start_day_of_week,
                                                self.num_of_days_between)

  def __str__(self):
    return self.to_str()

  def to_str(self):
    '''
    e.g: to_str = "7:42 AM (9 days later)"
    '''
    hours_part, ampm_part = convert_24hour_to_12_hour(int(self.end_time_24h))

    minutes_part = int(
        round(convert_hours_to_minutes(fractional_part(self.end_time_24h))))

    return (
        f"{str(hours_part)}:{str(minutes_part).zfill(2)} {ampm_part}" +
        f"{f', {self.end_day_of_week}' if self.end_day_of_week is not None else ''}"
        + f"{' (next day)' if self.num_of_days_between == 1 else ''}" +
        f"{f' ({self.num_of_days_between} days later)' if self.num_of_days_between > 1 else ''}"
    )
