CREATE TABLE [dbo].[Leitores]
(
	[nleitor] INT NOT NULL PRIMARY KEY identity,
	nome varchar(40) not null,
	data_nasc date,
	fotografia IMAGE,
	ativo BIT
)
CREATE TABLE [dbo].[Livros]
(
	[nlivro] INT NOT NULL PRIMARY KEY identity,
	nome varchar(100),
	ano int,
	data_aquisicao date,
	preco decimal(4,2),
	capa varchar(300),
	estado bit
)
CREATE TABLE [dbo].[Emprestimos]
(
	[nemprestimo] INT NOT NULL PRIMARY KEY identity,
	nlivro int,
	nleitor int,
	data_emprestimo date,
	data_devolve date,
	estado bit,
	foreign key (nlivro) references Livros(nlivro),
	foreign key (nleitor) references Leitores(nleitor)
)
/*como testar se uma tabela existe antes de tentar criar*/
if not exists (select * from sysobjects where name='nome' and xtype='U')
    create table nome (
        campo varchar(64) not null
    )
go