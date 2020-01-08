CREATE DATABASE AutoParts --создание БД

USE AutoParts

CREATE TABLE Авто
(
	id int IDENTITY(1,1) PRIMARY KEY,
	марка varchar(100) NOT NULL,
	модель varchar(100) NOT NULL,
	кузов varchar(30) NOT NULL,
	год int NOT NULL,
	объём float NOT NULL
)

CREATE TABLE Склад --создание таблицы "Склад"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	адрес varchar(100) NOT NULL,
	вместимость int NOT NULL
)

CREATE TABLE Деталь --создание таблицы "Деталь"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_склада int NOT NULL,
	id_авто int NOT NULL,
	название varchar(100) NOT NULL,
	артикул varchar(100) NOT NULL UNIQUE,
	производитель varchar(50) NOT NULL,
	категория varchar(30) NOT NULL,
	цена float NOT NULL,
	FOREIGN KEY (id_склада) REFERENCES Склад(id),
	FOREIGN KEY (id_авто) REFERENCES Авто(id)
)

CREATE TABLE Брак --создание таблицы "Брак"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_детали int NOT NULL,
	описание varchar(50) NOT NULL,
	FOREIGN KEY (id_детали) REFERENCES Деталь(id)
)

CREATE TABLE Заказ --создание таблицы "Заказ"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_детали int NOT NULL,
	количество int NOT NULL DEFAULT 0,
	сумма float NULL DEFAULT -1,
	FOREIGN KEY (id_детали) REFERENCES Деталь(id)
)

CREATE TABLE Заявка --создание таблицы "Заявка"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_детали int NOT NULL,
	количество int NOT NULL DEFAULT 0,
	сумма float NOT NULL DEFAULT 0,
	FOREIGN KEY (id_детали) REFERENCES Деталь(id)
)

CREATE TABLE Продажа --создание таблицы "Продажа"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_заявки int NOT NULL,
	дата date,
	FOREIGN KEY (id_заявки) REFERENCES Заявка(id)
)

--заполнение таблиц

INSERT INTO Авто VALUES ('Mazda', 'Xedos 9', 'седан',2001,2.5), ('Mercedes-Benz', 'W212', 'седан',2011,5.5),('Fort', 'Transit', 'минивэн',2001,2.5),('Jaguar', 'Type-S', 'седан',2000,3.0),('Mazda', 'Xedos 9', 'седан',1997,2.0)

INSERT INTO Склад VALUES (N'Матусевича 113', 1500), (N'Домбровская 15', 250), (N'Казинца 11а', 300), (N'Гурского 3а', 300), (N'Железнодорожная 27', 500)

INSERT INTO Деталь VALUES 
(1,1, 'Фильтр масляный', 'A210115', 'DENCKERMANN', N'ТО и фильтра', 5.20),
(2,1, 'Фильтр воздушный', 'CMZ11431', 'COMLINE', N'ТО и фильтра', 3.29),
(3,2, 'Маховик', 'CF-509', 'JAPANPARTS', N'Коробка передач', 38.04),
(4,3, 'Выхлопная труба', 'CT829', 'CONTITECH', N'Двигатель и выхлоп', 28.40),
(5,4, 'Тормозной суппорт', 'ADC1246V', 'COMLINE', N'Тормозная система', 69.62),
(1,5, 'Аккумулятор', '6СТ60А1Е', 'Ista Power Optimal', N'Шины и аккумуляторы', 86.90)

INSERT INTO Брак VALUES (5, 'Трещина на тормозном диске'), (4, 'Виден корд ремня ГРМ')

INSERT INTO Заказ VALUES (1, 10, 52.0), (2, 5, 16.45), (3, 10, 380.04) , (4, 3, 85.2), (5, 5, 348.1)

INSERT INTO Заявка VALUES (1, 1, 5.20), (2, 3, 9.87), (3, 2, 76.08), (4, 1, 69.62), (5, 1, 86.9)

INSERT INTO Продажа VALUES (1, '26/9/2018'), (2, '26/9/2019'), (3, '25/9/2019'), (4, '20/9/2019')

