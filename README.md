Aplikacja symulująca ruch i wizualizację samolotów, przy pomocy wcześniej przygotowanych danych. 
Kluczowe funkcjonalności:
1. Wczytywanie danych z pliku FTR
2. Wczytywanie danych za pomocą źródła danych symulującego działanie serwera TCP dostarczającego informacje o lotach. Źródło danych przekazuje wiadomości w formacie binarnym.
3. Komenda "print" - serializuje dane do pliku JSON (mechanizm robienia snapshotów) - aplikacja nasłuchuje na polecenie, dane na temat obiektów zapisuje do odpowiedniego pliku
w folderze \bin\Debug\net8.0. Domyślnie wszsytkie inne, wcześniej wykonane snapshoty są usuwane.
4. Komenda "exit" - kończy działanie aplikacji
5. Generowanie wiadomości (z telewizji, radia oraz gazet) o jednym z trzech obiektów: lotnisku, samolocie pasażerskim lub samolocie towarowym
