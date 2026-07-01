# University Library API

Система для управления университетской библиотекой.


## 2) Настройка соединения с базой данных
* Настроена строка подключения в appsettings.json для связи с clever-cloud.com
* Установлена библиотеки MySql
* Создание модели Books, интерфейса IBookRepository.cs и класса BookRepository.cs
* Создание контроллера BooksController.cs

### Установка библиотеки MySql
```bash
    dotnet add package MySql.Data
```