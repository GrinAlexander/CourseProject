CREATE DATABASE AutoParts --�������� ��

USE AutoParts

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
	�������� varchar(100) NOT NULL,
	������������� varchar(50) NOT NULL,
	��������� varchar(30) NOT NULL,
	���� float NOT NULL,
	FOREIGN KEY (id_������) REFERENCES �����(id)
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

CREATE TABLE ��������� --�������� ������� "���������"
(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_������ int NOT NULL,
	�������� varchar(50) NOT NULL,
	FOREIGN KEY (id_������) REFERENCES �����(id)
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
INSERT INTO ����� VALUES (N'���������� 113', 1500), (N'����������� 15', 250), (N'������� 11�', 300), (N'�������� 3�', 300), (N'��������������� 27', 500)

INSERT INTO ������ VALUES 
(1, '������ �������� A210115', 'DENCKERMANN', N'�� � �������', 5.20),
(2, '������ ��������� CMZ11431', 'COMLINE', N'�� � �������', 3.29),
(3, '������� CF-509', 'JAPANPARTS', N'������� �������', 38.04),
(4, '��������� ����� CT829', 'CONTITECH', N'��������� � ������', 28.40),
(5, '��������� ������� ADC1246V', 'COMLINE', N'��������� �������', 69.62),
(1, '����������� 6��60�1�', 'Ista Power Optimal', N'���� � ������������', 86.90)

INSERT INTO ���� VALUES (5, '������� �� ��������� �����'), (4, '����� ���� ����� ���')

INSERT INTO ����� VALUES (1, 10, 52.0), (2, 5, 16.45), (3, 10, 380.04) , (4, 3, 85.2), (5, 5, 348.1)

INSERT INTO ��������� VALUES (1, 'Autosell'), (2, 'Autosell'), (3, 'ATR'), (4, 'AMR'), (5, 'ATR')

INSERT INTO ������ VALUES (1, 1, 5.20), (2, 3, 9.87), (3, 2, 76.08), (4, 1, 69.62), (5, 1, 86.9)

INSERT INTO ������� VALUES (1, '26/9/2018'), (2, '26/9/2019'), (3, '25/9/2019'), (4, '20/9/2019')

Go
CREATE VIEW ������View AS
	SELECT ������.id, �����.�����, ������.��������, ������.���������, ������.���� FROM dbo.������
	JOIN ����� ON �����.id = ������.id_������

Go
CREATE VIEW �����View AS
	SELECT �����.id, ������.��������, �����.����������, �����.����� FROM �����
	JOIN ������ ON ������.id = �����.id_������

Go
CREATE VIEW ������View AS
	SELECT ������.id, ������.��������, ������.����������, ������.����� FROM ������
	JOIN ������ ON ������.id = ������.id_������

Go
CREATE VIEW ���������View AS
	SELECT ���������.id, �����.id AS [����� ������], ���������.�������� FROM ���������
	JOIN ����� ON �����.id = ���������.id_������

Go
CREATE VIEW �������View AS
	SELECT �������.id, ������.id AS [����� ������], �������.���� FROM �������
	JOIN ������ ON ������.id = �������.id_������

Go
CREATE VIEW �����View AS
	SELECT * FROM �����