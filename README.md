# Books

Projekt API zwracającego książki
Testy zostały utworzone w projekcie Books.Core.Unit.Tests

# API
GET: /books/{page}/{count} zwraca ilość książek na aktualnej stronie (dla strony 1ej zwraca ilość całkowitą w nagłówku0
POST: /book payload={book} dodaje lub robi update książki jeśli podano istniejący ID
GET: /orders/{page}/{count} zwraca ilość zamówień, analogicznie jak /books/{page}/{count}

# TOKEN
API jest zabezpieczone na poziomie middleware, które weryfikuje czy przekazano token: b@rdz0BeZpi3cznyToken
