CREATE TABLE dono_posto (
    donopostoid INT PRIMARY KEY DEFAULT nextval('donopostoid_sequence'),
    nome VARCHAR(35) NOT NULL,
    cpf VARCHAR(20) NOT NULL,
    email VARCHAR(200) NOT NULL,
    senhahasheada VARCHAR(255) NOT NULL,
    telefone VARCHAR(20) NOT NULL
);
CREATE TABLE posto (
    postoid INT PRIMARY KEY DEFAULT nextval('postoid_sequence'),
    nomeposto VARCHAR(40),
    cnpj VARCHAR(30) NOT NULL,
    endereco VARCHAR(60) NOT NULL,
    telefone VARCHAR(20),
    hora_funcionamento VARCHAR(50),
    donopostoid INT NOT NULL,
    gasolina DECIMAL(8,4),
    diesel DECIMAL(8,4),
    etanol DECIMAL(8,4),
    FOREIGN KEY (donopostoid) REFERENCES dono_posto(donopostoid)
);

CREATE SEQUENCE donopostoid_sequence START WITH 1;
CREATE SEQUENCE postoid_sequence START WITH 1;

