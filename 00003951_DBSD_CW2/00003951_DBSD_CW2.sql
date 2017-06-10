/* DDL FILE */

/*Query for dropping db*/

USE master;
IF EXISTS(select * from sys.databases where name='HR')
DROP DATABASE HR

CREATE DATABASE HR

USE HR;


/* create query for creating table of departments*/
CREATE TABLE department(
  department_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  department_name varchar(200) NOT NULL
);

CREATE TABLE department_backup (
  department_id INT NOT NULL,
  department_name varchar(200) NOT NULL,
  logged_at datetime,
  operation nvarchar(50),
  performer nvarchar(50),
);

/* create query for creating table of recruiters*/
CREATE TABLE recruiter(
  recruiter_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  recruiter_name varchar(250) NOT NULL,
  recruiter_email varchar(200) NOT NULL,
  recruiter_password varchar(200) NOT NULL 
);
/* create query for creating table of vacancies*/
CREATE TABLE vacancy(
   vacancy_id INT NOT NULL IDENTITY(1,1) Primary Key,
   department_id INT references department(department_id),
   recruiter_id INT references recruiter(recruiter_id),
   vacancy_title varchar(250) NOT NULL,
   vacancy_description varchar(500) NOT NULL,
   vacancy_is_open bit DEFAULT 1,
   vacancy_created_at date NOT NULL,
   vacancy_is_archived bit DEFAULT 0,
);
  
/* create query for creating table of stages*/
CREATE TABLE stage(
  stage_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  stage_name varchar(20) NOT NULL,
  stage_order INT NOT NULL
);

/* create query for creating table of applications*/
CREATE TABLE application(
    application_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    vacancy_id INT references vacancy(vacancy_id),
    stage_id INT references stage(stage_id),
    candidate_email varchar(250) NOT NULL,
    candidate_name varchar(250) NOT NULL,
    candidate_phone varchar(250) NOT NULL,
    candidate_address varchar(250) NOT NULL,
    is_disqualified bit DEFAULT 0,
    application_created_at date NOT NULL,
);

GO

/* Creating triggers on department */

CREATE TRIGGER TRG_InsertDepartment
ON [dbo].[department]
AFTER INSERT AS
BEGIN

INSERT INTO department_backup(
  department_id,
  department_name,
  logged_at,
  operation,
  performer
)
SELECT 
  department_id,
  department_name,
  getdate(),
  'INSERT',
  user
    FROM INSERTED
END

CREATE TRIGGER TRG_UPDATE_Department
ON [dbo].[department]
AFTER UPDATE AS
BEGIN

INSERT INTO department_backup(
  department_id,
  department_name,
  logged_at,
  operation,
  performer
)
SELECT 
  department_id,
  department_name,
  getdate(),
  'UPDATED',
  user
    FROM INSERTED
END

/*Queries creating necessary indexes*/
    
/* intended to optimize search by vacancy title */
CREATE INDEX idx_vacancy_title
ON vacancy ([vacancy_title]);

/* intended to optimize search by vacancy description */
CREATE INDEX idx_vacancy_description
ON vacancy ([vacancy_description]);

/* intended to optimize fecthing vacancies created by logged in user */
CREATE INDEX idx_recruiter_id
ON vacancy ([recruiter_id]);

/* intended to optimize fecthing application on particular vacancy */
CREATE INDEX idx_vacancy_id
ON application ([vacancy_id]);




/* INSERT SAMPLE DATA */

/*Inserting data into department table */
INSERT INTO department (department_name) 
VALUES 
('Marketing'),
('Distributor'),
('Delivery'),
('Accountant'),
('General');

/*Inserting data into recruiter table */
INSERT INTO recruiter (recruiter_name, recruiter_email, recruiter_password) 
VALUES 
('Ulugbek Tadjibaev', 'tadjibaev@gmail.com', 'password123'),
('Karamiddin Sharapov', 'sharapov@gmail.com', 'password123'),
('Baxtiyor Tadjibaev', 'btadjibaev@gmail.com', 'password123'),
('Sherxon Obidov', 'obidov@gmail.com', 'password123'),
('Sato Kurasava', 'kurasava@gmail.com', 'password123'),
('Thee Saym', 'saym@gmail.com', 'password123');


