-- ============================================================
-- University Transport DB - Full Database Script
-- ============================================================

-- 1. Drop database if exists
IF DB_ID('UniversityTransportDB') IS NOT NULL
BEGIN
    ALTER DATABASE UniversityTransportDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE UniversityTransportDB;
END
GO

-- 2. Create database
CREATE DATABASE UniversityTransportDB;
GO

-- 3. Use the database
USE UniversityTransportDB;
GO

-- ============================================================
-- 4. Create tables
-- ============================================================

-- 4.1 Wilaya
CREATE TABLE Wilaya (
    WilayaID INT IDENTITY(1,1) NOT NULL,
    WilayaName NVARCHAR(100) NOT NULL,
    WilayaCode NVARCHAR(10) NOT NULL,
    CONSTRAINT PK_Wilaya PRIMARY KEY (WilayaID),
    CONSTRAINT UQ_Wilaya_WilayaCode UNIQUE (WilayaCode)
);
GO

-- 4.2 Municipality
CREATE TABLE Municipality (
    MunicipalityID INT IDENTITY(1,1) NOT NULL,
    WilayaID INT NOT NULL,
    MunicipalityName NVARCHAR(100) NOT NULL,
    PostalCode NVARCHAR(20) NULL,
    CONSTRAINT PK_Municipality PRIMARY KEY (MunicipalityID),
    CONSTRAINT FK_Municipality_Wilaya FOREIGN KEY (WilayaID) REFERENCES Wilaya(WilayaID),
    CONSTRAINT UQ_Municipality_WilayaID_MunicipalityName UNIQUE (WilayaID, MunicipalityName)
);
GO

-- 4.3 Department
CREATE TABLE Department (
    DepartmentID INT IDENTITY(1,1) NOT NULL,
    DepartmentName NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_Department PRIMARY KEY (DepartmentID),
    CONSTRAINT UQ_Department_DepartmentName UNIQUE (DepartmentName)
);
GO

-- 4.4 Specialty
CREATE TABLE Speciality (
    SpecialityID INT IDENTITY(1,1) NOT NULL,
    DepartmentID INT NOT NULL,
    SpecialityName NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_Speciality PRIMARY KEY (SpecialityID),
    CONSTRAINT FK_Speciality_Department FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID),
    CONSTRAINT UQ_Speciality_DepartmentID_SpecialityName UNIQUE (DepartmentID, SpecialityName)
);
GO

-- 4.5 Person
CREATE TABLE Person (
    PersonID INT IDENTITY(1,1) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    MidName NVARCHAR(50) NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NULL,
    Gender BIT NOT NULL,
    PhoneNumber NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(200) NULL,
    CONSTRAINT PK_Person PRIMARY KEY (PersonID)
);
GO

-- 4.6 Employee
CREATE TABLE Employee (
    EmployeeID INT IDENTITY(1,1) NOT NULL,
    PersonID INT NOT NULL,
    HireDate DATE NOT NULL,
    EmployeeStatus BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Employee PRIMARY KEY (EmployeeID),
    CONSTRAINT FK_Employee_Person FOREIGN KEY (PersonID) REFERENCES Person(PersonID),
    CONSTRAINT UQ_Employee_PersonID UNIQUE (PersonID)
);
GO

-- 4.7 Driver
CREATE TABLE Driver (
    DriverID INT IDENTITY(1,1) NOT NULL,
    EmployeeID INT NOT NULL,
    LicenseNumber NVARCHAR(50) NOT NULL,
    LicenseExpiryDate DATE NOT NULL,
    DriverStatus BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Driver PRIMARY KEY (DriverID),
    CONSTRAINT FK_Driver_Employee FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    CONSTRAINT UQ_Driver_EmployeeID UNIQUE (EmployeeID),
    CONSTRAINT UQ_Driver_LicenseNumber UNIQUE (LicenseNumber)
);
GO

-- 4.8 Student
CREATE TABLE Student (
    StudentID INT IDENTITY(1,1) NOT NULL,
    PersonID INT NOT NULL,
    SpecialityID INT NOT NULL,
    StudentStatus BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Student PRIMARY KEY (StudentID),
    CONSTRAINT FK_Student_Person FOREIGN KEY (PersonID) REFERENCES Person(PersonID),
    CONSTRAINT FK_Student_Speciality FOREIGN KEY (SpecialityID) REFERENCES Speciality(SpecialityID),
    CONSTRAINT UQ_Student_PersonID UNIQUE (PersonID)
);
GO

