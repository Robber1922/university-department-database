--����� �������� ����� (�� ���������) ��� ������� �����;
USE uni;
GO
CREATE PROCEDURE ucheb_plan (@semestr int)
AS
BEGIN
	SELECT �������.ID_�������, �������.��������, �������.����, ��
	FROM �������
	WHERE �������.������� LIKE @semestr
	ORDER BY ID_�������
END

exec ucheb_plan 4;