/*Inserting data into vacancy table */
INSERT INTO vacancy (department_id, recruiter_id, vacancy_title, vacancy_description, vacancy_created_at) 
VALUES 
(2, 1, 'New Opening in Distributor department', 'Some vacancy description goes here', '1-13-2013'),
(3, 1, 'New Opening in Delivery department', 'Some vacancy description goes here', '8-10-2013'),
(3, 2, 'Skilled delivery guys are needed at Bayer', 'Some vacancy description goes here', '2-22-2014'),
(4, 4, 'Accountants with 4 years of experience at least', 'Some vacancy description goes here', '1-20-2015'),
(5, 2, 'Genitor for Bayer company is needed', 'Some vacancy description goes here', '4-22-2015'),
(5, 4, 'Guards for Bayer company is needed', 'Some vacancy description goes here', '3-22-2015'),
(1, 3, 'Bayer is hiring high educated and skilled Marketing managers', 'Some vacancy description goes here', '6-27-2015'),
(1, 4, 'Bayer is Marketing managers assistant', 'Some vacancy description goes here', '2-28-2015'),
(4, 3, 'Bayer is hiring high educated and skilled accountants', 'Some vacancy description goes here', '6-12-2016'),
(2, 5, 'Distributor truck driver is required in Bayer LTD', 'Some vacancy description goes here', '8-21-2016'),
(3, 5, 'Untitled job', 'Some vacancy description goes here', '8-24-2016');

/*Inserting data into candidate table */
/* 
INSERT INTO candidate(candidate_email, candidate_name, candidate_phone, candidate_address, candidate_resume) 
VALUES
('johnkarlik@gmail.com', 'John Karlik', '+998901548796', 'Some address specified here', 'path/to/resume/file.pdf'),
('dexter@gmail.com', 'Bahodir Boydedaev', '+998991325478', 'Some address specified here', 'path/to/resume/file.pdf'),
('bunyod@gmail.com', 'Bunyod Xoshimaliev', '+998977361245', 'Some address specified here', 'path/to/resume/file.pdf'),
('ravshan@gmail.com', 'Ravshan Bilmimanev', '+998971789456', 'Some address specified here', 'path/to/resume/file.pdf'),
('rustambek@gmail.com', 'Rustambek Xoshimaliev', '+998971234578', 'Some address specified here', 'path/to/resume/file.pdf'),
('madina.karimjonova@gmail.com', 'Madina Karimjonova', '+998977894562', 'Some address specified here', 'path/to/resume/file.pdf'),
('mamadali@gmail.com', 'Mamadali Jalolov', '+998971234576', 'Some address specified here', 'path/to/resume/file.pdf'),
('shohjahon@gmail.com', 'Shohjahon Jurayev', '+998917453612', 'Some address specified here', 'path/to/resume/file.pdf'),
('oybek@gmail.com', 'Oybek Alimov', '+998914653648', 'Some address specified here', 'path/to/resume/file.pdf'),
('hamid@gmail.com', 'Hamidullo Maxkamov', '+998934675795', 'Some address specified here', 'path/to/resume/file.pdf'),
('jamshid@gmail.com', 'Jamshid Maxsudov', '+998914321648', 'Some address specified here', 'path/to/resume/file.pdf'),
('theesaym@gmail.com', 'Thee Saym', '+998914321648', 'Some address specified here', 'path/to/resume/file.pdf');

*/

/*Inserting data into stage table */
INSERT INTO stage(stage_name, stage_order) 
VALUES
('Applied', 1), 
('Phone Interview', 2), 
('Main Interview', 3), 
('Skill Test', 4), 
('Offered', 5),
('Hired', 6);

