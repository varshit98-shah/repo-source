use studentdb

CREATE TABLE  Student
(
	StudentId INT PRIMARY KEY,
	Name VARCHAR(50),
	city VARCHAR(50)
);
select * from Student;
INSERT INTO Student(StudentId,Name,city)VALUES(1,'aaa','a'),(2,'bbb','b'),(3,'ccc','c');
Select*from student;
delete from student where studentid='2';
SELECT *FROM Student;
alter table student add age int;
SELECT *FROM Student;
update  student set age='20'where studentid=1;

