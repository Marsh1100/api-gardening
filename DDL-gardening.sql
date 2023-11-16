CREATE DATABASE IF NOT EXISTS gardening;
USE gardening;

CREATE TABLE rol (
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(50)  NOT NULL,
    PRIMARY KEY (id)
);
CREATE TABLE user (
    id int NOT NULL AUTO_INCREMENT,
    idenNumber varchar(20)  NOT NULL,
    username varchar(50) NOT NULL,
    email varchar(100)  NOT NULL,
    password varchar(255)  NOT NULL,
    PRIMARY KEY (id)
);
CREATE TABLE userRol (
    idUser int NOT NULL,
    idRol int NOT NULL,
	PRIMARY KEY (idUser, idRol),
    FOREIGN KEY (idRol) REFERENCES rol (id) ON DELETE CASCADE,
    FOREIGN KEY (idUser) REFERENCES user (id) ON DELETE CASCADE
);

CREATE TABLE refreshToken (
    id int NOT NULL AUTO_INCREMENT,
    userId int NOT NULL,
    token longtext,
    expires datetime(6) NOT NULL,
    created datetime(6) NOT NULL,
    revoked datetime(6) NULL,
    PRIMARY KEY (id),
	FOREIGN KEY (userId) REFERENCES user (id) ON DELETE CASCADE
);
CREATE TABLE office(
	id INT AUTO_INCREMENT NOT NULL,
	officineCode VARCHAR(10) NOT NULL,
	city VARCHAR(30) NOT NULL,
	country VARCHAR(50) NOT NULL,
	region VARCHAR(50) NOT NULL,
	zipCode VARCHAR(10) NOT NULL,
	phone VARCHAR(20) NOT NULL,
	address1 VARCHAR(50) NOT NULL,
	address2 VARCHAR(50) DEFAULT NULL,
	PRIMARY KEY (id)
);

CREATE TABLE employee (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    firstSurname VARCHAR(50) NOT NULL,
    secondSurname VARCHAR(50) NOT NULL,
    extension VARCHAR(10) NOT NULL,
    email VARCHAR(100) NOT NULL,
    idOffice INT NOT NULL,
    idBoss INT DEFAULT NULL,
    position VARCHAR(50) DEFAULT NULL,
    FOREIGN KEY (idOffice) REFERENCES office(id),
    FOREIGN KEY (idBoss) REFERENCES employee (id)
);

CREATE TABLE productType (
	id INT AUTO_INCREMENT NOT NULL,
    type VARCHAR(50) NOT NULL,
	descriptionText TEXT,
	descriptionHtml TEXT,
	image VARCHAR(256),
	PRIMARY KEY(id)
);

CREATE TABLE client (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nameClient VARCHAR(50) NOT NULL,
    nameContact VARCHAR(50) DEFAULT NULL,
    lastnameContact VARCHAR(50) DEFAULT NULL,
    phoneNumber VARCHAR(15) NOT NULL,
    fax VARCHAR(15) NOT NULL,
    address1 VARCHAR(50) NOT NULL,
    address2 VARCHAR(50) DEFAULT NULL,
    city VARCHAR(50) NOT NULL,
    region VARCHAR(50) DEFAULT NULL,
    country VARCHAR(50) DEFAULT NULL,
    zipCode VARCHAR(10) DEFAULT NULL,
    idEmployee INT DEFAULT NULL,
    creditLimit NUMERIC(15,2) DEFAULT NULL,
    FOREIGN KEY (idEmployee) REFERENCES employee (id)
);

CREATE TABLE request(
	id INT AUTO_INCREMENT NOT NULL,
	requestDate DATE NOT NULL,
	expectedDate DATE NOT NULL, 
	deliveryDate DATE DEFAULT NULL,
	state VARCHAR(15) NOT NULL,
	feedback TEXT,
	idClient INTEGER NOT NULL,
	PRIMARY KEY(id),
	FOREIGN KEY(idClient) REFERENCES client(id)
);

CREATE TABLE product(
	id INT AUTO_INCREMENT NOT NULL,
	productCode VARCHAR(15) NOT NULL,
	name VARCHAR(70) NOT NULL,
	idProductType INT NOT NULL,
	dimensions VARCHAR(25) NULL,
	provider VARCHAR(50) DEFAULT NULL,
	description TEXT NULL, 
	stock SMALLINT NOT NULL,
	salePrice NUMERIC(15,2) NOT NULL,
	providerPrice NUMERIC(15,2) DEFAULT NULL,
	PRIMARY KEY (id),
	FOREIGN KEY (idProductType) REFERENCES productType (id)
);

CREATE TABLE requestDetail (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    idRequest INT NOT NULL,
    idProduct INT NOT NULL,
    quantity INT NOT NULL,
    unitPrice NUMERIC(15,2),
    lineNumber SMALLINT NOT NULL,
    FOREIGN KEY  (idRequest) REFERENCES request (id),
    FOREIGN KEY  (idProduct) REFERENCES product (id)
    );
CREATE TABLE payment (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    idClient INT NOT NULL,
    transactionId VARCHAR(50) NOT NULL,
    paymentMethod VARCHAR(40) NOT NULL,
    paymentDate DATE NOT NULL,
    total NUMERIC(15,2) NOT NULL,
    FOREIGN KEY (idClient) REFERENCES client (id)
);
