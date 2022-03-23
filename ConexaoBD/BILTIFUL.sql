	SET LANGUAGE 'Brazilian'

CREATE TABLE Cliente
(
	CPF nvarchar(14) not null,
	Nome nvarchar(50) not null,
	DataNasc date  not null,
	Sexo char not null,
	UltimaCompra date not null DEFAULT CONVERT(DATE, GETDATE()),
	DataCadastro date not null DEFAULT CONVERT(DATE, GETDATE()),
	Situacao CHAR NOT NULL DEFAULT 'A',
	CONSTRAINT pk_Cliente PRIMARY KEY (CPF)
);


CREATE TABLE Fornecedor
(
	CNPJ NVARCHAR(18) NOT NULL,
	Razao_Social NVARCHAR(50) NOT NULL,
	Data_Abertura DATE NOT NULL ,
	Ultima_Compra DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Data_Cadastro DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Situacao CHAR NOT NULL DEFAULT 'A',
	CONSTRAINT PK_CNPJ_Fornecedor PRIMARY KEY (CNPJ)
);


CREATE TABLE Materia_Prima
(
	ID NVARCHAR(6) NOT NULL,
	Nome NVARCHAR(20) NOT NULL,
	Ultima_Compra DATE DEFAULT CONVERT(DATE, GETDATE()),
	Data_Cadastro DATE DEFAULT CONVERT(DATE, GETDATE()),
	Situacao CHAR
	CONSTRAINT PK_Materia_Prima PRIMARY KEY(ID),
	CONSTRAINT UN_Materia_Prima UNIQUE (ID, Nome)
);

CREATE TABLE Produto 
(
	Codigo_Barras NVARCHAR(13) NOT NULL,
	Nome NVARCHAR(20) NOT NULL,
	Valor_Venda DECIMAL (10,2) NOT NULL,
	Ultima_Venda DATE,
	Data_Cadastro DATE,
	Situacao CHAR NOT NULL,
	CONSTRAINT PK_Produto PRIMARY KEY(Codigo_Barras),
	CONSTRAINT UN_Produto UNIQUE (Codigo_Barras, Nome)
);

CREATE TABLE Venda 
(
	Id INT IDENTITY(1, 1) NOT NULL,
	Data_Venda DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	CPF_Cliente NVARCHAR(14) NOT NULL,
	Valor_Total DECIMAL(7,2) NULL,
	CONSTRAINT fk_cliente FOREIGN KEY (CPF_Cliente) REFERENCES Cliente (CPF),
	CONSTRAINT pk_venda PRIMARY KEY (Id),
	CONSTRAINT UN_Venda UNIQUE (Id, CPF_Cliente)
);

CREATE TABLE Item_Venda 
(
	Id INT NOT NULL,
	Produto NVARCHAR(13) NOT NULL,
	Quantidade FLOAT NOT NULL DEFAULT 0.00,
	Valor_Unitario DECIMAL(5, 2) NOT NULL,
	Total_Item DECIMAL(7,2) NULL,
	CONSTRAINT fk_produto_item_venda FOREIGN KEY (Produto) REFERENCES
	Produto(Codigo_Barras),
	CONSTRAINT pk_venda_item PRIMARY KEY (Id, produto),
	CONSTRAINT FK_Venda FOREIGN KEY (Id) REFERENCES Venda(Id)
);

CREATE TABLE Producao
(
	ID INT IDENTITY NOT NULL,
	DataProducao DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Produto NVARCHAR (13),
	Quantidade DECIMAL (5,2),
	CONSTRAINT PK_Producao PRIMARY KEY (ID),
	CONSTRAINT FK_Produto FOREIGN KEY(Produto) REFERENCES
	Produto(Codigo_Barras)
);

CREATE TABLE Item_Producao
(
	ID INT NOT NULL,
	Data_Producao DATE ,
	Id_Materia_Prima NVARCHAR (6) NOT NULL,
	Quantidade_Materia_Prima DECIMAL (5,2) NOT NULL,
	CONSTRAINT PK_ItemProducao PRIMARY KEY (ID),
	CONSTRAINT FK_IdProducao FOREIGN KEY (ID) REFERENCES Producao(ID),
	CONSTRAINT FK_Materia_Prima FOREIGN KEY (Id_Materia_Prima) REFERENCES
	Materia_Prima(ID)
);

CREATE TABLE Compra (
	ID INT IDENTITY(1,1)NOT NULL,
	Data_Compra DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Fornecedor NVARCHAR(18) NOT NULL,
	Valor_Total DECIMAL(7,2) NOT NULL,
	CONSTRAINT PK_Compra PRIMARY KEY(Id),
	CONSTRAINT FK_Fornecedor FOREIGN KEY(Fornecedor) REFERENCES
	Fornecedor(CNPJ)
);

CREATE TABLE Item_Compra
(
	ID INT NOT NULL,
	Data_Compra DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Codigo_MPrima NVARCHAR(6) NOT NULL,
	Quantidade INT NULL DEFAULT 0,
	Valor_Unitario DECIMAL(5,2) NULL DEFAULT 0,
	Total_Item DECIMAL(6,2) NULL DEFAULT 0.0,
	CONSTRAINT PK_Id_Item_Compra PRIMARY KEY (ID),
	CONSTRAINT FK_IdCompra FOREIGN KEY (ID) REFERENCES Compra(ID),
	CONSTRAINT FK_Materia_Prima_Item FOREIGN KEY (Codigo_MPrima) REFERENCES
	Materia_Prima(ID)
);