/* Inserting data into application table */

INSERT INTO application (vacancy_id, stage_id, candidate_email, candidate_name, candidate_phone, candidate_address, application_created_at)
VALUES
(1, 1, 'dexter@gmail.com', 'Bahodir Boydedaev', '+998991325478', 'Some address specified here', '1-14-2013'),
(1, 2, 'johnkarlik@gmail.com', 'John Karlik', '+998901548796', 'Some address specified here', '1-14-2013'),
(1, 1, 'madina.karimjonova@gmail.com', 'Madina Karimjonova', '+998977894562', 'Some address specified here', '1-15-2013'),
(2, 3, 'shohjahon@gmail.com', 'Shohjahon Jurayev', '+998917453612', 'Some address specified here', '8-25-2013'),
(2, 1, 'jamshid@gmail.com', 'Jamshid Maxsudov', '+998914321648', 'Some address specified here', '8-25-2013'),
(2, 1, 'hamid@gmail.com', 'Hamidullo Maxkamov', '+998934675795', 'Some address specified here', '8-25-2013'),
(2, 1, 'oybek@gmail.com', 'Oybek Alimov', '+998914653648', 'Some address specified here', '8-26-2013'),
(3, 1, 'hamid@gmail.com', 'Jamshid Maxsudov', '+998914321648', 'Some address specified here', '2-23-2014'),
(3, 3, 'rustambek@gmail.com', 'Rustambek Xoshimaliev', '+998971234578', 'Some address specified here', '2-24-2014'),
(3, 1, 'ravshan@gmail.com', 'Ravshan Bilmimanev', '+998971789456', 'Some address specified here', '2-25-2014'),
(3, 4, 'johnkarlik@gmail.com', 'John Karlik', '+998901548796', 'Some address specified here', '2-24-2014'),
(4, 3, 'bunyod@gmail.com', 'Bunyod Xoshimaliev', '+998977361245', 'Some address specified here', '1-21-2015'),
(4, 5, 'mamadali@gmail.com', 'Mamadali Jalolov', '+998971234576', 'Some address specified here', '1-25-2015'),
(4, 2, 'ravshan@gmail.com', 'Ravshan Bilmimanev', '+998971789456', 'Some address specified here', '1-27-2015'),
(5, 1, 'jamshid@gmail.com', 'Jamshid Maxsudov', '+998914321648', 'Some address specified here', '4-22-2015'),
(5, 1, 'madina.karimjonova@gmail.com', 'Madina Karimjonova', '+998977894562', 'Some address specified here', '4-24-2015'),
(5, 1, 'shohjahon@gmail.com', 'Shohjahon Jurayev', '+998917453612', 'Some address specified here', '4-26-2015'),
(6, 4, 'oybek@gmail.com', 'Oybek Alimov', '+998914653648', 'Some address specified here', '3-22-2015'),
(6, 4, 'dexter@gmail.com', 'Bahodir Boydedaev', '+998991325478', 'Some address specified here', '3-26-2015'),
(7, 5, 'ravshan@gmail.com', 'Ravshan Bilmimanev', '+998971789456', 'Some address specified here', '6-28-2015'),
(7, 4, 'mamadali@gmail.com', 'Mamadali Jalolov', '+998971234576', 'Some address specified here', '6-29-2015'),
(8, 6, 'dexter@gmail.com', 'Bahodir Boydedaev', '+998991325478', 'Some address specified here', '3-2-2015'),
(8, 5, 'hamid@gmail.com', 'Hamidullo Maxkamov', '+998934675795', 'Some address specified here', '3-12-2015'),
(9, 1, 'ravshan@gmail.com', 'Ravshan Bilmimanev', '+998971789456', 'Some address specified here', '6-13-2016'),
(10, 2, 'johnkarlik@gmail.com', 'John Karlik', '+998901548796', 'Some address specified here', '8-25-2016');

GO
/* PROCEDURES & FUNCTIONS */

