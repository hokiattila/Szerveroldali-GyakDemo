-- Adatbázis létrehozása, ha nem létezik
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CarSalesDB')
BEGIN
    CREATE DATABASE CarSalesDB;
END
GO

-- Adatbázis használata
USE ottakocsid;
GO

-- Táblák létrehozása
-- users tábla létrehozása
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'users')
BEGIN
    CREATE TABLE users (
        username VARCHAR(50) PRIMARY KEY,
        hashed_psw VARCHAR(256) NOT NULL,
        permission VARCHAR(3) NOT NULL DEFAULT '_1_',
        first_name VARCHAR(50) NOT NULL,
        last_name VARCHAR(50) NOT NULL,
        birth_date DATE NOT NULL,
        gender VARCHAR(10) CHECK (gender IN ('Férfi', 'Nő')) NOT NULL,
        join_date DATE NOT NULL,
        phone_number VARCHAR(30) NOT NULL DEFAULT 'unknown',
        email VARCHAR(50) NOT NULL
    );
END
GO

-- cars tábla létrehozása, VIN mint elsődleges kulcs
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'cars')
BEGIN
    CREATE TABLE cars (
        VIN VARCHAR(50) PRIMARY KEY,
        brand VARCHAR(50) NOT NULL,
        modell VARCHAR(30) NOT NULL,
        build_year SMALLINT NOT NULL,
        door_count SMALLINT NOT NULL,
        color VARCHAR(12) NOT NULL,
        weight SMALLINT NULL,
        power SMALLINT NULL,
        con VARCHAR(20) CHECK (con IN ('Totálkár', 'Újszerű', 'Új', 'Viseltes')) NOT NULL,
        fuel_type VARCHAR(20) CHECK (fuel_type IN ('Benzin', 'Diesel', 'Elektromos', 'Gázüzem', 'Hidrogén')) NOT NULL,
        price INT NOT NULL
    );
END
GO

-- pages tábla létrehozása
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pages')
BEGIN
    CREATE TABLE pages (
        url VARCHAR(15) PRIMARY KEY,
        page VARCHAR(15) NOT NULL,
        permission VARCHAR(4) NOT NULL,
        sortingorder TINYINT NOT NULL
    );
END
GO

-- Alapértelmezett adatok beszúrása
IF NOT EXISTS (SELECT * FROM users WHERE username = 'admin')
BEGIN
    INSERT INTO users (username, hashed_psw, permission, first_name, last_name, birth_date, gender, join_date, phone_number, email)
    VALUES ('admin', '$2y$10$L0.GWGh5.SSz8VV/6kIFkO592nHJmDvNUPS/pYxYGyJBGi82horJu', '__1', 'Admin', 'Admin', CAST(GETDATE() AS DATE), 'Férfi', CAST(GETDATE() AS DATE), '2', 'admin@carsales.com');
END
GO

-- cars adatok beszúrása, VIN mint kulcs
IF NOT EXISTS (SELECT * FROM cars WHERE VIN = 'SADJN3331JNCDS')
BEGIN
    INSERT INTO cars (VIN, brand, modell, build_year, door_count, color, weight, power, con, fuel_type, price)
    VALUES 
        ('SADJN3331JNCDS', 'Peugeot', '206', 2004, 5, 'fehér', 1, 110, 'Újszerű', 'Benzin', 400000),
        ('ASDFASD23232323', 'BMW', 'M3', 2011, 5, 'fekete', 2, 220, 'Viseltes', 'Diesel', 1200000),
        ('FKNGMDFJKGNDJF232', 'Mercedes-Benz', 'CLA', 2017, 5, 'fekete', 3, 180, 'Új', 'Benzin', 13000000),
        ('SDGFDFSGSFDG33', 'Audi', 'R8', 2002, 5, 'szürke', 1, 110, 'Totálkár', 'Benzin', 120000),
        ('SGFSDGDFSG', 'Mazda', 'RX7', 1992, 5, 'fehér', 1, 110, 'Újszerű', 'Benzin', 500000);
END
GO

-- pages adatok beszúrása
IF NOT EXISTS (SELECT * FROM pages WHERE url = 'home')
BEGIN
    INSERT INTO pages (url, page, permission, sortingorder)
    VALUES 
        ('home', 'Főoldal', '111', 10),
        ('contact', 'Kapcsolat', '111', 20),
        ('admin', 'Admin', '__1', 30),
        ('logout', 'Kilépés', '__1', 50);
END
GO
