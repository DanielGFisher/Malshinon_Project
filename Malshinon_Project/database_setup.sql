-- Create a database
CREATE DATABASE Malshinon CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;

-- Create the agents table
CREATE TABLE Agents (
    id INT PRIMARY KEY AUTO_INCREMENT,
    firstName VARCHAR(45),
    lastName VARCHAR(45),
    secretCode VARCHAR(45) UNIQUE,
    type VARCHAR(45),
    numReports INT,
    numMentions INT
);

-- Create the targets table
CREATE TABLE Targets (
    id INT PRIMARY KEY AUTO_INCREMENT,
    firstName VARCHAR(45),
    lastName VARCHAR(45),
    secretCode VARCHAR(45),
    type VARCHAR(45),
    numReports INT,
    numMentions INT
);

-- Create the Reports table
CREATE TABLE Reports (
    id INT PRIMARY KEY AUTO_INCREMENT,
    FOREIGN KEY (id) REFERENCES Agents(id),
    FOREIGN KEY (id) REFERENCES Targets(id),
    documentation TEXT,
    timeOfReport DATETIME DEFAULT NOW()
);