USE GestaoLivro

GO
SELECT CodAu Id, Nome Name FROM Autor (NOLOCK) WHERE CodAu = 11

INSERT INTO Autor(Nome) VALUES ('teste1')
INSERT INTO Autor(Nome) VALUES ('teste2')
INSERT INTO Autor(Nome) OUTPUT INSERTED.CodAu VALUES ('test')

INSERT INTO Livro(titulo, AnoPublicacao, Editora,Edicao) VALUES('titulo1','2020','editora2',1)
INSERT INTO Livro(titulo, AnoPublicacao, Editora,Edicao) VALUES('titulo2','2020','editora2',1)
INSERT INTO Livro(titulo, AnoPublicacao, Editora,Edicao) VALUES('titulo3','2020','editora2',1)

SELECT * FROM Autor
SELECT * FROM Assunto
SELECT * FROM Livro
SELECT * FROM Livro_Autor
SELECT * FROM Livro_Assunto



INSERT INTO Livro_Autor (Autor_CodAu, Livro_CodL) VALUES (1,1)
INSERT INTO Livro_Autor (Autor_CodAu, Livro_CodL) VALUES (1,2)

INSERT INTO Livro_Autor (Autor_CodAu, Livro_CodL) VALUES (2,1)
INSERT INTO Livro_Autor (Autor_CodAu, Livro_CodL) VALUES (2,2)

INSERT INTO Assunto(descricao) VALUES ('descricao1')
INSERT INTO Assunto(descricao) VALUES ('descricao2')

INSERT INTO Livro_Assunto(Assunto_CodAs, Livro_CodL) VALUES(1,1)
INSERT INTO Livro_Assunto(Assunto_CodAs, Livro_CodL) VALUES(1,2)
INSERT INTO Livro_Assunto(Assunto_CodAs, Livro_CodL) VALUES(2,1)
INSERT INTO Livro_Assunto(Assunto_CodAs, Livro_CodL) VALUES(2,2)

INSERT INTO Livro_Assunto(Assunto_CodAs, Livro_CodL) VALUES(1,3)



SELECT 	 l.CodL Id
    ,l.Titulo Title
    ,l.Editora PublisherBook
    ,l.Edicao Edition
    ,l.AnoPublicacao YearPublication
	,au.CodAu AuthorId
	,au.Nome Name
	,[as].codAs AssuntoId
	,[as].Descricao Description
FROM Livro l (NOLOCK)
	LEFT JOIN Livro_Autor lAu (NOLOCK)
		ON l.CodL = lAu.Livro_CodL
	LEFT JOIN Autor au (NOLOCK)
		ON lAu.Autor_CodAu = au.CodAu
	LEFT JOIN Livro_Assunto lAs (NOLOCK)
		ON l.CodL = lAs.Livro_CodL
	LEFT JOIN Assunto as [as] (NOLOCK)
		ON lAs.Assunto_CodAs = [as].codAs
WHERE l.CodL = 4