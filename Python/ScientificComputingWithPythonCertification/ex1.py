def main():
    print("Select a program:")
    print("0 - \"Exit\"")
    print("1 - \"Greetings\"")
    print("2 - \"Total pay\"")
    print("3 - \"TCP HTTP GET\"")

    (lambda input = input() : {
        "0": lambda : 0,

        "1": greetings,
        "2": total_pay,
        "3": get_web_page,
    }.get(
        input,
        lambda : print("WTF?")
    )())()

    print("\nDone. Good by.");

def greetings():
    name = input("Insert your name: ")
    print(f"Hello, {name}");

def total_pay():
    hours = float(input("Enter hours: "))
    rate = float(input("Enter rate: "))
    print(f"Pay: {hours * rate}");

def get_web_page():
    import socket

    url = input("Domain: ")

    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect( (url, 80) )

    page = input("Page: ")

    sock.send(f"GET http://{url}/{page} HTTP/1.0\n\n".encode())

    while True:
        data = sock.recv(512)
        if len(data) < 1:
            break
        try: print(data.decode("utf-8"))
        except: print(data.decode("latin-1"))

    sock.close();

main();
