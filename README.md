# My Private Library

## Instrukcja uruchomienia projektu
- Należy utworzyć nową bazę danych
- W pliku Startup.cs należy skonfigurować connection string do bazy danych (linia 41).
- Należy wykonać polecenie "update-database" w Package Manager Console.
- Aplikacja klienta wykonana jest w frameworku Angular, dlatego do uruchomienia potrzebny jest Node.js. 
  Testowałem działanie na dwóch wersjach Node.js:
   - Dla wersji 8.11.2 aplikacja nie uruchamia się poprawnie
   - Dla wersji 10.4.0 działanie jest prawidłowe
- Należy wykonać polecenie "npm install" w folderze "MyPrivateLibrary/MyPrivateLibraryAPI/MyPrivateLibraryAPI/ClientApp/"
- Projekt jest gotowy do uruchomienia
