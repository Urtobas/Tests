using System.ComponentModel.DataAnnotations;

namespace Tests.Models
{
    public class QuestionBlock
    {
        [Required, Display(Name = "Выбрать тест")]
        public int Id { get; set; }

        [Required, Display(Name = "Вопрос")]
        public string Question { get; set; }

        [Required, Display(Name = "Первый вариант ответа")]
        public string Answer1 { get; set; }

        [Required, Display(Name = "Второй вариант ответа")]
        public string Answer2 { get; set; }

        [Required, Display(Name = "Третий вариант ответа")]
        public string Answer3 { get; set; }

        [Required, Display(Name = "Четвертый вариант ответа")]
        public string Answer4 { get; set; }

        [Required, Display(Name = "Номер правильного ответа")]
        public int RightAnswerNum { get; set; }

        public int TestId { get; set; } // Внешний ключ
        //public Test Test { get; set; } // Навигационное свойство


        //[Required, Display(Name = "Выбрать тест, к которому будут добавлен блок вопрос-ответ")]
        //public ICollection<Test> TestId { get; set; }



    }
}
