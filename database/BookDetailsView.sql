CREATE VIEW BookDetailsView AS
SELECT 
    l.CodL AS Id,
    l.Titulo AS Title,
    l.Editora AS PublisherBook,
    l.Edicao AS Edition,
    l.AnoPublicacao AS YearPublication,
    au.CodAu AS AuthorId,
    au.Nome AS Name,
    [as].codAs AS AssuntoId,
    [as].Descricao AS Description
FROM Livro l (NOLOCK)
    LEFT JOIN Livro_Autor lAu (NOLOCK)
        ON l.CodL = lAu.Livro_CodL
    LEFT JOIN Autor au (NOLOCK)
        ON lAu.Autor_CodAu = au.CodAu
    LEFT JOIN Livro_Assunto lAs (NOLOCK)
        ON l.CodL = lAs.Livro_CodL
    LEFT JOIN Assunto [as] (NOLOCK)
        ON lAs.Assunto_CodAs = [as].codAs;


		