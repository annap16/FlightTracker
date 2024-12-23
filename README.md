FlightTracker to aplikacja, której główną funkcjonalnością jest symulowanie ruchu i wizualizacja samolotów, przy pomocy wcześniej przygotowanych danych. Ruch samolotów jest symulowany na interaktywnej mapie. Wyświetlana mapa może zostać przybliżona za pomocą scrolla na myszy. Ruch jest na tyle niewielki, że zaleca się przybliżenie mapy w celu zaobserwowania ruchu samolotu. \n
Aplikacja wyznacza najkrótszą trasę dla danego lotu, korzystając z metody okręgu wielkiego, która pozwala znaleźć minimalny łuk na sferze. Bieżące położenie jest aproksymowane na podstawie współrzędnych lotniska początkowego i docelowego, czasów startu i lądowania, a także aktualnego czasu.


Kluczowe funkcjonalności funkcjonalności:
1. Wczytywanie danych z pliku FTR
2. Wczytywanie danych za pomocą źródła danych symulującego działanie serwera TCP dostarczającego informacje o lotach. 
3. Mechanizm aktualizujący stan wczytanych wcześniej obiektów
   1. Aktualizacja ID obiektu
   2. Aktualizacja danych dotyczących pozycji obiektu
   3. Aktualizacja danych kontaktowych zapisanych w obiekcie
Zdarzenia te mogą być zgłaszane w dowolnym momencie po wczytaniu danych. Aktualizacja danych przebiega poprzez wczytanie z pliku o formacie .ftre odpowiedniego polecenia. Wszystkie wprowadzone zmiany są logowane do pliku tekstowego. Pliki te są zapisywane w oddzielnym folderze w folderze: \bin\Debug\net8.0\Logs. Jeden plik tekstowy jest przeznaczony na dane z całego dnia, dlatego przy kilkukrotnym uruchomieniu aplikacji jednego dnia, nowe logi są zapisywane do już istniejącego pliku, ale bez nadpisywania wcześniejszych danych
4. Serializacja danych na temat obiektów aplikacji do pliku JSON
5. Logowanie wprowadzonych zmian w danych obiektów - opisane w punkcie 3
6. Generowanie predefiniowanych wiadomości na temat obiektów - opisane w sekcji Dostępne komendy ("report")
7. Filtrowanie i przeszukiwanie danych za pomocą prostych poleceń podobne do zapytań SQL.


Dostępne komendy:
1.  Komenda "print" - serializuje dane do pliku JSON (mechanizm robienia snapshotów) - aplikacja nasłuchuje na polecenie, dane na temat obiektów zapisuje do odpowiedniego pliku w folderze \bin\Debug\net8.0. Domyślnie wszsytkie inne, wcześniej wykonane snapshoty są usuwane.
2.  Komenda "exit" - kończy działanie aplikacji
3.  Komenda "report" - wypisuje na konsolę przegląd wiadomości wygenerowany na podstawie danych wczytanych z pliku FTR. Generowanie wiadomości (ze źródła symulującego telewizję, radio oraz gazety) o jednym z trzech obiektów: lotnisku, samolocie pasażerskim lub samolocie towarowym. Format wiadomości jest predefiniowany, zmieniają się tylko takie elementy jak, np. nazwa samolotu lub lotniska.
4. Przeszukiwanie i filtrowanie danych. Możliwe komendy:
   4.1. display - wyświetla dane w tabeli, składnia: display {object_fields} from {object_class} [where {conditions}]
   4.2. update - aktualizuje dane, składnia: update {object_class} set ({key_value_list}) [where {conditions}]
   4.3. delete - usuwa wybrane dane, składnia: delete {object_class} [where {conditions}]
   4.4. add - dodaje nowe dany, składnia: add {object_class} new ({key_value_list})
   Gdzie:
   {object_class} - to nazwa klasy obiektu: Crew, Passenger, Cargo, CargoPlane, PassengerPlane, Airport lub Flight
   {object_fields} - to lista pól klasy rozdzielona przecinkami lub * dla wszystkich pól
   {conditions} - lista warunków rodzielona operatorami and i or. Warunek składa się z nazwy pola klasy obiektu operatora (=, <=, >=, !=) oraz wartości. Bez możliwości          grupowania nawiasami
   {key_value_list - lista par klucz-wartość rozdzielona przecinkami ze znakiem równa się pomiędzy nimi, gdzie klucz to nazwa pola danej klasy obiektu


Dodatkowe komentarze:


