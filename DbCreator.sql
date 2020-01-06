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
	артикул varchar(100) NOT NULL,
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

CREATE TABLE Поставщик --создание таблицы "Поставщик"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_заказа int NOT NULL,
	название varchar(50) NOT NULL,
	FOREIGN KEY (id_заказа) REFERENCES Заказ(id)
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

INSERT INTO Поставщик VALUES (1, 'Autosell'), (2, 'Autosell'), (3, 'ATR'), (4, 'AMR'), (5, 'ATR')

INSERT INTO Заявка VALUES (1, 1, 5.20), (2, 3, 9.87), (3, 2, 76.08), (4, 1, 69.62), (5, 1, 86.9)

INSERT INTO Продажа VALUES (1, '26/9/2018'), (2, '26/9/2019'), (3, '25/9/2019'), (4, '20/9/2019')

Go
CREATE VIEW ДетальView AS
	SELECT Деталь.id, Авто.модель, Склад.адрес, Деталь.название, Деталь.артикул, Деталь.категория, Деталь.цена FROM dbo.Деталь
	JOIN Склад ON Склад.id = Деталь.id_склада
	JOIN Авто ON Авто.id = Деталь.id_авто

Go
CREATE VIEW ЗаказView AS
	SELECT Заказ.id, Деталь.артикул, Заказ.количество, Заказ.сумма FROM Заказ
	JOIN Деталь ON Деталь.id = Заказ.id_детали

Go
CREATE VIEW ЗаявкаView AS
	SELECT Заявка.id, Деталь.артикул, Заявка.количество, Заявка.сумма FROM Заявка
	JOIN Деталь ON Деталь.id = Заявка.id_детали

Go
CREATE VIEW ПоставщикView AS
	SELECT Поставщик.id, Заказ.id AS [Номер заказа], Поставщик.название FROM Поставщик
	JOIN Заказ ON Заказ.id = Поставщик.id_заказа

Go
CREATE VIEW ПродажаView AS
	SELECT Продажа.id, Заявка.id AS [Номер заявки], Продажа.дата FROM Продажа
	JOIN Заявка ON Заявка.id = Продажа.id_заявки

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
CREATE VIEW SearchView AS
	SELECT Деталь.id, Авто.марка, Авто.модель, Деталь.артикул, Деталь.категория, Деталь.название, Деталь.производитель FROM Деталь
	JOIN Авто ON Авто.id = Деталь.id_авто

SELECT * FROM SearchView