-- 4.9 Station
CREATE TABLE Station (
    StationID INT IDENTITY(1,1) NOT NULL,
    StationName NVARCHAR(100) NOT NULL,
    LocationDescription NVARCHAR(200) NULL,
    MunicipalityID INT NOT NULL,
    StationStatus BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Station PRIMARY KEY (StationID),
    CONSTRAINT FK_Station_Municipality FOREIGN KEY (MunicipalityID) REFERENCES Municipality(MunicipalityID)
);
GO

-- 4.10 BusModel
CREATE TABLE BusModel (
    BusModelID INT IDENTITY(1,1) NOT NULL,
    ModelName NVARCHAR(100) NOT NULL,
    ManufacturerName NVARCHAR(100) NOT NULL,
    DefaultCapacity INT NOT NULL,
    CONSTRAINT PK_BusModel PRIMARY KEY (BusModelID),
    CONSTRAINT CK_BusModel_DefaultCapacity CHECK (DefaultCapacity > 0)
);
GO

-- 4.11 Bus
CREATE TABLE Bus (
    BusID INT IDENTITY(1,1) NOT NULL,
    BusModelID INT NOT NULL,
    PlateNumber NVARCHAR(20) NOT NULL,
    BusCode NVARCHAR(50) NULL,
    ManufacturingYear INT NULL,
    BusStatus BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Bus PRIMARY KEY (BusID),
    CONSTRAINT FK_Bus_BusModel FOREIGN KEY (BusModelID) REFERENCES BusModel(BusModelID),
    CONSTRAINT UQ_Bus_PlateNumber UNIQUE (PlateNumber),
    CONSTRAINT CK_Bus_ManufacturingYear CHECK (ManufacturingYear IS NULL OR ManufacturingYear >= 1900)
);
GO

-- 4.12 TransportLine
CREATE TABLE TransportLine (
    TransportLineID INT IDENTITY(1,1) NOT NULL,
    LineName NVARCHAR(100) NOT NULL,
    OriginStationID INT NOT NULL,
    DestinationStationID INT NOT NULL,
    LineStatus BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_TransportLine PRIMARY KEY (TransportLineID),
    CONSTRAINT FK_TransportLine_OriginStation FOREIGN KEY (OriginStationID) REFERENCES Station(StationID),
    CONSTRAINT FK_TransportLine_DestinationStation FOREIGN KEY (DestinationStationID) REFERENCES Station(StationID),
    CONSTRAINT CK_TransportLine_DifferentStations CHECK (OriginStationID <> DestinationStationID)
);
GO

-- 4.13 LineStation
CREATE TABLE LineStation (
    LineStationID INT IDENTITY(1,1) NOT NULL,
    TransportLineID INT NOT NULL,
    StationID INT NOT NULL,
    StationOrder INT NOT NULL,
    DistanceFromOrigin DECIMAL(10,2) NULL,
    CONSTRAINT PK_LineStation PRIMARY KEY (LineStationID),
    CONSTRAINT FK_LineStation_TransportLine FOREIGN KEY (TransportLineID) REFERENCES TransportLine(TransportLineID),
    CONSTRAINT FK_LineStation_Station FOREIGN KEY (StationID) REFERENCES Station(StationID),
    CONSTRAINT CK_LineStation_StationOrder CHECK (StationOrder > 0),
    CONSTRAINT CK_LineStation_DistanceFromOrigin CHECK (DistanceFromOrigin IS NULL OR DistanceFromOrigin >= 0),
    CONSTRAINT UQ_LineStation_TransportLineID_StationOrder UNIQUE (TransportLineID, StationOrder),
    CONSTRAINT UQ_LineStation_TransportLineID_StationID UNIQUE (TransportLineID, StationID)
);
GO

-- 4.14 Schedule
CREATE TABLE Schedule (
    ScheduleID INT IDENTITY(1,1) NOT NULL,
    TransportLineID INT NOT NULL,
    DayOfWeek NVARCHAR(20) NOT NULL,
    DepartureTime TIME NOT NULL,
    ArrivalTime TIME NOT NULL,
    ScheduleStatus BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Schedule PRIMARY KEY (ScheduleID),
    CONSTRAINT FK_Schedule_TransportLine FOREIGN KEY (TransportLineID) REFERENCES TransportLine(TransportLineID),
    CONSTRAINT CK_Schedule_ArrivalAfterDeparture CHECK (ArrivalTime > DepartureTime),
    CONSTRAINT UQ_Schedule_TransportLineID_DayOfWeek_DepartureTime UNIQUE (TransportLineID, DayOfWeek, DepartureTime)
);
GO

