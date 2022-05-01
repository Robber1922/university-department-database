--����� �������� ����� (�� ���������) ��� ������� �����;
USE uni;
GO
CREATE PROCEDURE ucheb_plan (@curs int)
AS
BEGIN
	SELECT ��������.ID_�������, ��������.��������, ��������.����, ��������.�������
	FROM ��������
	WHERE ��������.���� LIKE @curs
	ORDER BY ID_�������
END

exec ucheb_plan 4;


--����� ���������� ������� ��� ��������������;
--USE uni;
GO
CREATE PROCEDURE timetable (@teacher varchar(50))
AS
BEGIN
	SELECT ����������.�������, ����������.����_������, ����������.�����, ����������.������
	FROM ����������
	JOIN ������������� ON ����������.ID_������ = �������������.ID_������
	WHERE �������������.��� LIKE @teacher
	ORDER BY ����������.�����
END

exec timetable "������� ����� ����������";

--������ �������� ��� ��������������;

ALTER PROCEDURE nagruzka (@teacher varchar(50))
AS
BEGIN
	SELECT �����_����� = SUM(��������.����) FROM �������� 
	JOIN ������������� ON ��������.ID_������ = �������������.ID_������
	WHERE �������������.��� LIKE @teacher

	SELECT COUNT(����������.�������) AS '����� � ������' FROM ����������
	JOIN ������������� ON ����������.ID_������ = �������������.ID_������
	WHERE �������������.��� LIKE @teacher
END

exec nagruzka "������� ����� ����������";

--���� ������������� ����� ������� ������������ ����������;

ALTER PROCEDURE caf_plan 
AS
BEGIN
	SELECT �����.ID_�����, �����.��������, ��������.�������� AS '�������', �������������.��� AS '�����'
	FROM �����
	INNER JOIN �������� ON �����.ID_�������� = ��������.ID_�������
	INNER JOIN ������������� ON �����.ID_����� = �������������.ID_������
END

exec caf_plan;

--������������ ������ ���������� ����� ������� (�� ��������������, �� �������, �� �����������)
USE uni;
GO
ALTER PROCEDURE report1 (@teacher varchar(50))
AS
BEGIN
	SELECT �����.ID_�����, �����.�������� AS '��������', ��������.�������� AS '����������', �������������.��� AS '�����', �����.����, �����.����������
	FROM �����
	INNER JOIN ����� ON �����.ID_����� = �����.ID_����� 
	INNER JOIN ������������� ON �����.ID_����� = �������������.ID_������
	INNER JOIN �������� ON �����.ID_�������� = ��������.ID_�������
	WHERE �������������.��� LIKE @teacher
END

exec report1 '������ ������� ����������';

USE uni;
GO
ALTER PROCEDURE report2 (@date date)
AS
BEGIN
	SELECT �����.ID_�����, �����.�������� AS '��������', ��������.�������� AS '����������', �������������.��� AS '�����', �����.����, �����.����������
	FROM �����
	INNER JOIN ����� ON �����.ID_����� = �����.ID_����� 
	INNER JOIN ������������� ON �����.ID_����� = �������������.ID_������
	INNER JOIN �������� ON �����.ID_�������� = ��������.ID_�������
	WHERE �����.���� LIKE  @date
END

exec report2 '11.11.2018';

USE uni;
GO
CREATE PROCEDURE report3 (@subject varchar(50))
AS
BEGIN
	SELECT �����.ID_�����, �����.�������� AS '��������', ��������.�������� AS '����������', �������������.��� AS '�����', �����.����, �����.����������
	FROM �����
	INNER JOIN ����� ON �����.ID_����� = �����.ID_����� 
	INNER JOIN ������������� ON �����.ID_����� = �������������.ID_������
	INNER JOIN �������� ON �����.ID_�������� = ��������.ID_�������
	WHERE ��������.�������� LIKE @subject
END

exec report3 '�������������� ������';

--������� ���������-����������� (�� ��������������)
GO
ALTER PROCEDURE diplom (@teacher varchar(50))
AS
BEGIN
	SELECT �������.ID_�������, �������.���_���� AS '��� ��������', �������.������
	FROM �������
	INNER JOIN ������������� ON �������.ID_�������� = �������������.ID_������
	WHERE �������������.��� LIKE @teacher
	ORDER BY �������.���_����
END

exec diplom '�������� ����� ��������';

 