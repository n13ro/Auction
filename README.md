# 🏛️ Auction

<div align="center">

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=JSON%20web%20tokens&logoColor=white)

**Современная система управления аукционами с архитектурой Clean Architecture**

[🚀 Быстрый старт](#-быстрый-старт) • [📚 Документация](#-документация) • [🏗️ Архитектура](#️-архитектура) • [🔧 API Endpoints](#-api-endpoints)

</div>

---

## 📖 Описание

**Auction** — это полнофункциональная система управления аукционами, построенная на современном стеке технологий .NET 9.0 с использованием принципов Clean Architecture. Система предоставляет RESTful API для создания лотов, размещения ставок, управления пользователями и автоматического закрытия аукционов.

### ✨ Основные возможности

- 🎯 **Управление лотами** - создание, редактирование и автоматическое закрытие
- 💰 **Система ставок** - размещение ставок с автоматическим продлением времени
- 👥 **Управление пользователями** - регистрация, аутентификация, управление балансом
- 🔐 **JWT аутентификация** - безопасный доступ с refresh токенами
- ⏰ **Автоматизация** - фоновые сервисы для управления жизненным циклом
- 📊 **Swagger документация** - интерактивная API документация

---

## 🏗️ Архитектура

Проект построен по принципам **Clean Architecture** с четким разделением слоев:

```
Auction/
├──Backend/
   ├── 🎯 Domain/           # Бизнес-логика и сущности
   ├── 📱 Application/       # Слой приложения (CQRS, сервисы)
   ├── 🏗️ Infrastructure/   # Внешние зависимости (БД, фоновые сервисы)
   ├── 🌐 Auction/          # Web API (контроллеры, конфигурация)
   ├── 🔐 Registration/     # JWT аутентификация и авторизация
   ├── 🔧 Shared/           # Общие утилиты и маппинг
   └── 🧪 Tests/            # Unit и интеграционные тесты
├──Frontend/
   ├── 📁 Public/           # Изображения
   ├── ⚙️ src/              # Компоненты и сервисы приложения
   
```

### 🎯 Domain Layer
- **User** - пользователи системы с балансом и токенами
- **Lot** - лоты аукциона с временем жизни и статусами
- **Bid** - ставки пользователей с автоматическим продлением

### 📱 Application Layer
- **CQRS** - разделение команд и запросов
- **MediatR** - медиатор для обработки команд
- **DTOs** - объекты передачи данных

### 🏗️ Infrastructure Layer
- **Entity Framework Core** - ORM для PostgreSQL
- **Background Services** - фоновые задачи
- **Repositories** - абстракция доступа к данным

---

## 🚀 Быстрый старт

### 📋 Предварительные требования
- [.ANGULAR CLI](https://v17.angular.io/cli)
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) 12+
- [Git](https://git-scm.com/)

### 🔧 Установка и настройка

1. **Клонирование репозитория**
```bash
git clone https://github.com/your-username/auction-api.git
cd auction-api
```

2. **Настройка переменных окружения**
Создайте файл `.env` в корне проекта:
```env
# База данных
DB_Connect=Host=localhost;Database=auction_db;Username=your_username;Password=your_password

# JWT настройки
Jwt__Issuer=auction-api
Jwt__Audience=auction-users
Jwt__Key=your-super-secret-key-here-minimum-16-characters
```

3. **Восстановление зависимостей**
```bash
dotnet restore
```

4. **Применение миграций**
```bash
cd Infrastructure
dotnet ef database update
cd ..
```

5. **Запуск backend-части приложения**
```bash
cd Auction-backend
dotnet run
```

6. **Установкка зависимостей и запуск frontend-части приложения**
```bash
cd Auction-frontend
npm install 
ng serve
```

Приложение будет доступно по адресам: 
```
**https://localhost:7000** - backend
**https://localhost:4200** - frontend
```

### 📚 Swagger документация
После запуска откройте: **https://localhost:7000/swagger**

---

## 🔧 API Endpoints

### 🔐 Аутентификация

| Метод | Endpoint | Описание |
|-------|----------|----------|
| `POST` | `/api/Auth/SignIn` | Регистрация нового пользователя |
| `POST` | `/api/Auth/Login` | Вход в систему |
| `POST` | `/api/Auth/Refresh` | Обновление JWT токена |

### 👥 Пользователи

| Метод | Endpoint | Описание |
|-------|----------|----------|
| `GET` | `/api/Users/{id}` | Получение информации о пользователе |
| `PUT` | `/api/Users/{id}` | Обновление данных пользователя |
| `POST` | `/api/Users/{id}/deposit` | Пополнение баланса |
| `POST` | `/api/Users/{id}/withdraw` | Снятие средств |

### 🏛️ Лоты

| Метод | Endpoint | Описание |
|-------|----------|----------|
| `GET` | `/api/Lots` | Получение всех лотов |
| `GET` | `/api/Lots/{id}` | Получение лота по ID |
| `POST` | `/api/Lots` | Создание нового лота |
| `PUT` | `/api/Lots/{id}/close` | Закрытие лота |

### 💰 Ставки

| Метод | Endpoint | Описание |
|-------|----------|----------|
| `POST`| `/api/Bids` | Размещение ставки |
| `GET` | `/api/Bids/lot/{lotId}` | Получение ставок по лоту |

---

## 🔐 Аутентификация

Система использует **JWT токены** с автоматическим обновлением:

### 📝 Типы токенов
- **Access Token** - для доступа к API (60 минут)
- **Refresh Token** - для обновления access токена (10 минут)

### 🔄 Автоматическое обновление
Фоновый сервис `CheckRefreshTokenLife` автоматически:
- Проверяет токены каждые 30 секунд
- Отзывает истекшие refresh токены
- Очищает неактивные сессии

### 🛡️ Безопасность
- Пароли не передаются в ответах
- Токены автоматически отзываются
- Защищенные эндпоинты требуют авторизации

---

## ⚙️ Конфигурация

### 🔧 Основные настройки(Custom)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=auction_db;Username=your_username;Password=your_password"
  }
}
```

### 🌍 Переменные окружения

| Переменная | Описание | Пример |
|------------|----------|---------|
| `DB_Connect` | Строка подключения к PostgreSQL | `Host=localhost;Database=auction_db;Username=user;Password=pass` |
| `Jwt__Issuer` | Издатель JWT токенов | `auction-api` |
| `Jwt__Audience` | Аудитория JWT токенов | `auction-users` |
| `Jwt__Key` | Секретный ключ для JWT | `your-super-secret-key-here` |

---

## 🧪 Тестирование

### 🧪 Unit тесты(Custom)
```bash
cd Tests
dotnet test
```

### 📊 Покрытие кода
```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

