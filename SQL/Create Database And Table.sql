update Student set age = 10 where StudentId =1

select * from Student
update Student set age=22 where StudentId=3
select*from Student
select * from Student where age<20;
select*from Student
alter table student alter column name varchar(100)
select *from Student
insert into Student(studentid,name,city,age)values (4,'ddd','d'),(2,'bbb','b'),(5,'eee','e');
select*from Student
insert into Student(studentid,name,city,age)values (4,'ddd','d',24),(2,'bbb','b',15),(5,'eee','e',50);
select*from Student
alter table student drop column city
select*from Student
select top 2*from Student
SELECT *FROM Student;
select name from Student
SELECT *FROM Student;
select *from student order by age asc
select * from student
select *from student order by age desc

create table student_course
(
	StudentId int,
	course varchar(50)
);

select *from student join student_course on Student.StudentId=student_course.StudentId
select*from student
INSERT INTO Student_Course (StudentId, Course)values(1,'java'),(2,'c#'),(1,'python'),(1,'c++'),(1,'c')
select*from student_course