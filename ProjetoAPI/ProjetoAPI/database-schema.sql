CREATE TABLE dono_posto (
    donopostoid INT PRIMARY KEY DEFAULT nextval('hibernate_sequence'),
    nome VARCHAR(35) NOT NULL,
    cpf VARCHAR(11) NOT NULL,
    email VARCHAR(200) NOT NULL,
    senhahasheada VARCHAR(255) NOT NULL,
    telefone VARCHAR(20) NOT NULL
);
CREATE TABLE posto (
    postoid INT PRIMARY KEY DEFAULT nextval('hibernate_sequence'),
    cnpj VARCHAR(14) NOT NULL,
    endereco VARCHAR(60) NOT NULL,
    telefone VARCHAR(20),
    hora_funcionamento VARCHAR(50),
    bandeira VARCHAR(15),
    donopostoid INT NOT NULL,
    gasolina DECIMAL(3,2),
    diesel DECIMAL(3,2),
    etanol DECIMAL(3,2),
    FOREIGN KEY (donopostoid) REFERENCES dono_posto(donopostoid)
);

CREATE SEQUENCE hibernate_sequence START WITH 1;