## 🚀 Развертывание(Custom)

### 🐳 Docker
```bash
docker build -t auction-api .
docker run -p 8080:80 auction-api
```

### ☁️ Azure
```bash
az webapp up --name auction-api --resource-group myResourceGroup --runtime "DOTNETCORE:9.0"
```

### 🐧 Linux
```bash
dotnet publish -c Release -o ./publish
sudo systemctl enable auction-api
sudo systemctl start auction-api
```

---

## 🤝 Вклад в проект

Мы приветствуем вклад в развитие проекта! 

### 📝 Как внести вклад

1. **Fork** репозитория
2. Создайте **feature branch** (`git checkout -b feature/amazing-feature`)
3. **Commit** изменения (`git commit -m 'Add amazing feature'`)
4. **Push** в branch (`git push origin feature/amazing-feature`)
5. Откройте **Pull Request**

### 📋 Требования к коду

- Следуйте принципам Clean Architecture
- Добавляйте unit тесты для новой функциональности
- Используйте meaningful commit messages
- Обновляйте документацию при необходимости

---

## 📄 Лицензия

Этот проект лицензирован под **MIT License** - см. файл [LICENSE](LICENSE.txt) для деталей.

---

## 👥 Команда

**Auza Team** - команда разработчиков, создавшая Auction API

- 📧 **Email**: auzateamind@gmail.com
- 🌐 **Веб-сайт**: [auza-team.com](https://auza-team.com) (В разработке)
- 💬 **Telegram**: [@auzateamind](https://t.me/auzateamind)

---

## 🙏 Благодарности

- [.NET Community](https://dotnet.microsoft.com/) за отличную платформу
- [Entity Framework Team](https://docs.microsoft.com/en-us/ef/) за мощный ORM
- [JWT.io](https://jwt.io/) за стандарт токенов
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) за архитектурные принципы

---

<div align="center">

**⭐ Если проект вам понравился, поставьте звездочку! ⭐**

**Сделано с ❤️ командой Auza Team**

</div>
