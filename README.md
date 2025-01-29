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

--ENGLISH VERSION--

FlightTracker is an application whose main functionality is simulating and visualizing aircraft movement using pre-prepared data. The movement of aircraft is simulated on an interactive map. The displayed map can be zoomed in using the mouse scroll wheel. The movement is subtle enough that it is recommended to zoom in to observe the aircraft’s motion.

The application determines the shortest route for a given flight using the great-circle method, which helps find the minimal arc on a sphere. The current position is approximated based on the coordinates of the departure and destination airports, the takeoff and landing times, and the current time.
Key functionalities:

1. Loading data from an FTR file.
2. Loading data via a data source that simulates a TCP server providing flight information.
3. Mechanism for updating previously loaded objects:
   1. Updating the object’s ID.
   2. Updating object position data.
   3. Updating contact details stored in the object.
     
These events can be triggered at any time after loading data. Updates are processed by reading commands from a file in the .ftre format.
All changes are logged into a text file. These files are stored in a separate folder: \bin\Debug\net8.0\Logs. One text file is used per day, so if the application is restarted multiple times in one day, new logs are appended to the existing file without overwriting previous data.
4. Serializing application object data into a JSON file.
5. Logging changes in object data – as described in point 3.
6. Generating predefined messages about objects – as described in the Available Commands ("report") section.
7. Filtering and searching data using simple commands similar to SQL queries.

Available Commands:

1. "print" command – serializes data to a JSON file (snapshot mechanism). The application listens for this command and saves object data to the appropriate file in the \bin\Debug\net8.0 folder. By default, all previous snapshots are deleted.
2. "exit" command – terminates the application.
3. "report" command – displays a summary of messages on the console, generated based on data loaded from the FTR file. Messages are simulated from sources such as television, radio, and newspapers for one of three objects: an airport, a passenger plane, or a cargo plane. The message format is predefined, with only elements like the aircraft or airport name changing dynamically.
4. Data searching and filtering. Available commands:
   4.1. display – shows data in a table.
   Syntax: display {object_fields} from {object_class} [where {conditions}]
   4.2. update – updates data.
   Syntax: update {object_class} set ({key_value_list}) [where {conditions}]
   4.3. delete – deletes selected data.
   Syntax: delete {object_class} [where {conditions}]
   4.4 add – adds new data.
   Syntax: add {object_class} new ({key_value_list})
   
   Where:
   {object_class} – the name of the object class: Crew, Passenger, Cargo, CargoPlane, PassengerPlane, Airport, or Flight
   {object_fields} – a list of class fields separated by commas or * to select all fields
   {conditions} – a list of conditions separated by and and or operators. Each condition consists of a field name, an operator (=, <=, >=, !=), and a value. Parentheses for grouping are not supported.
   {key_value_list} – a list of key-value pairs separated by commas, with an equal sign between them. The key represents the name of an object class field.





