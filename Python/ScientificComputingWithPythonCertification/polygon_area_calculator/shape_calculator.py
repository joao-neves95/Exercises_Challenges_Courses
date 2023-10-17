class Rectangle:
  width: float
  height: float

  def __init__(self, width: float, height: float) -> None:
    self.width = width
    self.height = height

  def set_width(self, width: float):
    self.width = width

  def set_height(self, height: float):
    self.height = height

  def get_area(self) -> float:
    return self.width * self.height

  def get_perimeter(self) -> float:
    return 2 * self.width + 2 * self.height

  def get_diagonal(self) -> float:
    return (self.width**2 + self.height**2)**.5

  def __str__(self) -> str:
    return f"Rectangle(width={self.width}, height={self.height})"

  def get_picture(self) -> str:
    if self.width > 50 or self.height > 50:
      return "Too big for picture."

    table = []
    for iRow in range(self.height):
      row = []
      for iCol in range(self.width):
        row.append("*")
      table.append(row)

    return f"{table_to_string(table, '')}\n"

  def get_amount_inside(self, shape):
    return int(self.get_area() / shape.get_area())


class Square(Rectangle):

  def __init__(self, side: float) -> None:
    self.set_side(side)

  def set_side(self, side: int):
    self.width = side
    self.height = side

  def __str__(self) -> str:
    return f"Square(side={self.width})"


def table_to_string(table: [[]], column_separator: str):
  return "\n".join((column_separator.join(row) for row in table))