-- 4.15 TransportSubscription
CREATE TABLE TransportSubscription (
    TransportSubscriptionID INT IDENTITY(1,1) NOT NULL,
    StudentID INT NOT NULL,
    TransportLineID INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    SubscriptionStatus BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_TransportSubscription PRIMARY KEY (TransportSubscriptionID),
    CONSTRAINT FK_TransportSubscription_Student FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    CONSTRAINT FK_TransportSubscription_TransportLine FOREIGN KEY (TransportLineID) REFERENCES TransportLine(TransportLineID),
    CONSTRAINT CK_TransportSubscription_EndDate CHECK (EndDate IS NULL OR EndDate >= StartDate)
);
GO

-- 4.16 SubscriptionPayment
CREATE TABLE SubscriptionPayment (
    PaymentID INT IDENTITY(1,1) NOT NULL,
    TransportSubscriptionID INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    PaymentStatus BIT NULL,
    CONSTRAINT PK_SubscriptionPayment PRIMARY KEY (PaymentID),
    CONSTRAINT FK_SubscriptionPayment_TransportSubscription FOREIGN KEY (TransportSubscriptionID) REFERENCES TransportSubscription(TransportSubscriptionID),
    CONSTRAINT CK_SubscriptionPayment_Amount CHECK (Amount > 0)
);
GO

-- 4.17 BusAssignment
CREATE TABLE BusAssignment (
    BusAssignmentID INT IDENTITY(1,1) NOT NULL,
    BusID INT NOT NULL,
    TransportLineID INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    AssignmentStatus BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_BusAssignment PRIMARY KEY (BusAssignmentID),
    CONSTRAINT FK_BusAssignment_Bus FOREIGN KEY (BusID) REFERENCES Bus(BusID),
    CONSTRAINT FK_BusAssignment_TransportLine FOREIGN KEY (TransportLineID) REFERENCES TransportLine(TransportLineID),
    CONSTRAINT CK_BusAssignment_EndDate CHECK (EndDate IS NULL OR EndDate >= StartDate)
);
GO

-- 4.18 IncidentType
CREATE TABLE IncidentType (
    IncidentTypeID INT IDENTITY(1,1) NOT NULL,
    IncidentTypeName NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_IncidentType PRIMARY KEY (IncidentTypeID),
    CONSTRAINT UQ_IncidentType_IncidentTypeName UNIQUE (IncidentTypeName)
);
GO

-- 4.19 Trip
CREATE TABLE Trip (
    TripID INT IDENTITY(1,1) NOT NULL,
    BusID INT NOT NULL,
    DriverID INT NOT NULL,
    TransportLineID INT NOT NULL,
    ScheduleID INT NOT NULL,
    TripDate DATE NOT NULL,
    ActualDepartureTime TIME NULL,
    ActualArrivalTime TIME NULL,
    TripStatus BIT NOT NULL DEFAULT 1,
    DelayInMinutes INT NOT NULL DEFAULT 0,
    CONSTRAINT PK_Trip PRIMARY KEY (TripID),
    CONSTRAINT FK_Trip_Bus FOREIGN KEY (BusID) REFERENCES Bus(BusID),
    CONSTRAINT FK_Trip_Driver FOREIGN KEY (DriverID) REFERENCES Driver(DriverID),
    CONSTRAINT FK_Trip_TransportLine FOREIGN KEY (TransportLineID) REFERENCES TransportLine(TransportLineID),
    CONSTRAINT FK_Trip_Schedule FOREIGN KEY (ScheduleID) REFERENCES Schedule(ScheduleID),
    CONSTRAINT CK_Trip_DelayInMinutes CHECK (DelayInMinutes >= 0),
    CONSTRAINT CK_Trip_Times CHECK (ActualDepartureTime IS NULL OR ActualArrivalTime IS NULL OR ActualArrivalTime > ActualDepartureTime)
);
GO

