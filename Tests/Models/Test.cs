using System.ComponentModel.DataAnnotations;

namespace Tests.Models
{
    public class Test
    {
        [Required]
        public int Id { get; set; }

        [Required, Display(Name ="Уровень сложности")]
        public string DifficultyLevel { get; set; }

        [Required, Display(Name = "Название теста")]
        public string TestTitle { get; set; }

        [Required, Display(Name = "Язык програмирования")]
        public string Language { get; set; }

        [Required, Display(Name = "Автор теста")]
        public string Author { get; set; }
    }
}
