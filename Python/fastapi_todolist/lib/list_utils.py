from typing import Callable, Iterable, TypeVar

TTarget = TypeVar("TTarget")


def try_get(
    target: Iterable[TTarget], index: int, default: TTarget | None = None
) -> TTarget | None:
    try:
        for i, item in enumerate(target):
            if i == index:
                return item
    except IndexError:
        return default


def try_get_first(
    target: Iterable[TTarget], default: TTarget | None = None
) -> TTarget | None:
    try:
        it = iter(target)
        return next(it)
    except IndexError:
        return default


TListItem = TypeVar("TListItem")
TProperty = TypeVar("TProperty")


def select_distinct(
    source: Iterable[TListItem], property_selector: Callable[[TListItem], TProperty]
) -> list[TProperty]:
    """
    Returns all distinct values from property selector.
    Does not preserve the order.

    Example Usage: `select_distinct(all_items, lambda item: item.user_id)`

    Args:
        source (Iterable[TListItem]): An Iterable (lists, tuples, querysets, etc.).
        property_selector (Callable[[TListItem], TProperty]): A lambda that selects the desired property.

    Returns:
        list[TProperty]: A list containing all distinct values.
    """
    return list(set(property_selector(list_item) for list_item in source))


def select_distinct_order_preserved(
    source: Iterable[TListItem], property_selector: Callable[[TListItem], TProperty]
) -> list[TProperty]:
    """
    Returns all distinct values from property selector.
    This method preserves the original order.

    Example Usage: `select_distinct(all_items, lambda item: item.user_id)`

    Args:
        source (Iterable[TListItem]): An Iterable (lists, tuples, querysets, etc.).
        property_selector (Callable[[TListItem], TProperty]): A lambda that selects the desired property.

    Returns:
        list[TProperty]: A list containing all distinct values.
    """
    existing_items = set()
    result = []

    for item in source:
        current_value = property_selector(item)

        if current_value not in existing_items:
            existing_items.add(current_value)
            result.append(current_value)

    return result
