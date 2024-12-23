Aplikacja symulująca ruch i wizualizację samolotów, przy pomocy wcześniej przygotowanych danych. 
Kluczowe funkcjonalności:
1. Wczytywanie danych z pliku FTR
2. Wczytywanie danych za pomocą źródła danych symulującego działanie serwera TCP dostarczającego informacje o lotach. Źródło danych przekazuje wiadomości w formacie binarnym.
3. Komenda "print" - serializuje dane do pliku JSON (mechanizm robienia snapshotów) - aplikacja nasłuchuje na polecenie, dane na temat obiektów zapisuje do odpowiedniego pliku
w folderze \bin\Debug\net8.0. Domyślnie wszsytkie inne, wcześniej wykonane snapshoty są usuwane.
4. Komenda "exit" - kończy działanie aplikacji
5. Generowanie wiadomości (z telewizji, radia oraz gazet) o jednym z trzech obiektów: lotnisku, samolocie pasażerskim lub samolocie towarowym. Format wiadomości jest predefiniowany, zmieniają się tylko takie elementy jak, np. nazwa samolotu lub lotniska. Polecenie "report" - wypisuje na konsolę przegląd wiadomości wygenerowany na podstawie danych wczytanych z pliku FTR
6. Mechanizm aktualizujący stan wczytanych wcześniej obiektów - aktualizacja ID obiektu, aktualizacja danych dotyczących pozycji obiektu, aktualizacja danych kontaktowych zapisanych w obiekcie. Zdarzenia te mogą być zgłaszane w dowolnym momencie po wczytaniu danych. Aktualizacja danych przebiega poprzez wczytanie z pliku o formacie .ftre odpowiedniego polecenia. Wszystkie wprowadzone zmiany są logowane do pliku tekstowego. Pliki te są zapisywane w oddzielnym folderze w folderze: \bin\Debug\net8.0\Logs. Jeden plik teksotwy jest przeznaczony na dane z całego dnia, dlatego przy kilkukrotnym uruchomieniu aplikacji jednego dnia, nowe logi są zapisywane do już istniejącego pliku, ale bez nadpisywania wcześniejszych dnaych