GO
CREATE TRIGGER UpdateSum ON Заказ AFTER INSERT, UPDATE AS
BEGIN
	declare @id_d int
	declare @id int
	declare @count int
	declare @price float
	set @id = (SELECT inserted.id FROM inserted)
	set @id_d = (SELECT inserted.id_детали FROM inserted)
	set @count = (SELECT inserted.количество FROM inserted)
	set @price = (SELECT TOP 1 Деталь.цена FROM Деталь JOIN Заказ ON Заказ.id_детали = Деталь.id WHERE Деталь.id = @id_d)
	UPDATE Заказ SET сумма = ROUND((CAST(@count AS FLOAT) * @price),2) WHERE Заказ.id = @id
END

GO
CREATE TRIGGER UpdateSum2 ON Заявка AFTER INSERT, UPDATE AS
BEGIN
	declare @id_d int
	declare @id int
	declare @count int
	declare @price float
	set @id = (SELECT inserted.id FROM inserted)
	set @id_d = (SELECT inserted.id_детали FROM inserted)
	set @count = (SELECT inserted.количество FROM inserted)
	set @price = (SELECT TOP 1 Деталь.цена FROM Деталь JOIN Заявка ON Заявка.id_детали = Деталь.id WHERE Деталь.id = @id_d)
	UPDATE Заявка SET сумма = ROUND((CAST(@count AS FLOAT) * @price),2) WHERE Заявка.id = @id
END

Go
CREATE VIEW ДетальView AS
	SELECT Деталь.id, Авто.модель, Склад.адрес, Деталь.название, Деталь.артикул, Деталь.категория, Деталь.цена FROM dbo.Деталь
	JOIN Склад ON Склад.id = Деталь.id_склада
	JOIN Авто ON Авто.id = Деталь.id_авто

Go
CREATE VIEW ЗаявкаView AS
	SELECT Деталь.артикул, Заявка.количество, Заявка.сумма FROM Заявка
	JOIN Деталь ON Деталь.id = Заявка.id_детали

Go
CREATE VIEW ПродажаView AS
	SELECT Продажа.id, Заявка.id AS [Номер заявки], Продажа.дата FROM Продажа
	JOIN Заявка ON Заявка.id = Продажа.id_заявки
Go
CREATE VIEW ЗаказView AS
	SELECT Заказ.id, Заказ.количество, Заказ.сумма FROM Заказ

Go
CREATE VIEW СкладView AS
	SELECT * FROM Склад

GO
CREATE VIEW АвтоView AS
	SELECT * FROM Авто

GO
CREATE VIEW ДетальНаСкладеView AS
	SELECT Склад.id, Деталь.название, Деталь.артикул, Деталь.категория, Деталь.цена, Авто.модель  FROM Деталь
	JOIN Склад ON Склад.id = Деталь.id_склада
	JOIN Авто ON Авто.id = Деталь.id_авто

GO
CREATE VIEW ЗаказДеталиView AS
	SELECT Заказ.id, Деталь.название, Деталь.артикул, Деталь.категория, Деталь.цена, Авто.модель FROM Деталь
	JOIN Заказ ON Заказ.id_детали = Деталь.id
	JOIN Авто ON Авто.id = Деталь.id_авто

GO
CREATE VIEW SearchView AS
	SELECT Деталь.id, Авто.марка, Авто.модель, Деталь.артикул, Деталь.категория, Деталь.название, Деталь.производитель, Деталь.цена FROM Деталь
	JOIN Авто ON Авто.id = Деталь.id_авто

GO
CREATE VIEW PriceListView AS
	SELECT (название + ' ' + артикул) AS [Название], цена FROM Деталь

GO
CREATE VIEW SalesInYearView AS
	SELECT YEAR(Продажа.дата) AS  [Год], Деталь.категория, Деталь.id FROM Деталь
	JOIN Заявка ON Заявка.id_детали = Деталь.id
	JOIN Продажа ON Заявка.id = Продажа.id_заявки

GO
CREATE VIEW TopDetailsView AS
	SELECT TOP 3 (название + ' ' + артикул) AS [Название], COUNT(Продажа.дата) AS [Количество] FROM Деталь
	JOIN Заявка ON Заявка.id_детали = Деталь.id
	JOIN Продажа ON Заявка.id = Продажа.id_заявки
	GROUP BY (название + ' ' + артикул)
	ORDER BY  COUNT(Продажа.дата) DESC

GO
CREATE VIEW СостояниеСкладовView AS
	SELECT Склад.адрес, (название + ' ' + артикул) AS [Название]  FROM Деталь
	JOIN Склад ON Склад.id = Деталь.id_склада
	JOIN Авто ON Авто.id = Деталь.id_авто