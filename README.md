# SkkmConnector

C#-библиотека для **РБ-Софт: Сервер ККМ 4** — полное покрытие REST API v4.

Версия: **1.25.5** | Платформа: **.NET 6**

## Документация

Полное руководство и справочник API — в папке документации коннектора (`README.md`, `API.md`).

Демо-приложение `SkkmNugetSample` — коллекция примеров в структуре Bruno.

## Быстрый старт

```csharp
using SkkmConnector;

using var kkm = new ServerKkm
{
    Host = "localhost",
    Token = "<api_key>",
    DeviceName = "Emu",
    Cashier = new Cashier { Name = "Иванов А.И." }
};

kkm.NewRequest();
kkm.PaymentType = (int)CheckType.Sale;
kkm.Positions.Add(new FiscalLine { Name = "Кофе", Price = 150, Tax = "20" });
kkm.Payments = new Payments { Cash = 150 };

await kkm.PrintCheck();
```

## Сборка

```powershell
dotnet build SkkmConnector.sln -c Release
dotnet pack SkkmConnector\SkkmConnector.csproj -c Release -o .\nupkg
```

## Лицензия

MIT — см. [LICENSE](LICENSE)
