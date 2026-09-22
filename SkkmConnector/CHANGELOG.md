## ИЗМЕНЕНИЯ


* 1.28.10
  Шаблоны
  - Шаблоны печати и шаблоны чека собираются так же, как обычный чек: `NewTemplate` / `NewCheckTemplate`, используют `AddText`, `AddBarcode`, `AddSeparatorLine`, `AddPicture`, `AddPosition`;
  - Перегрузки с именем в параметре: `AddCheckTemplate(name)`, `UpdateCheckTemplate(name)`, `GetTemplate(name)`, `GetCheckTemplate(name)`, `DeleteTemplate(name)`.
  - Проверка перед отправкой: имя шаблона обязательно и без пробелов, шаблон не должен быть пустым.

  Формирование чека
  - `NewCheck(тип операции)` / `NewCheck(имя кассир, тип операции, СНО)` и `NewCheckCorrection(тип операции, основание коррекции, описание коррекции, время коррекции, номер предписания)` / `NewCheckCorrection(кассир, СНО, тип операции, основание коррекции, описание коррекции, время коррекции, номер предписания)` очищают предыдущий чек и ответ
  - Данные кассира и СНО задаются на смену (`SetCashierName`, `SetCashierVatin`, `SetTaxType`) и не сбрасываются между чеками; 
  - `Clear()` чистит текущий чек, `ClearAll()` — чистит и кассира с СНО.
  - Ставка НДС позиции переведены enum `TaxRate` (`Vat20`, `Vat22`, `Vat10`, `Vat0`, `Vat5`, `Vat7`, `None`, расчётные ставки `Vat20_120`, `Vat5_105`, `Vat7_107`,`Vat10_110`, `Vat20_120`, `Vat22_122` )

  Запросы по идентификатору
  - GET-методы принимают значение параметром: `GetCheck(docId)`, `GetFiscalSign(номерФД)`, `GetCheckList(номерСмены)`, `GetOperation(docId)`, `GetReportX(docId)`, `GetReportZ(docId)`, `GetOpenShift(docId)`, `GetReportSettlement(docId)`, `GetCorrection120(docId)`, `GetCorrection105(docId)`, `GetTaskStatus(docId)`, `GetCheck(docId)`, `GetFiscalSign(docNumber)`, `GetPrintForm(documentId)`, `GetCashIn(docId)`, `GetCashOut(docId)`, `GetSlip(docId)`, `GetPicture(pictureId)`, `GetTemplate(name)`, `GetCheckTemplate(name)`, `GetQueueTask(taskId)`, `GetQueueTaskHistory(taskId)`, `GetFiscalization(docId)`, `GetOperation(docId)`, `GetOperationHistory(docId)`, `GetOperationTlv(docId)`, `GetOperationKm(docId)`, `GetOperationRelated(docId)`
  - `FromTo(с, по)` — задание периода для списков за период.

* 1.25.5
  - Коннектор покрывает REST API Сервера ККМ 4: чеки, смены, отчёты, наличные, нефискальные документы, картинки, маркировка, шаблоны, очередь, операции, фискализация и администрирование.
  - Добавлена работа с печатными шаблонами:
    - `POST` `/template` - создание шаблона
    - `PUT` `/template` - изменение шаблона
    - `DELETE` `/template` - удаление шаблона
    - `GET` `/template/list` - список шаблонов
    - `GET` `/template` - получение шаблона по имени
  - Добавлена работа с шаблонами чека:
    - `POST` `/checkTemplate` - создание шаблона чека
    - `PUT` `/checkTemplate` - изменение шаблона чека
    - `DELETE` `/checkTemplate` - удаление шаблона чека
    - `GET` `/checkTemplate/list` - список шаблонов чека
    - `GET` `/checkTemplate` - получение шаблона чека по имени
  - При создании шаблона чека позиции уходят в том же виде, что и при печати чека (`FiscalString`), поэтому на сервере сохраняются строки чека, а не пустой шаблон.
  - Добавлена работа с очередью печати:
    - `GET` `/queue` - состояние очереди
    - `GET` `/queue/task` - состояние задания
    - `GET` `/queue/task/history` - история задания
    - `DELETE` `/queue/task` - отмена задания в очереди
  - Добавлены запросы для получения дополнительных данных операции:
    - `GET` `/operation/last` - последняя операция
    - `GET` `/operation` - операция по идентификатору документа
    - `GET` `/operation/history` - история выполнения операции
    - `GET` `/operation/tlv` - TLV операции
    - `GET` `/operation/km` - коды маркировки операции
    - `GET` `/operation/related` - связанные операции
    - `GET` `/operation/list` - список операций за период
  - Добавлена фискализация кассы:
    - `POST` `/fiscalization` - фискализация
    - `POST` `/fiscalization/async` - асинхронная фискализация
    - `GET` `/fiscalization` - результат фискализации по документу
    - `GET` `/fiscalization/list` - список операций фискализации
  - Добавлена проверка кодов маркировки:
    - `POST` `/marking/km/verify` - проверка КМ
    - `POST` `/marking/km/tspiot/verify` - проверка КМ через ТС ПИоТ
    - `POST` `/marking/km/lmcz/verify`- проверка КМ через ЛМ ЧЗ
  - Добавлено администрирование сервера и касс:
    - `GET` `/user/token` - получение токена по логину и паролю
    - `GET` `/user/list` - список пользователей
    - `POST` `/user` - добавление пользователя
    - `PUT` `/user` - изменение пользователя
    - `DELETE` `/user` - удаление пользователя
    - `GET` `/service/settings` - настройки службы
    - `POST` `/service/settings` - сохранение настроек службы
    - `POST` `/kkt` - добавление кассы
    - `PUT` `/kkt` - изменение кассы
    - `DELETE` `/kkt` - удаление кассы
    - `POST` `/kkt/reboot` - перезагрузка кассы
    - `POST` `/kkt/font/setting` - настройка шрифтов кассы
    - `GET` `/pool/list` - список пулов
    - `GET` `/kkt/list/byPool` - кассы пула
    - `GET` `/version` - версия сервера
  - Добавлены недостающие операции с документами и картинками:
    - `GET` `/check/list` - список чеков за период
    - `POST` `/check/copy/fn` - копия чека из ФН
    - `GET` `/slip` - получение нефискального документа
    - `GET` `/slip/list` - список нефискальных документов
    - `GET` `/picture` - получение изображения
    - `DELETE` `/picture` - удаление изображения
  - Для смен, отчётов и внесений/выемок добавлены асинхронные методы: `OpenShiftAsync`, `CloseShiftAsync`, `ReportXAsync`, `ReportSettlementAsync`, `CashInAsync`, `CashOutAsync`.
  - Период списков отчётов, чеков и операций задаётся свойствами `ShiftsFrom` и `ShiftsTo`.
  - Перечисление `CheckType` дополнено типами заданий сервера: слип, фискализация, смена, отчёты, внесение, выемка, копия из ФН, дубликат документа.
  - Транспорт поддерживает `PUT` и `DELETE`, а также Basic Auth для получения токена пользователя.