-- 4.20 StudentTripAttendance
CREATE TABLE StudentTripAttendance (
    StudentTripAttendanceID INT IDENTITY(1,1) NOT NULL,
    StudentID INT NOT NULL,
    TripID INT NOT NULL,
    BoardingStationID INT NULL,
    DropOffStationID INT NULL,
    BoardingTime TIME NULL,
    DropOffTime TIME NULL,
    AttendanceStatus BIT NOT NULL DEFAULT 1,
    Notes NVARCHAR(500) NULL,
    CONSTRAINT PK_StudentTripAttendance PRIMARY KEY (StudentTripAttendanceID),
    CONSTRAINT FK_StudentTripAttendance_Student FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    CONSTRAINT FK_StudentTripAttendance_Trip FOREIGN KEY (TripID) REFERENCES Trip(TripID),
    CONSTRAINT FK_StudentTripAttendance_BoardingStation FOREIGN KEY (BoardingStationID) REFERENCES Station(StationID),
    CONSTRAINT FK_StudentTripAttendance_DropOffStation FOREIGN KEY (DropOffStationID) REFERENCES Station(StationID),
    CONSTRAINT UQ_StudentTripAttendance_StudentID_TripID UNIQUE (StudentID, TripID),
    CONSTRAINT CK_StudentTripAttendance_Times CHECK (BoardingTime IS NULL OR DropOffTime IS NULL OR DropOffTime > BoardingTime)
);
GO

-- 4.21 Incident
CREATE TABLE Incident (
    IncidentID INT IDENTITY(1,1) NOT NULL,
    TripID INT NOT NULL,
    ReportedByEmployeeID INT NOT NULL,
    IncidentTypeID INT NOT NULL,
    IncidentDescription NVARCHAR(500) NULL,
    IncidentDateTime DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT PK_Incident PRIMARY KEY (IncidentID),
    CONSTRAINT FK_Incident_Trip FOREIGN KEY (TripID) REFERENCES Trip(TripID),
    CONSTRAINT FK_Incident_Employee FOREIGN KEY (ReportedByEmployeeID) REFERENCES Employee(EmployeeID),
    CONSTRAINT FK_Incident_IncidentType FOREIGN KEY (IncidentTypeID) REFERENCES IncidentType(IncidentTypeID)
);
GO

-- ============================================================
-- Indexes
-- ============================================================

CREATE INDEX IX_Municipality_WilayaID ON Municipality(WilayaID);
GO
CREATE INDEX IX_Speciality_DepartmentID ON Speciality(DepartmentID);
GO
CREATE INDEX IX_Student_SpecialityID ON Student(SpecialityID);
GO
CREATE INDEX IX_Station_MunicipalityID ON Station(MunicipalityID);
GO
CREATE INDEX IX_Bus_BusModelID ON Bus(BusModelID);
GO
CREATE INDEX IX_TransportSubscription_StudentID ON TransportSubscription(StudentID);
GO
CREATE INDEX IX_TransportSubscription_TransportLineID ON TransportSubscription(TransportLineID);
GO
CREATE INDEX IX_SubscriptionPayment_SubscriptionID ON SubscriptionPayment(TransportSubscriptionID);
GO
CREATE INDEX IX_BusAssignment_BusID ON BusAssignment(BusID);
GO
CREATE INDEX IX_BusAssignment_TransportLineID ON BusAssignment(TransportLineID);
GO
CREATE INDEX IX_Schedule_TransportLineID ON Schedule(TransportLineID);
GO
CREATE INDEX IX_Trip_BusID ON Trip(BusID);
GO
CREATE INDEX IX_Trip_DriverID ON Trip(DriverID);
GO
CREATE INDEX IX_Trip_TransportLineID ON Trip(TransportLineID);
GO
CREATE INDEX IX_Trip_ScheduleID ON Trip(ScheduleID);
GO
CREATE INDEX IX_StudentTripAttendance_TripID ON StudentTripAttendance(TripID);
GO
CREATE INDEX IX_StudentTripAttendance_StudentID ON StudentTripAttendance(StudentID);
GO
CREATE INDEX IX_Incident_TripID ON Incident(TripID);
GO
CREATE INDEX IX_Incident_EmployeeID ON Incident(ReportedByEmployeeID);
GO
CREATE INDEX IX_Incident_IncidentTypeID ON Incident(IncidentTypeID);
GO