/* update department */
CREATE PROCEDURE udpUpdateDepartment 
         @department_id int, 
         @department_name nvarchar(200)
        AS 
        BEGIN
          UPDATE [dbo].[department]
          SET   [department_name] = @department_name
          WHERE   [department_id] = @department_id;
        END

GO
/* create department */
CREATE PROCEDURE udpCreateDepartment
              @department_name nvarchar(200)
              AS 
              BEGIN
                INSERT INTO [dbo].[department]([department_name]) VALUES (@department_name);
              END
/* create recruiter */
GO

CREATE PROCEDURE udpCreateRecruiter
              @recruiter_name nvarchar(250),
              @recruiter_email nvarchar(200),
              @recruiter_password nvarchar(200)
              AS 
              BEGIN
                INSERT INTO [dbo].[recruiter](
                  [recruiter_name],
                  [recruiter_email],
                  [recruiter_password]
                  ) VALUES (
                  @recruiter_name,
                  @recruiter_email,
                  @recruiter_password
                  );
              END;

GO
/* check login */
CREATE FUNCTION udfLogin (@email varchar(200), @pass varchar(200))
        RETURNS bit
        AS
        BEGIN
       declare @result bit = 0;
            IF exists(select 1 from dbo.recruiter where [recruiter_email] = @email and [recruiter_password] = @pass) 
            set @result = 1;    

return @result;
        END;

GO
/* create vacancy */

CREATE PROCEDURE udpCreateVacancy 
        @department_id int,
        @recruiter_id int,
        @vacancy_title nvarchar(250),
        @vacancy_description nvarchar(500),
        @vacancy_created_at date
        AS
        BEGIN
        INSERT INTO [dbo].[vacancy](
          [department_id],
          [recruiter_id],
          [vacancy_title],
          [vacancy_description],
          [vacancy_created_at])
          VALUES (
          @department_id,
          @recruiter_id,
          @vacancy_title,
          @vacancy_description,
          @vacancy_created_at
          );
        END;

GO
/* update vacancy */

CREATE PROCEDURE udpUpdateVacancy 
        @vacancy_id int,
        @department_id int,
        @vacancy_title nvarchar(250),
        @vacancy_description nvarchar(500)
        AS 
        BEGIN
          UPDATE [dbo].[vacancy]
          SET
            [department_id] = @department_id,
            [vacancy_title] = @vacancy_title,
            [vacancy_description] = @vacancy_description
          WHERE   [vacancy_id] = @vacancy_id;
        END
GO
/* create application */

CREATE PROCEDURE udpCreateApplication
        @vacancy_id int,
        @stage_id int,
        @candidate_email nvarchar(250),
        @candidate_name nvarchar(250),
        @candidate_phone nvarchar(250),
        @candidate_address nvarchar(250),
        @application_created_at datetime
        AS
        BEGIN
        INSERT INTO [dbo].[application](
                [vacancy_id],
                [stage_id],
                [candidate_email],
                [candidate_name],
                [candidate_phone],
                [candidate_address],
                [application_created_at]
              ) VALUES (
                @vacancy_id,
                @stage_id,
                @candidate_email,
                @candidate_name,
                @candidate_phone,
                @candidate_address,
                @application_created_at
                );
        END;

GO

CREATE FUNCTION udfReport(@vacancy_id int, @recruiter_id int)
  RETURNS TABLE
    AS
        RETURN( 
      SELECT s.stage_id, s.stage_name, v.vacancy_title, count(a.stage_id) as cand_count FROM [dbo].[stage] s
      LEFT JOIN [dbo].[application] a on a.stage_id = s.stage_id
      JOIN [dbo].[vacancy] v on a.vacancy_id = v.vacancy_id
      WHERE a.vacancy_id = @vacancy_id AND v.recruiter_id = @recruiter_id AND a.is_disqualified = 0
      GROUP BY s.stage_id, s.stage_name, v.vacancy_title
        );
