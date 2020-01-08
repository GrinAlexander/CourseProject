CREATE DATABASE AutoParts --�������� ��

USE AutoParts

CREATE TABLE ����
(
	id int IDENTITY(1,1) PRIMARY KEY,
	����� varchar(100) NOT NULL,
	������ varchar(100) NOT NULL,
	����� varchar(30) NOT NULL,
	��� int NOT NULL,
	����� float NOT NULL
)

CREATE TABLE ����� --�������� ������� "�����"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	����� varchar(100) NOT NULL,
	����������� int NOT NULL
)

CREATE TABLE ������ --�������� ������� "������"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_������ int NOT NULL,
	id_���� int NOT NULL,
	�������� varchar(100) NOT NULL,
	������� varchar(100) NOT NULL UNIQUE,
	������������� varchar(50) NOT NULL,
	��������� varchar(30) NOT NULL,
	���� float NOT NULL,
	FOREIGN KEY (id_������) REFERENCES �����(id),
	FOREIGN KEY (id_����) REFERENCES ����(id)
)

CREATE TABLE ���� --�������� ������� "����"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_������ int NOT NULL,
	�������� varchar(50) NOT NULL,
	FOREIGN KEY (id_������) REFERENCES ������(id)
)

CREATE TABLE ����� --�������� ������� "�����"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_������ int NOT NULL,
	���������� int NOT NULL DEFAULT 0,
	����� float NULL DEFAULT -1,
	FOREIGN KEY (id_������) REFERENCES ������(id)
)

CREATE TABLE ������ --�������� ������� "������"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_������ int NOT NULL,
	���������� int NOT NULL DEFAULT 0,
	����� float NOT NULL DEFAULT 0,
	FOREIGN KEY (id_������) REFERENCES ������(id)
)

CREATE TABLE ������� --�������� ������� "�������"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_������ int NOT NULL,
	���� date,
	FOREIGN KEY (id_������) REFERENCES ������(id)
)

--���������� ������

INSERT INTO ���� VALUES ('Mazda', 'Xedos 9', '�����',2001,2.5), ('Mercedes-Benz', 'W212', '�����',2011,5.5),('Fort', 'Transit', '�������',2001,2.5),('Jaguar', 'Type-S', '�����',2000,3.0),('Mazda', 'Xedos 9', '�����',1997,2.0)

INSERT INTO ����� VALUES (N'���������� 113', 1500), (N'����������� 15', 250), (N'������� 11�', 300), (N'�������� 3�', 300), (N'��������������� 27', 500)

INSERT INTO ������ VALUES 
(1,1, '������ ��������', 'A210115', 'DENCKERMANN', N'�� � �������', 5.20),
(2,1, '������ ���������', 'CMZ11431', 'COMLINE', N'�� � �������', 3.29),
(3,2, '�������', 'CF-509', 'JAPANPARTS', N'������� �������', 38.04),
(4,3, '��������� �����', 'CT829', 'CONTITECH', N'��������� � ������', 28.40),
(5,4, '��������� �������', 'ADC1246V', 'COMLINE', N'��������� �������', 69.62),
(1,5, '�����������', '6��60�1�', 'Ista Power Optimal', N'���� � ������������', 86.90)

INSERT INTO ���� VALUES (5, '������� �� ��������� �����'), (4, '����� ���� ����� ���')

INSERT INTO ����� VALUES (1, 10, 52.0), (2, 5, 16.45), (3, 10, 380.04) , (4, 3, 85.2), (5, 5, 348.1)

INSERT INTO ������ VALUES (1, 1, 5.20), (2, 3, 9.87), (3, 2, 76.08), (4, 1, 69.62), (5, 1, 86.9)

INSERT INTO ������� VALUES (1, '26/9/2018'), (2, '26/9/2019'), (3, '25/9/2019'), (4, '20/9/2019')

