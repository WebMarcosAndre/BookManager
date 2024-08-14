using BookManager.Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace BookManager.Domain.Entities
{
    public class Book : EntityValidate
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Título é obrigatório.")]
        [StringLengthRange("Título", 5, 40)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Editora é obrigatório.")]
        [StringLengthRange("Editora", 5, 40)]
        public string PublisherBook { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Edição deve ser maior que zero (0).")]
        public int Edition { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ano de publicação é obrigatório.")]
        [YearLessThanOrEqualToCurrentYear("ano de publicação")]
        public string YearPublication { get; set; }

        public ICollection<Author> Authors { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
