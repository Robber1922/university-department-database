--поиск учебного плана (по семестрам) для каждого курса;
USE uni;
GO
CREATE PROCEDURE ucheb_plan (@semestr int)
AS
BEGIN
	SELECT Предмет.ID_предмет, Предмет.Название, Предмет.Часы, Пр
	FROM Предмет
	WHERE Предмет.Семестр LIKE @semestr
	ORDER BY ID_предмет
END

exec ucheb_plan 4;
