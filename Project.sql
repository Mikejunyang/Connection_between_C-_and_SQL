CREATE DATABASE College1en;
GO

USE College1en;

CREATE TABLE Programs(
	ProgId VARCHAR(5) NOT NULL,
	ProgName VARCHAR(50) NOT NULL,
	PRIMARY KEY(ProgId));

CREATE TABLE Courses(
	CId VARCHAR(7) NOT NULL,
	CName VARCHAR(50) NOT NULL,
	ProgId VARCHAR(5) NOT NULL,
	PRIMARY KEY(CId),
	FOREIGN KEY(ProgId) REFERENCES Programs(ProgId)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Students(
	StId VARCHAR(10) NOT NULL,
	StName VARCHAR(50) NOT NULL,
	ProgId VARCHAR(5) NOT NULL,
	PRIMARY KEY(StId),
	FOREIGN KEY(ProgId) REFERENCES Programs(ProgId)
		ON DELETE NO ACTION
		ON UPDATE CASCADE
);

CREATE TABLE Enrollments(
	StId VARCHAR(10) NOT NULL,
	CId VARCHAR(7) NOT NULL,
	FinalGrade INTEGER,
	PRIMARY KEY(StId, CId),
	FOREIGN KEY(StId) REFERENCES Students(StId)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FOREIGN KEY(CId) REFERENCES Courses(CId)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
);

USE College1en;

INSERT INTO Programs (ProgId, ProgName)
VALUES	('P0001', 'Computer Programming'),
		('P0002', 'Computer Science');


INSERT INTO Courses (CId, CName, ProgId)
VALUES	('C000001', 'Database 1', 'P0002'),
		('C000002', 'Web Development', 'P0002'),
		('C000003', 'Information System', 'P0001');


INSERT INTO Students (StId, StName, ProgId)
VALUES	('S000000001', 'Mary', 'P0001'),
		('S000000002', 'Jane', 'P0001'),
		('S000000003', 'John', 'P0001'),
		('S000000004', 'Brian', 'P0001'),
		('S000000005', 'Anne', 'P0002'),
		('S000000006', 'James', 'P0002'),
		('S000000007', 'Eddy', 'P0002'),
		('S000000008', 'Harry', 'P0002');


INSERT INTO Enrollments (StId, CId, FinalGrade)
VALUES	('S000000001', 'C000003', 98),
		('S000000002', 'C000003', 78),
		('S000000006', 'C000002', 59),
		('S000000008', 'C000001', 23);