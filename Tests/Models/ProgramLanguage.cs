using System.ComponentModel.DataAnnotations;

namespace Tests.Models
{
    public class ProgramLanguage
    {
        public int Id { get; set; }

        [Display(Name ="Добавить язык программирования")]
        public string LanguageTitle { get; set; }
    }
}
