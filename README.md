<div align="center"><h1> PaymentSystemAPI </h1></div>

Тестовое задание Backend-потока на [Xsolla Summer School 2020](https://github.com/FJCrux/xsolla-backend-school-2020) - платёжная система.    
Автор: Салимов Руслан.
## Содержание
- [Возможности](#возможности)
- [Установка и запуск](#Установка и запуск)
- [Создано с помощью](#Создано с помощью)

## Возможности  
Созданный API позволяет:
- Создать платеж
- Оплатить платеж
- Вернуть список платежей за указанный период (доступно авторизованным пользователям)
- Создание аккаунта
- Авторизация

## Установка и запуск
___Для запуска программы должны быть установлены: .NET Core 3.1 и SQL Server 2019.___    
Скачивание проекта с Github:
```
git clone https://github.com/FrostyCreator/APIPaymentSystem
```    
Перейти в директорию: ..\APIPaymentSystem\src\APIPaymentSystem
```
cd ..\APIPaymentSystem\src\APIPaymentSystem
```    
Восстановить все зависимости решения:
```
dotnet restore
```
Запустить программу:
```
dotnet run
```
## Создано с помощью
- IDE - Visual Studio 2019
- База данных - SQL Server 2019
- Технологии
    - Asp.Net Core Mvc 3.1
    - Entity Framework Core
