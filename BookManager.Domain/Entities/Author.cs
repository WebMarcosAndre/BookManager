using BookManager.Domain.Validators;

namespace BookManager.Domain.Entities
{
    public class Author : EntityValidate
    {
        public int Id { get; set; }

        [StringLengthRange("Nome", 5, 40)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
