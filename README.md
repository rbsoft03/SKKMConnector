# SkkmConnector

C# клиент HTTP API **Сервера ККМ 4** (`/PrintService/api/v4`).  
Заполняете свойства, вызываете метод, читаете результат. Обмен с кассой скрыт внутри.

Нужны: .NET 6, запущенный Сервер ККМ (по умолчанию `localhost:4398`), касса в списке устройств.

## Установка

```bash
dotnet add package SkkmConnector
```

## Подключение

Хост, порт и токен — это сессия, не поля чека. Их можно менять между запросами.

Токен — заголовок `api_key`. Логин и пароль Admin в коннектор не передаются: ими на сервере получают токен (`GET user/token`).

```csharp
using var kkm = new ServerKkm();
kkm.Host = "localhost";
kkm.Port = 4398;
kkm.Token = token;          // api_key
kkm.DeviceName = "Emu";     // имя ККТ
```

Один экземпляр — одна сессия. В конце вызовите `Dispose()` (или `using`).

## Печать чека

```csharp
kkm.DeviceName = "Emu";
kkm.Cashier = new Cashier { Name = "Иванов А.И.", Vatin = "7722345678" };
kkm.NewRequest();
kkm.PaymentType = (int)CheckType.Sale;
kkm.TaxVariant = (int)TaxSystem.ОСН;
kkm.Positions.Add(new FiscalLine
{
    Name = "Кофе американо",
    Quantity = 1,
    Price = 150,
    Sum = 150,
    Tax = "20"
});
kkm.Payments = new Payments { Cash = 150 };
await kkm.PrintCheck();

if (!kkm.Ok)
    throw new InvalidOperationException($"{kkm.ErrorCode}: {kkm.ErrorDescription}");

var fp = kkm.FiscalResult; // ФП, номер ФД, смена, DocId
```

`Ping()` ключа не требует.

## Чего в клиенте нет

Админка сервера: получение токена, регистрация ККТ, пулы, reboot, шаблоны, фискализация. Это API сервера, не этого пакета.
