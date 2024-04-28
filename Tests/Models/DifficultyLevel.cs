﻿using System.ComponentModel.DataAnnotations;

namespace Tests.Models
{
    public class DifficultyLevel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Название уровеня сложности")]
        public string Level { get; set; }
    }
}
