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
CREATE TABLE People (
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
    agentid int,
    FOREIGN KEY (agentid) REFERENCES Agents(id),
    targetid INT,
    FOREIGN KEY (targetid) REFERENCES Targets(id),
    documentation TEXT,
    timeOfReport DATETIME DEFAULT CURRENT_TIMESTAMP()
);