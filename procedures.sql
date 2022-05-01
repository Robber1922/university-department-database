--поиск учебного плана (по семестрам) для каждого курса;
USE uni;
GO
CREATE PROCEDURE ucheb_plan (@curs int)
AS
BEGIN
	SELECT Предметы.ID_предмет, Предметы.Название, Предметы.Часы, Предметы.Семестр
	FROM Предметы
	WHERE Предметы.Курс LIKE @curs
	ORDER BY ID_предмет
END

exec ucheb_plan 4;


--поиск расписания занятий для преподавателей;
--USE uni;
GO
CREATE PROCEDURE timetable (@teacher varchar(50))
AS
BEGIN
	SELECT Расписание.Предмет, Расписание.День_недели, Расписание.Время, Расписание.Группа
	FROM Расписание
	JOIN Преподаватель ON Расписание.ID_препод = Преподаватель.ID_препод
	WHERE Преподаватель.ФИО LIKE @teacher
	ORDER BY Расписание.Время
END

exec timetable "Суворов Тарас Робертович";

--расчет нагрузки для преподавателей;

ALTER PROCEDURE nagruzka (@teacher varchar(50))
AS
BEGIN
	SELECT Всего_часов = SUM(Предметы.Часы) FROM Предметы 
	JOIN Преподаватель ON Предметы.ID_препод = Преподаватель.ID_препод
	WHERE Преподаватель.ФИО LIKE @teacher

	SELECT COUNT(Расписание.Предмет) AS 'Часов в неделю' FROM Расписание
	JOIN Преподаватель ON Расписание.ID_препод = Преподаватель.ID_препод
	WHERE Преподаватель.ФИО LIKE @teacher
END

exec nagruzka "Суворов Тарас Робертович";

--учет кафедрального плана изданий методической литературы;

ALTER PROCEDURE caf_plan 
AS
BEGIN
	SELECT Книга.ID_книги, Книга.Название, Предметы.Название AS 'Предмет', Преподаватель.ФИО AS 'Автор'
	FROM Книга
	INNER JOIN Предметы ON Книга.ID_предмета = Предметы.ID_предмет
	INNER JOIN Преподаватель ON Книга.ID_автор = Преподаватель.ID_препод
END

exec caf_plan;

--формирование отчета выполнения плана изданий (по преподавателям, по месяцам, по дисциплинам)
USE uni;
GO
ALTER PROCEDURE report1 (@teacher varchar(50))
AS
BEGIN
	SELECT Отчет.ID_отчет, Книга.Название AS 'Название', Предметы.Название AS 'Дисциплина', Преподаватель.ФИО AS 'Автор', Отчет.Дата, Отчет.Количество
	FROM Отчет
	INNER JOIN Книга ON Отчет.ID_книги = Книга.ID_книги 
	INNER JOIN Преподаватель ON Книга.ID_автор = Преподаватель.ID_препод
	INNER JOIN Предметы ON Книга.ID_предмета = Предметы.ID_предмет
	WHERE Преподаватель.ФИО LIKE @teacher
END

exec report1 'Белова Надежда Михаиловна';

USE uni;
GO
ALTER PROCEDURE report2 (@date date)
AS
BEGIN
	SELECT Отчет.ID_отчет, Книга.Название AS 'Название', Предметы.Название AS 'Дисциплина', Преподаватель.ФИО AS 'Автор', Отчет.Дата, Отчет.Количество
	FROM Отчет
	INNER JOIN Книга ON Отчет.ID_книги = Книга.ID_книги 
	INNER JOIN Преподаватель ON Книга.ID_автор = Преподаватель.ID_препод
	INNER JOIN Предметы ON Книга.ID_предмета = Предметы.ID_предмет
	WHERE Отчет.Дата LIKE  @date
END

exec report2 '11.11.2018';

USE uni;
GO
CREATE PROCEDURE report3 (@subject varchar(50))
AS
BEGIN
	SELECT Отчет.ID_отчет, Книга.Название AS 'Название', Предметы.Название AS 'Дисциплина', Преподаватель.ФИО AS 'Автор', Отчет.Дата, Отчет.Количество
	FROM Отчет
	INNER JOIN Книга ON Отчет.ID_книги = Книга.ID_книги 
	INNER JOIN Преподаватель ON Книга.ID_автор = Преподаватель.ID_препод
	INNER JOIN Предметы ON Книга.ID_предмета = Предметы.ID_предмет
	WHERE Предметы.Название LIKE @subject
END

exec report3 'Математический анализ';

--списков студентов-дипломников (по преподавателям)
GO
ALTER PROCEDURE diplom (@teacher varchar(50))
AS
BEGIN
	SELECT Студент.ID_студент, Студент.ФИО_студ AS 'ФИО студента', Студент.Группа
	FROM Студент
	INNER JOIN Преподаватель ON Студент.ID_научника = Преподаватель.ID_препод
	WHERE Преподаватель.ФИО LIKE @teacher
	ORDER BY Студент.ФИО_студ
END

exec diplom 'Журавлёва Нинна Петровна';

 