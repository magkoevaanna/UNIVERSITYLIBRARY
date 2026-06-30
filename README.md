# University Library API

Система для управления университетской библиотекой.

## 1) Инициализация проекта
* Создана структура решения Backend.sln и Backend.csproj.
* Настроен базовый файл `Program.cs`.
* Добавлен автоматический `.gitignore` для .NET проекта.
* Инициализирован Git-репозиторий.


### Создание минимальной структуры проекта
```bash
dotnet new sln -n Backend
dotnet new webapi -n Backend
dotnet sln Backend.sln add Backend/Backend.csproj
git init
dotnet new gitignore
```