GO
CREATE TRIGGER UpdateSum ON ����� AFTER INSERT, UPDATE AS
BEGIN
	declare @id_d int
	declare @id int
	declare @count int
	declare @price float
	set @id = (SELECT inserted.id FROM inserted)
	set @id_d = (SELECT inserted.id_������ FROM inserted)
	set @count = (SELECT inserted.���������� FROM inserted)
	set @price = (SELECT TOP 1 ������.���� FROM ������ JOIN ����� ON �����.id_������ = ������.id WHERE ������.id = @id_d)
	UPDATE ����� SET ����� = ROUND((CAST(@count AS FLOAT) * @price),2) WHERE �����.id = @id
END

GO
CREATE TRIGGER UpdateSum2 ON ������ AFTER INSERT, UPDATE AS
BEGIN
	declare @id_d int
	declare @id int
	declare @count int
	declare @price float
	set @id = (SELECT inserted.id FROM inserted)
	set @id_d = (SELECT inserted.id_������ FROM inserted)
	set @count = (SELECT inserted.���������� FROM inserted)
	set @price = (SELECT TOP 1 ������.���� FROM ������ JOIN ������ ON ������.id_������ = ������.id WHERE ������.id = @id_d)
	UPDATE ������ SET ����� = ROUND((CAST(@count AS FLOAT) * @price),2) WHERE ������.id = @id
END

Go
CREATE VIEW ������View AS
	SELECT ������.id, ����.������, �����.�����, ������.��������, ������.�������, ������.���������, ������.���� FROM dbo.������
	JOIN ����� ON �����.id = ������.id_������
	JOIN ���� ON ����.id = ������.id_����

Go
CREATE VIEW ������View AS
	SELECT ������.�������, ������.����������, ������.����� FROM ������
	JOIN ������ ON ������.id = ������.id_������

Go
CREATE VIEW �������View AS
	SELECT �������.id, ������.id AS [����� ������], �������.���� FROM �������
	JOIN ������ ON ������.id = �������.id_������
Go
CREATE VIEW �����View AS
	SELECT �����.id, �����.����������, �����.����� FROM �����

Go
CREATE VIEW �����View AS
	SELECT * FROM �����

GO
CREATE VIEW ����View AS
	SELECT * FROM ����

GO
CREATE VIEW ��������������View AS
	SELECT �����.id, ������.��������, ������.�������, ������.���������, ������.����, ����.������  FROM ������
	JOIN ����� ON �����.id = ������.id_������
	JOIN ���� ON ����.id = ������.id_����

GO
CREATE VIEW �����������View AS
	SELECT �����.id, ������.��������, ������.�������, ������.���������, ������.����, ����.������ FROM ������
	JOIN ����� ON �����.id_������ = ������.id
	JOIN ���� ON ����.id = ������.id_����

GO
CREATE VIEW SearchView AS
	SELECT ������.id, ����.�����, ����.������, ������.�������, ������.���������, ������.��������, ������.�������������, ������.���� FROM ������
	JOIN ���� ON ����.id = ������.id_����

GO
CREATE VIEW PriceListView AS
	SELECT (�������� + ' ' + �������) AS [��������], ���� FROM ������

GO
CREATE VIEW SalesInYearView AS
	SELECT YEAR(�������.����) AS  [���], ������.���������, ������.id FROM ������
	JOIN ������ ON ������.id_������ = ������.id
	JOIN ������� ON ������.id = �������.id_������

GO
CREATE VIEW TopDetailsView AS
	SELECT TOP 3 (�������� + ' ' + �������) AS [��������], COUNT(�������.����) AS [����������] FROM ������
	JOIN ������ ON ������.id_������ = ������.id
	JOIN ������� ON ������.id = �������.id_������
	GROUP BY (�������� + ' ' + �������)
	ORDER BY  COUNT(�������.����) DESC

GO
CREATE VIEW ����������������View AS
	SELECT �����.�����, (�������� + ' ' + �������) AS [��������]  FROM ������
	JOIN ����� ON �����.id = ������.id_������
	JOIN ���� ON ����.id = ������.id_����