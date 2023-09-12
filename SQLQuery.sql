Create database CourseManagmentPortal
use CourseManagmentPortal

Create Table Course(
cs_id int primary key identity(1,1) not null,
cs_Name nvarchar(30),
cs_Duration nvarchar(30),
cs_Price float,
cs_CreationTime datetime,
cs_ModificationTime datetime
)


create Table Students(
st_Id int primary key identity(1,1) not null,
st_Name nvarchar(30),
st_Surname nvarchar(40),
st_BirthDate Date,
st_CreationTime datetime,
st_ModificationTime datetime,
cs_id int ,
constraint fk_cs_st foreign key(cs_id)
references Course(cs_id),
tc_Id int ,
constraint fk_tc_st foreign key(tc_Id)
references Teachers(tc_Id)
)

drop table Students
drop table Course
drop Table Teachers
create Table Teachers(
tc_Id int primary key identity(1,1) not null,
tc_Name nvarchar(30),
tc_Surname nvarchar(30),
tc_BirthDate Date,
tc_Profession nvarchar(40),
tc_ModificationTime datetime
)

create table PlanningCourse(
id int primary key identity(1,1) not null,
Course nvarchar(30),
Student nvarchar(30),
Teacher nvarchar(30),
Duration nvarchar(30),
Price float,
PlannedStartingDate datetime,
StartedDate datetime

)



select *from PlanningCourse where StartedDate is not null
select * from PlanningCourse order by Course

Update PlanningCourse set StartedDate=GETDATE()
Update PlanningCourse set StartedDate=getdate() where Course='Logic'

select COUNT(Student) from PlanningCourse where Course='Math'

drop table PlanningCourse


select * From Students
select * from Teachers

update Students set st_Name='GulBala1', st_Surname='Nuraliyev' where st_id=1

select c.cs_Name Courses,
s.st_Name+' '+s.st_Surname Students,
t.tc_Name+' '+t.tc_Surname Teachers,
c.cs_Duration Duration,c.cs_Price Price,
c.cs_CreationTime CreationTime,
c.cs_ModificationTime ModificationTime
from Course c 
inner join Students s on s.cs_id=c.cs_id
inner join Teachers t on s.tc_Id=t.tc_Id

select * from STUDENTS
select * from Teachers
select tc_Id from Teachers  where tc_Name+' '+tc_Surname='Samira Akimova'

ALTER TABLE Students
    NOCHECK CONSTRAINT fk_cs_st
UPDATE Students set cs_id=3,tc_Id=5 where st_Name='Kamil'



ALTER TABLE Students  NOCHECK CONSTRAINT fk_tc_st UPDATE Students set  tc_Id =5 where st_Name+' '+st_Surname ='Sanan Huseynov'


insert into Students(st_Name,st_Surname,st_BirthDate,cs_id,tc_Id) values()