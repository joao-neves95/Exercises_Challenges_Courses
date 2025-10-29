from fastapi import HTTPException, status


invalid_credentials_exception = HTTPException(
    status_code=status.HTTP_401_UNAUTHORIZED,
    detail="Could not validate credentials",
    headers={"WWW-Authenticate": "Bearer"},
)

invalid_login_credentials_exception = HTTPException(
    status_code=status.HTTP_401_UNAUTHORIZED,
    detail="Incorrect username or password",
    headers={"WWW-Authenticate": "Bearer"},
)


def user_not_found_exception(user_ulid: str) -> HTTPException:
    return HTTPException(
        status_code=status.HTTP_404_NOT_FOUND, detail=f"User '{user_ulid}' not found"
    )


user_has_no_permissions_exception = HTTPException(
    status_code=status.HTTP_401_UNAUTHORIZED,
    detail="The current user has no access permissions to this resource",
    headers={"WWW-Authenticate": "Bearer"},
)


def raise_if_user_has_no_permissions(token_user_ulid, request_user_ulid):
    if request_user_ulid != token_user_ulid:
        raise user_has_no_permissions_exception
