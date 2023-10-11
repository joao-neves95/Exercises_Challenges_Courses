from time_span import TimeSpan


def add_time(start, duration, current_day_of_week=None):
  time_span = TimeSpan(start, current_day_of_week)
  time_span.add_hours(duration)

  return time_span.to_str()
