using BookManager.Domain.Validators;

namespace BookManager.Domain.Entities
{
    public class Subject : EntityValidate
    {
        public int Id { get; set; }
        [StringLengthRange("Descrição", 5, 20)]
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
