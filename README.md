# University Library API 

REST API для управления процессами университетской библиотеки. Проект написан на .NET 8 с использованием многослойной архитектуры и чистого ADO.NET для работы с базой данных.

## Тестирование проекта (Swagger)
Приложение развернуто в облаке и доступно по ссылке:
👉 **[Swagger](https://universitylibrary.onrender.com/swagger/index.html)**

---

## Стек технологий
* **Backend:** .NET 8 (ASP.NET Core Web API)
* **База данных:** MySQL (Хостинг: Clever Cloud)
* **Хостинг API:** Render


---

## Структура проекта
* `Controllers/` — обработка HTTP-запросов и маршрутизация API.
* `Repositories/` — слой доступа к данным (SQL-запросы через `MySqlDataReader`).
* `DTO/` — модели передачи данных для оптимизации JSON-структур.
* `Data/Entities/` — доменные сущности таблиц базы данных.

---

## Локальный запуск
1. `git clone https://github.com/magkoevaanna/UNIVERSITYLIBRARY`
2. `cd Backend`
3. `dotnet run`
